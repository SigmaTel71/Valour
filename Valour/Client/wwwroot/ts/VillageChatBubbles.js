export const VILLAGE_BUBBLE_HOLD_MS = 7000;
export const VILLAGE_BUBBLE_FADE_MS = 1200;
export const VILLAGE_BUBBLE_STACK_LIMIT = 4;
/**
 * Adds a bubble to a speaker's bounded queue. A matching line arriving soon
 * afterward is the server echo of our optimistic local bubble, so refresh it
 * rather than displaying the same message twice.
 */
export function enqueueVillageBubble(bubbles, userId, text, now = performance.now(), optimistic = false) {
    const key = String(userId);
    const safeText = String(text);
    const queue = bubbles.get(key) ?? [];
    let pendingIndex = -1;
    if (!optimistic) {
        for (let index = queue.length - 1; index >= 0; index--) {
            if (queue[index].optimistic && queue[index].text === safeText) {
                pendingIndex = index;
                break;
            }
        }
    }
    if (pendingIndex >= 0) {
        // Replace the optimistic card with its confirmed server echo and move
        // it to the end so the visual order follows confirmed message order.
        queue.splice(pendingIndex, 1);
        queue.push({ text: safeText, bornAt: now, optimistic: false });
    }
    else {
        queue.push({ text: safeText, bornAt: now, optimistic });
        if (queue.length > VILLAGE_BUBBLE_STACK_LIMIT) {
            queue.splice(0, queue.length - VILLAGE_BUBBLE_STACK_LIMIT);
        }
    }
    bubbles.set(key, queue);
    return queue;
}
export function getVillageBubbleAlpha(age, holdMs = VILLAGE_BUBBLE_HOLD_MS, fadeMs = VILLAGE_BUBBLE_FADE_MS) {
    if (age <= holdMs) {
        return 1;
    }
    return Math.max(0, 1 - ((age - holdMs) / fadeMs));
}
//# sourceMappingURL=VillageChatBubbles.js.map