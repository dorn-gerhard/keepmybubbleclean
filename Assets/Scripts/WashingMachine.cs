using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : MonoBehaviour
{

    public Sprite spriteOpen;
    public Sprite spriteClosed;
    public SpriteRenderer spriteRenderer;
    public bool isOpen = true;
    public GameObject playerController;
    public ParticleSystem particleSystem;

    public Bubble washedBubble;
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
        // Debug.Log("Washing machine collision");
        Bubble bubble = collision.GetComponent<Bubble>();
        if (bubble && (bubble.state == BubbleState.FLOATING || bubble.state == BubbleState.STICKY) && bubble.isDragged && !bubble.isWashed && isOpen)
        {
            // Debug.Log("Info bubble has hit washing machine");

            // spriteRenderer.sprite = spriteClosed;
            isOpen = false;
            spriteRenderer.sprite = spriteClosed;

            particleSystem.Play();

            bubble.state = BubbleState.WASHING;
            bubble.rigidbody.velocity = new Vector3(0f, 0f, 0f);
            bubble.gameObject.transform.position = this.transform.position + new Vector3(0f, 1.55f, 0f);

            bubble.isDragged = false;
            
            
            bubble.gameObject.SetActive(false);
            washedBubble = bubble;

            playerController.GetComponent<PlayerController>().selectedObject = null;

            AudioManager.Instance.PlayScrub();
            AudioManager.Instance.PlayBubble();
            StartCoroutine(WashingProcess(3.0f));
        }

    }

    IEnumerator WashingProcess(float washingTime)
    {
        yield return new WaitForSeconds(washingTime);
        washedBubble.gameObject.SetActive(true);
        washedBubble.gameObject.transform.position = this.transform.position + new Vector3(0f, 1.55f, 0f);
        washedBubble.SetRevealed();
        spriteRenderer.sprite = spriteOpen;
        isOpen = true;



    }
}
