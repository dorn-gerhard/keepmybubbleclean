using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using shuffle_list;

public class Spawnmanager : MonoBehaviour
{

    public float spawnTime = 3.0f;
    public Bubble bubble; // gameobject without values
    public List<BubbleScriptableObject> bubbleContent = new List<BubbleScriptableObject>();
    public bool spawnNow;
    public int spawnedBubbles = 0;

    public RectTransform spawnArea;
    // Start is called before the first frame update
    void Start()
    {
        spawnArea = this.GetComponent<RectTransform>();
        bubbleContent.Shuffle();

        StartCoroutine(SpawnCounter(spawnTime));
            

    }
    private void OnValidate()
    {
        if (spawnNow)
        {
            Spawn();
            spawnNow = false;
        }
    }

    public IEnumerator SpawnCounter(float waitTime)
    {
        while (true && spawnedBubbles < 20)
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
        var bubbleCont = bubbleContent[0];
        bubbleContent.RemoveAt(0);
        newBubble.bubbleData = bubbleCont;
        spawnedBubbles += 1;
        if (spawnedBubbles % 10 == 0)
        {
            spawnTime -= 0.1f;
        }

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