using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // The values of the Axis 0 = start, 1 = lost game
    public float anxiety = 0;
    public float distrust = 0;
    public float loneliness = 0;
    public float fomo = 0;
    public float doomScrolling = 0;

    // Initial values
    private int totalAnxiety = 0;
    private int totalDistrust = 0;
    private int totalLoneliness = 0;
    private int totalFomo = 0;
    private int totalDoomScrolling = 0;

    // Max values
    private int maxAnxiety = 100;
    private int maxDistrust = 100;
    private int maxLoneliness = 100;
    private int maxFomo = 100;
    private int maxDoomScrolling = 100;

    // Factor values
    private float factorAnxiety = 0.01F;
    private float factorDistrust = 0.01F;
    private float factorLoneliness = 0.01F;
    private float factorFomo = 0.01F;
    private float factorDoomScrolling = 0.01F;

    public GameObject anxietySpike;
    public GameObject distrustSpike;
    public GameObject lonelinessSpike;
    public GameObject fomoSpike;
    public GameObject doomScrollingSpike;

    public void AddValues(int anxiety, int distrust, int loneliness, int fomo, int doomScrolling) {
        totalAnxiety += anxiety;
        totalDistrust += distrust;
        totalLoneliness += loneliness;
        totalFomo += fomo;
        totalDoomScrolling += doomScrolling;

        this.anxiety = totalAnxiety * factorAnxiety;
        this.distrust = totalDistrust * factorDistrust;
        this.loneliness = totalLoneliness * factorLoneliness;
        this.fomo = totalFomo * factorFomo;
        this.doomScrolling = totalDoomScrolling * factorDoomScrolling;

        if (this.anxiety >= maxAnxiety || this.distrust >= maxDistrust || this.loneliness >= maxLoneliness || this.fomo >= maxFomo || this.doomScrolling >= maxDoomScrolling) {
            // Lost game
            Debug.Log("You lost the game");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        factorAnxiety = (maxAnxiety - totalAnxiety) / 100;
        factorDistrust = (maxDistrust - totalDistrust) / 100;
        factorLoneliness = (maxLoneliness - totalLoneliness) / 100;
        factorFomo = (maxFomo - totalFomo) / 100;
        factorDoomScrolling = (maxDoomScrolling - totalDoomScrolling) / 100;        
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
