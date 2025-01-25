using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float anxiety = 0;
    public float distrust = 0;
    public float loneliness = 0;
    public float fomo = 0;
    public float doomScrolling = 0;

    public GameObject anxietySpike;
    public GameObject distrustSpike;
    public GameObject lonelinessSpike;
    public GameObject fomoSpike;
    public GameObject doomScrollingSpike;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anxiety != anxietySpike.transform.localPosition.y) {
            anxietySpike.transform.localPosition = new Vector3(anxietySpike.transform.localPosition.x, anxiety, anxietySpike.transform.localPosition.z);
        }
        if (distrust != distrustSpike.transform.localPosition.y) {
            distrustSpike.transform.localPosition = new Vector3(distrustSpike.transform.localPosition.x, distrust, distrustSpike.transform.localPosition.z);
        }
        if (fomo != fomoSpike.transform.localPosition.y) {
            fomoSpike.transform.localPosition = new Vector3(fomoSpike.transform.localPosition.x, fomo, fomoSpike.transform.localPosition.z);
        }
        if (loneliness != lonelinessSpike.transform.localPosition.y) {
            lonelinessSpike.transform.localPosition = new Vector3(lonelinessSpike.transform.localPosition.x, loneliness, lonelinessSpike.transform.localPosition.z);
        }
        if (doomScrolling != doomScrollingSpike.transform.localPosition.y) {
            doomScrollingSpike.transform.localPosition = new Vector3(doomScrollingSpike.transform.localPosition.x, doomScrolling, doomScrollingSpike.transform.localPosition.z);
        }
    }
}
