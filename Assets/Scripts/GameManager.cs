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
    public float maxAnxiety = 100;
    public float maxDistrust = 100;
    public float maxLoneliness = 100;
    public float maxFomo = 100;
    public float maxDoomScrolling = 100;

    // Factor values
    private float factorAnxiety = 0.01f;
    private float factorDistrust = 0.01f;
    private float factorLoneliness = 0.01f;
    private float factorFomo = 0.01f;
    private float factorDoomScrolling = 0.01f;

    [Header("Sprites for start and final screen")]
    public GameObject defeatAnxiety;
    public GameObject defeatFomo;
    public GameObject defeatLoneliness;
    public GameObject defeatDistrust;
    public GameObject defeatText;
    public GameObject defeatDoomScroller;

    public GameObject StartUI;
    public GameObject EndUI;

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



    public void StartGame()
    {
        Debug.Log("Button pressed");
        StartUI.SetActive(false);
        Spawnmanager.Instance.StartSpawning();
        
    }

    public void RestartGame()
    {
        // reset initial values
        defeatAnxiety.SetActive(false);
        defeatLoneliness.SetActive(false);
        defeatDistrust.SetActive(false);
        defeatFomo.SetActive(false);
        defeatText.SetActive(false);
        defeatDoomScroller.SetActive(false);

        EndUI.SetActive(false);

        anxiety = 0;
        distrust = 0;
        loneliness = 0;
        fomo = 0;
        doomScrolling = 0;

        // Initial values
        totalAnxiety = 0;
        totalDistrust = 0;
        totalLoneliness = 0;
        totalFomo = 0;
        totalDoomScrolling = 0;

        Spawnmanager.Instance.StartSpawning();

        // Todo: Remove old bubbles
        

}

    public void AddValues(float anxietyBubble, float distrustBubble, float lonelinessBubble, float fomoBubble, float doomScrollingBubble) {
        totalAnxiety += anxietyBubble;
        totalDistrust += distrustBubble;
        totalLoneliness += lonelinessBubble;
        totalFomo += fomoBubble;
        totalDoomScrolling += doomScrollingBubble;

        anxiety = totalAnxiety / maxAnxiety;// factorAnxiety;
        distrust = totalDistrust / maxDistrust; //  * factorDistrust;
        loneliness = totalLoneliness / maxLoneliness; // * factorLoneliness;
        fomo = totalFomo / maxFomo; // * factorFomo;
        doomScrolling = totalDoomScrolling / maxDoomScrolling; // * factorDoomScrolling;

        /*
        if (anxiety >= maxAnxiety || distrust >= maxDistrust || loneliness >= maxLoneliness || fomo >= maxFomo || this.doomScrolling >= maxDoomScrolling) {
            // Lost game
            Debug.Log("You lost the game");
        }
        */

        if ((anxiety >= 1f) || (distrust >= 1f) || (loneliness >= 1f) || (fomo >= 1f) || (doomScrolling >= 1f))
        {
            GameOver();
        }

    }

    public void GameOver()
    {
        Debug.Log("Gameover");
        Spawnmanager.Instance.spawningActive = false;
        var bubbleList = new List<Bubble>(FindObjectsOfType<Bubble>());
        for (int k = 0; k < bubbleList.Count; k++)
        {
            bubbleList[k].DestroyWithEffect();
        }
        EndUI.SetActive(true);

        defeatAnxiety.SetActive(false);
        defeatLoneliness.SetActive(false);
        defeatDistrust.SetActive(false);
        defeatFomo.SetActive(false);



        if (anxiety >= 1)
        {
            Debug.Log("gameover because of anxiety");
            defeatText.SetActive(true);
            defeatAnxiety.SetActive(true);
            return;
        }
        if (distrust >= 1)
        {
            Debug.Log("gameover because of distrust");
            defeatText.SetActive(true);
            defeatDistrust.SetActive(true);
            return;
        }
        if (loneliness >= 1)
        {
            Debug.Log("gameover because of loneliness");
            defeatText.SetActive(true);
            defeatLoneliness.SetActive(true);
            return;
        }
        if (fomo >= 1)
        {
            Debug.Log("gameover because of fomo");
            defeatText.SetActive(true);
            defeatFomo.SetActive(true);
            return;
        }
        if (doomScrolling >= 1)
        {
            Debug.Log("gameover because of doomScroller");
            defeatText.SetActive(false);
            defeatDoomScroller.SetActive(true);
            return;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        StartUI.SetActive(true);
        EndUI.SetActive(false);
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
