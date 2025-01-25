using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : MonoBehaviour
{

    public Sprite spriteOpen;
    public Sprite spriteClosed;
    public SpriteRenderer spriteRenderer;
    public Boolean isOpen = true;
    public GameObject playerController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        // if (isOpen)
        // {
        //     spriteRenderer.sprite = spriteOpen;
        // }
        // else
        // {
        //     spriteRenderer.sprite = spriteClosed;
        // }
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Washing machine collision");
        Bubble bubble = collision.GetComponent<Bubble>();
        if (bubble && bubble.state == BubbleState.FLOATING && bubble.isDragged)
        {
            Debug.Log("Info bubble has hit washing machine");

            // spriteRenderer.sprite = spriteClosed;
            isOpen = false;
            spriteRenderer.sprite = spriteClosed;

            bubble.state = BubbleState.WASHING;
            bubble.isDragged = false;
            playerController.GetComponent<PlayerController>().selectedObject = null;
        }

    }
}
