using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "New Bubble", menuName = "Assets/Bubble")]
public class BubbleScriptableObject : ScriptableObject
{
    // bubble data
    public float anxiety;
    public float distrust;
    public float fomo;
    public float loneliness;
    public float doomScrolling = 1;

    public Sprite bubbleImage;
}
