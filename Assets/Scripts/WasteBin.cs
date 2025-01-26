using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WasteBin : MonoBehaviour
{
    public Sprite spriteOpen;
    public Sprite spriteClosed;
    public SpriteRenderer spriteRenderer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bubble bubble = collision.GetComponent<Bubble>();
        if (bubble && (bubble.state == BubbleState.FLOATING || bubble.state == BubbleState.STICKY) && bubble.isDragged)
        {
            Debug.Log("Info bubble has hit wast bin");
            spriteRenderer.sprite = spriteClosed;

            Destroy(collision.gameObject);

            StartCoroutine(EatingProcess(0.5f));
        }
    }

    IEnumerator EatingProcess(float washingTime)
    {
        yield return new WaitForSeconds(washingTime);
        spriteRenderer.sprite = spriteOpen;
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
