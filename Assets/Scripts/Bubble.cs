using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Bubble : MonoBehaviour
{
    public BubbleState state = BubbleState.FLOATING;
    public bool isDragged = false; // basically defined by bubble state but helpful flag
    public bool isDraggable = true;
    public bool isWashed = false;
    public BubbleScriptableObject bubbleData;
    public Rigidbody2D rigidbody;
    public float floatingVelocity = 0.2f;
    public CircleCollider2D circleCollider;
    public SpriteRenderer renderer;

    public bool applyImage = false;

    public void Start()
    {
       
        SetImage();

    }
    public void OnValidate()
    {
        if (applyImage)
        {
            SetImage();
        }
    }

    

    public void SetImage()
    {
         // GetComponent<SpriteRenderer>();
        Texture2D skin = bubbleData.bubbleImage.texture;
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", skin);
        renderer.SetPropertyBlock(block);


    }
    public void Update()
    {
        if ((state == BubbleState.FLOATING) || (state == BubbleState.STICKY)) // Add velocity to center
        {
            Vector2 velocity = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            velocity.Normalize();
            rigidbody.velocity = velocity * -floatingVelocity;
        }
    }

    public IEnumerator OnAttachedToMindBubble()
    {
        state = BubbleState.STICKY;
        // shring radius
        float radius = GetComponent<CircleCollider2D>().radius;
        // Debug.Log("start shrinking bubble Collider");
        while (true)
        {

            if (circleCollider.radius < 0.01f)
            {
                circleCollider.enabled = false;
                // Debug.Log("Collider disabled");
                yield return new WaitForSeconds(1.5f);
                circleCollider.radius = 0.5f;
                circleCollider.enabled = true;

                // Debug.Log("Collider enabled");
                //state = BubbleState.INSIDE;


                break;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                circleCollider.radius -= 0.01f;
            }

        }
       

    }

    async Task WaitForSecondsAsync(float delay)
    {
        await Task.Delay(TimeSpan.FromSeconds(delay));
    }
     

    public static Texture2D RoundCrop(Texture2D sourceTexture)
    {
        int width = sourceTexture.width;
        int height = sourceTexture.height;
        float radius = (width < height) ? (width / 2f) : (height / 2f);
        float centerX = width / 2f;
        float centerY = height / 2f;
        Vector2 centerVector = new Vector2(centerX, centerY);

        // pixels are laid out left to right, bottom to top (i.e. row after row)
        Color[] colorArray = sourceTexture.GetPixels(0, 0, width, height);
        Color[] croppedColorArray = new Color[width * height];

        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                int colorIndex = (row * width) + column;
                float pointDistance = Vector2.Distance(new Vector2(column, row), centerVector);
                if (pointDistance < radius)
                {
                    croppedColorArray[colorIndex] = colorArray[colorIndex];
                }
                else
                {
                    croppedColorArray[colorIndex] = Color.clear;
                }
            }
        }
        Texture2D croppedTexture = new Texture2D(width, height);
        croppedTexture.SetPixels(croppedColorArray);
        croppedTexture.Apply();
        return croppedTexture;
    }

    public void SetRevealed()
    {
        SetImage();
        Debug.Log("Show Stats");
        state = BubbleState.FLOATING;
        isWashed = true;
        // Stats could be set to nicer values
    }

    public void DestroyWithEffect()
    {
        // Add animation

        Destroy(this.gameObject);
    }
}



public enum BubbleState {
    FLOATING,   // fresh bubble (most of the time obfoscated)
    STICKY,     // attached to big mental bubble
    WASHING,    // in washing machine
    INSIDE,     // in big bubble (because accepted or invaded)
    DECLINED    // dragged to wastebin
}



   