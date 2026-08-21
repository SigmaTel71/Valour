# Direct and Group Calls

Valour direct calls are first-class call attempts attached to direct-message
conversations. They do not create hidden voice-channel rows and they do not use
peer-to-peer WebRTC. Media is carried by the instance voice provider selected by
`VoiceCoordinator` (Cloudflare RealtimeKit by default, or instance LiveKit).
Planet-owned voice configuration is intentionally not used for private calls.

## Conversation model

- `DirectChat` remains a private two-person conversation.
- `GroupChat` contains 3–25 members and has a name.
- `ChannelMember.IsAdmin` controls group rename, invite, and removal operations.
- When someone is added during a 1:1 call, Valour creates a fresh group
  conversation. The original 1:1 history is never exposed to the new person.
- The call keeps the same call id and provider room while its `ChannelId` moves
  to the new group conversation.

## Call lifecycle

`DirectCall` stores `Ringing`, `Active`, and `Ended` state. Each
`DirectCallMember` independently stores `Invited`, `Joined`, `Declined`, or
`Left` state. A caller starts joined; the first acceptance activates the call.
Ringing invitations expire after 45 seconds. A background worker records missed
calls and closes their provider rooms.

Call updates are relayed to each user's primary-node SignalR group through the
existing inter-node relay. This makes ringing and participant changes work when
users are connected to different application nodes.

Only one active or ringing call is allowed per user, including planet voice
presence. Blocks are always respected. Call privacy is separate from DM privacy
and defaults to `FriendsOnly`.

## Media and cleanup

Provider rooms are keyed by `DirectCall.Id`. An ephemeral server channel model is
used only to select the provider's audio/video preset; no corresponding channel
is persisted. Direct-call membership is validated before every token issuance.
LiveKit credentials issued for direct calls expire after five minutes. RealtimeKit
leave/removal cleanup both ejects the active peer and deletes its meeting
participant record, invalidating the reusable participant credential.

The planet voice cleanup worker ignores active direct-call room ids because
their presence is not stored in the planet Redis voice keys. The direct-call
cleanup worker owns their expiry and teardown. RealtimeKit orphan cleanup also
skips provider rooms that are still explicitly tracked.

## API summary

- `POST api/direct-calls` — start a voice or video call
- `GET api/direct-calls/current` — restore calls after reconnect/startup
- `POST api/direct-calls/{id}/accept|decline|leave|end`
- `POST api/direct-calls/{id}/participants` — invite additional people
- `POST api/direct-calls/{id}/token` — issue media credentials to joined members
- `POST api/channels/group` — create a group DM
- `POST api/channels/group/{id}/members` — add members (admin)
- `PUT api/channels/group/{id}` — rename (admin)
- `DELETE api/channels/group/{id}/members/{userId}` — leave/remove

## Test coverage

Provider unit tests validate LiveKit token lifetime/grants and RealtimeKit
participant-record revocation without external network calls. Database-backed
integration tests cover call lifecycle, call privacy, authorization, busy-user
enforcement, missed-call expiry, participant expansion, group administration,
and preservation of the original 1:1 conversation when a call becomes a group.

```bash
dotnet test Valour/Tests/Valour.Tests.csproj --filter \
  "FullyQualifiedName~LiveKitTokenTests|FullyQualifiedName~RealtimeKitReconciliationTests|FullyQualifiedName~DirectCallServiceTests|FullyQualifiedName~DirectCallApiTests|FullyQualifiedName~ChannelServiceTests.GroupDm_|FullyQualifiedName~ChannelServiceTests.AddingPeopleToDirectDm"
```

## Operational recommendation

Use Cloudflare RealtimeKit as the managed default and instance-wide LiveKit for
self-hosted deployments. Keep private calls off planet-operated SFUs: a planet
operator should not receive participant IP addresses or call metadata for a DM.
Push notifications and call-history presentation can be layered onto the durable
call records without changing the media architecture.
