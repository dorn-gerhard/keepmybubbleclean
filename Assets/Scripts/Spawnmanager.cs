using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using shuffle_list;
using UnityEngine.UI;

public class Spawnmanager : MonoBehaviour
{

    public float spawnTime = 3.0f;
    public Bubble bubble; // gameobject without values
    public List<BubbleScriptableObject> bubbleContent = new List<BubbleScriptableObject>();
    public List<BubbleScriptableObject> listForSpawning = new List<BubbleScriptableObject>();
    public int spawnedBubbles = 0;
    public bool spawningActive;
    public int numberOfImages;

    public RectTransform spawnArea;
    public static Spawnmanager Instance;

    public Slider anxietySlider;
    public Slider distrustSlider;
    public Slider lonelinessSlider;
    public Slider fomoSlider;

    public RenderTexture rt;
    public Camera camera;

    public bool showStats;

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

    void Start()
    {
        spawnArea = this.GetComponent<RectTransform>();
        bubbleContent.Shuffle();
        numberOfImages = bubbleContent.Count;

        // Set renderTexture and Camera Settings
        rt = new RenderTexture(324, 324, 32, RenderTextureFormat.ARGB32);
        rt.filterMode = FilterMode.Point;
        rt.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D32_SFloat;

        camera.targetTexture = rt;

        //StartSpawning();

    }

    public void StartSpawning()
    {
        listForSpawning = bubbleContent.ToList();
        listForSpawning.Shuffle();
        spawnedBubbles = 0;
        spawningActive = true;
        StartCoroutine(SpawnCounter(spawnTime));
    }


    public IEnumerator SpawnCounter(float waitTime)
    {
        while (spawningActive && spawnedBubbles < numberOfImages)
        {
            Spawn();
            yield return new WaitForSeconds(waitTime);
        }
    }
  

    public void Spawn()
    {
        // choose new position
        float xPos = UnityEngine.Random.Range(spawnArea.offsetMin.x, spawnArea.offsetMax.x);
        float yPos = UnityEngine.Random.Range(spawnArea.offsetMin.y, spawnArea.offsetMax.y);
        // Debug.Log($"spawnArea: {spawnArea.offsetMin.x}");
       
        Bubble newBubble =  Instantiate(bubble,new Vector2(xPos,yPos), new Quaternion());

        // Assign data:
        var bubbleCont = listForSpawning[0];
        listForSpawning.RemoveAt(0);
        newBubble.bubbleData = bubbleCont;

        spawnedBubbles += 1;
        if (spawnedBubbles % 10 == 0)
        {
            spawnTime -= 0.1f;
        }
        // Add Stats image
        anxietySlider.value = Math.Clamp(bubbleCont.anxiety, -5, 5);
        distrustSlider.value = Math.Clamp(bubbleCont.distrust, -5, 5);
        lonelinessSlider.value = Math.Clamp(bubbleCont.loneliness, -5, 5);
        fomoSlider.value = Math.Clamp(bubbleCont.fomo, -5, 5);

        Texture2D statImage = new Texture2D(rt.width, rt.height);
        RenderTexture.active = rt;

        statImage.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        statImage.Apply();
        newBubble.statImage = statImage;
    }


    

}

namespace shuffle_list
{
    static class ExtensionsClass
    {
        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new System.Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random rng)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (rng == null) throw new ArgumentNullException(nameof(rng));

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, System.Random rng)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}