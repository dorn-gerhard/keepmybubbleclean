using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseRotation : MonoBehaviour
{
    public float rotation;

    

    // Update is called once per frame
    void Update()
    {
        rotation = transform.rotation.ToEuler().z;
        transform.rotation = new Quaternion(0, 0, 0, 1);
    }
}
