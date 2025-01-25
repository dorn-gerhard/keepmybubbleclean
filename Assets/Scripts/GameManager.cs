using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float anxiety = 0;
    public float distrust = 0;
    public float fomo = 0;
    public float loneliness = 0;
    public float doomScrolling = 0;

    public GameObject anxietySpike;
    public GameObject distrustSpike;
    public GameObject fomoSpike;
    public GameObject lonelinessSpike;
    public GameObject doomScrollingSpike;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anxietySpike.transform.localPosition = new Vector3(anxietySpike.transform.localPosition.x, anxiety, anxietySpike.transform.localPosition.z);
        distrustSpike.transform.localPosition = new Vector3(distrustSpike.transform.localPosition.x, distrust, distrustSpike.transform.localPosition.z);
        fomoSpike.transform.localPosition = new Vector3(fomoSpike.transform.localPosition.x, fomo, fomoSpike.transform.localPosition.z);
        lonelinessSpike.transform.localPosition = new Vector3(lonelinessSpike.transform.localPosition.x, loneliness, lonelinessSpike.transform.localPosition.z);
        doomScrollingSpike.transform.localPosition = new Vector3(doomScrollingSpike.transform.localPosition.x, doomScrolling, doomScrollingSpike.transform.localPosition.z);
    }
}
