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

    [SerializeField][Range(0f, 1f)] public float fomo;
    [SerializeField][Range(0f, 1f)] public float distrust;
    [SerializeField][Range(0f, 1f)] public float anxiety;
    [SerializeField][Range(0f, 1f)] public float loneliness;

    public void PlayBGTheme()
    {
        // RuntimeManager.PlayOneShot(BGTheme);
        instance = RuntimeManager.CreateInstance(BGTheme);
        instance.start();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBGTheme();
    }

    // Update is called once per frame
    void Update()
    {
        instance.setParameterByName("FOMO", fomo * 100);
        instance.setParameterByName("Disstrust", distrust * 100);
        instance.setParameterByName("Loneliness", anxiety * 100);
        instance.setParameterByName("Anxiety", loneliness * 100);
    }
}
