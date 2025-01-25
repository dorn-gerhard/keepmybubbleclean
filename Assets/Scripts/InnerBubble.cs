using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerBubble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bubble bubble = collision.GetComponent<Bubble>();
        if (bubble && ((bubble.state == BubbleState.FLOATING) || (bubble.state == BubbleState.STICKY)))
        {
            bubble.state = BubbleState.INSIDE;
            GameManager.Instance.AddValues(bubble.bubbleData.anxiety, bubble.bubbleData.distrust, bubble.bubbleData.loneliness, bubble.bubbleData.fomo, bubble.bubbleData.doomScrolling);

        }
    }
}
 