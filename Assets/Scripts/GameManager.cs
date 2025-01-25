using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float totalAnxiety = 0;
    public float totalDistrust = 0;
    public float totalLoneliness = 0;
    public float totalFomo = 0;
    public float totalDoomScrolling = 0;

    // Max values
    private float maxAnxiety = 100;
    private float maxDistrust = 100;
    private float maxLoneliness = 100;
    private float maxFomo = 100;
    private float maxDoomScrolling = 100;

    // Factor values
    private float factorAnxiety = 0.01f;
    private float factorDistrust = 0.01f;
    private float factorLoneliness = 0.01f;
    private float factorFomo = 0.01f;
    private float factorDoomScrolling = 0.01f;

    public GameObject anxietySpike;
    public GameObject distrustSpike;
    public GameObject lonelinessSpike;
    public GameObject fomoSpike;
    public GameObject doomScrollingSpike;

    public static GameManager Instance;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddValues(float anxiety, float distrust, float loneliness, float fomo, float doomScrolling) {
        totalAnxiety += anxiety;
        totalDistrust += distrust;
        totalLoneliness += loneliness;
        totalFomo += fomo;
        totalDoomScrolling += doomScrolling;

        this.anxiety = totalAnxiety / maxAnxiety;// factorAnxiety;
        this.distrust = totalDistrust / maxDistrust; //  * factorDistrust;
        this.loneliness = totalLoneliness / maxLoneliness; // * factorLoneliness;
        this.fomo = totalFomo / maxFomo; // * factorFomo;
        this.doomScrolling = totalDoomScrolling / maxDoomScrolling; // * factorDoomScrolling;

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
