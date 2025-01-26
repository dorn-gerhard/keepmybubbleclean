using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WasteBin : MonoBehaviour
{
    public Sprite spriteOpen;
    public Sprite spriteClosed;
    public Sprite spriteBig;

    public SpriteRenderer spriteRenderer;

    public bool isEating = false;
    public bool isBig = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bubble bubble = collision.GetComponent<Bubble>();
        if (bubble && (bubble.state == BubbleState.FLOATING || bubble.state == BubbleState.STICKY) && bubble.isDragged && !isEating)
        {
            // Debug.Log("Info bubble has hit wast bin");
            spriteRenderer.sprite = spriteClosed;
            isEating = true;

            Destroy(collision.gameObject);

            StartCoroutine(EatingProcess(3f));
        }
    }

    IEnumerator EatingProcess(float washingTime)
    {
        var shortTime = 0.3f;
        
        while (washingTime > 0) {
            yield return new WaitForSeconds(shortTime);

            if (isBig) {
                spriteRenderer.sprite = spriteClosed;
            } else {
                spriteRenderer.sprite = spriteBig;
            }

            isBig = !isBig;
            washingTime -= shortTime;
        }

        yield return new WaitForSeconds(washingTime);
        spriteRenderer.sprite = spriteOpen;
        isEating = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
