using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{

    public float spawnRate;
    public Bubble bubble; // gameobject without values
    public List<BubbleScriptableObject> bubbleContent;
    public bool spawnNow;

    public RectTransform spawnArea;
    // Start is called before the first frame update
    void Start()
    {
        spawnArea = this.GetComponent<RectTransform>();
    }
    private void OnValidate()
    {
        if (spawnNow)
        {
            Spawn();
            spawnNow = false;
        }
    }

    public void Spawn()
    {
        // choose new position
        float xPos = Random.Range(spawnArea.offsetMin.x, spawnArea.offsetMax.x);
        float yPos = Random.Range(spawnArea.offsetMin.y, spawnArea.offsetMax.y);
        Debug.Log($"spawnArea: {spawnArea.offsetMin.x}");
       
        Bubble newBubble =  Instantiate(bubble,new Vector2(xPos,yPos), new Quaternion());

        // Assign data:
        var bubbleCont = bubbleContent[0];
        bubbleContent.RemoveAt(0);
        newBubble.bubbleData = bubbleCont;


    }
}
