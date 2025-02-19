using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindBubble : MonoBehaviour
{
    public CircleCollider2D circleCollider;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Bubble bubble = collision.GetComponent<Bubble>();
        if ((bubble is not null) && ((bubble.state == BubbleState.FLOATING) || (bubble.state == BubbleState.STICKY)))
        {
            // Debug.Log("Info bubble has hit mind bubble");
            StartCoroutine(collision.GetComponent<Bubble>().OnAttachedToMindBubble());
        }
    }
}
    