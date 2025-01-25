using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Bubble : MonoBehaviour
{
    public BubbleState state = BubbleState.FLOATING;
    public bool dragable = true; // basically defined by bubble state but helpful flag

    public bool mystery = true;
    public BubbleScriptableObject bubbleData;
    public Rigidbody2D rigidbody;
    public float floatingVelocity = 0.2f;

    public bool applyImage = false;

    public void OnStart()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
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
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Texture2D skin = bubbleData.bubbleImage.texture;
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", skin);
        renderer.SetPropertyBlock(block);


    }
    public void Update()
    {
        if (state == BubbleState.FLOATING) // Add velocity to center
        {
            Vector2 velocity = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            velocity.Normalize();
            gameObject.GetComponent<Rigidbody2D>().velocity = velocity * -floatingVelocity;
        }
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
}

public enum BubbleState {
    FLOATING,   // fresh bubble (most of the time obfoscated)
    STICKY,     // attached to big mental bubble
    WASHING,    // in washing machine
    INSIDE,     // in big bubble (because accepted or invaded)
    DECLINED    // dragged to wastebin
}



   