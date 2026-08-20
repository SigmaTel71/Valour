import { test } from 'node:test';
import assert from 'node:assert/strict';

const {
  enqueueVillageBubble,
  getVillageBubbleAlpha,
  VILLAGE_BUBBLE_HOLD_MS,
  VILLAGE_BUBBLE_FADE_MS,
  VILLAGE_BUBBLE_STACK_LIMIT
} = await import('../../Client/wwwroot/ts/VillageChatBubbles.js');

test('chat bubbles retain a bounded vertical stack per speaker', () => {
  const bubbles = new Map();

  for (let index = 1; index <= VILLAGE_BUBBLE_STACK_LIMIT + 1; index++) {
    enqueueVillageBubble(bubbles, 42, `message ${index}`, index * 1000);
  }

  assert.equal(bubbles.get('42').length, VILLAGE_BUBBLE_STACK_LIMIT);
  assert.equal(bubbles.get('42')[0].text, 'message 2');
  assert.equal(bubbles.get('42').at(-1).text, `message ${VILLAGE_BUBBLE_STACK_LIMIT + 1}`);
});

test('an optimistic bubble and its prompt server echo are deduplicated', () => {
  const bubbles = new Map();
  enqueueVillageBubble(bubbles, 'me', 'hello', 1000, true);
  enqueueVillageBubble(bubbles, 'me', 'hello', 2500);

  assert.equal(bubbles.get('me').length, 1);
  assert.equal(bubbles.get('me')[0].bornAt, 2500);
  assert.equal(bubbles.get('me')[0].optimistic, false);
});

test('two confirmed messages with identical text still stack', () => {
  const bubbles = new Map();
  enqueueVillageBubble(bubbles, 'friend', 'hello', 1000);
  enqueueVillageBubble(bubbles, 'friend', 'hello', 1100);

  assert.equal(bubbles.get('friend').length, 2);
});

test('chat bubbles hold longer and then fade smoothly', () => {
  assert.equal(getVillageBubbleAlpha(VILLAGE_BUBBLE_HOLD_MS), 1);
  assert.equal(
    getVillageBubbleAlpha(VILLAGE_BUBBLE_HOLD_MS + VILLAGE_BUBBLE_FADE_MS / 2),
    0.5);
  assert.equal(
    getVillageBubbleAlpha(VILLAGE_BUBBLE_HOLD_MS + VILLAGE_BUBBLE_FADE_MS),
    0);
});
