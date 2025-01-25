using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public BubbleState state = BubbleState.FLOATING;
    public bool dragable = true; // basically defined by bubble state but helpful flag

    public bool mystery = true;
    public BubbleScriptableObject bubbleData;

    
}

public enum BubbleState {
    FLOATING,   // fresh bubble (most of the time obfoscated
    STICKY,     // attached to big mental bubble
    WASHING,    // in washing machine
    INSIDE,     // in big bubble (because accepted or invaded)
    DECLINED    // dragged to wastebin
}
