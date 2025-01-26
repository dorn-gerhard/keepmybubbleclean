using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private EventInstance instance;
    [SerializeField] EventReference BGTheme;
    [SerializeField] EventReference Bubble;
    [SerializeField] EventReference Burst;
    [SerializeField] EventReference Eat;
    [SerializeField] EventReference Scrub;
    [SerializeField] EventReference GameOver;

    [SerializeField][Range(0f, 100f)] private float fomo;
    [SerializeField][Range(0f, 100f)] private float distrust;
    [SerializeField][Range(0f, 100f)] private float anxiety;
    [SerializeField][Range(0f, 100f)] private float loneliness;

    public static AudioManager Instance;

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

    public void PlayBGTheme()
    {
        // RuntimeManager.PlayOneShot(BGTheme);
        instance = RuntimeManager.CreateInstance(BGTheme);
        instance.start();
    }

    public void PlayBubble()
    {
        RuntimeManager.PlayOneShot(Bubble);
    }

    public void PlayBurst()
    {
        RuntimeManager.PlayOneShot(Burst);
    }

    public void PlayEat()
    {
        RuntimeManager.PlayOneShot(Eat);
    }

    public void PlayScrub()
    {
        RuntimeManager.PlayOneShot(Scrub);
    }

    public void PlayGameOver()
    {
        RuntimeManager.PlayOneShot(GameOver);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBGTheme();
    }

    // Update is called once per frame
    void Update()
    {
        anxiety = Mathf.Clamp(GameManager.Instance.anxiety, 0, 1) * 100;
        distrust = Mathf.Clamp(GameManager.Instance.distrust, 0, 1) * 100;
        loneliness = Mathf.Clamp(GameManager.Instance.loneliness, 0, 1) * 100;
        fomo = Mathf.Clamp(GameManager.Instance.fomo, 0, 1) * 100;
    
        instance.setParameterByName("FOMO", fomo);
        instance.setParameterByName("Disstrust", distrust);
        instance.setParameterByName("Loneliness", anxiety);
        instance.setParameterByName("Anxiety", loneliness);
    }
}
