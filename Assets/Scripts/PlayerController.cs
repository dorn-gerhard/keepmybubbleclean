using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject selectedObject;
    public void OnTouch()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
        selectedObject = targetObject.gameObject;

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;

        }
        if (selectedObject)
        {
            Vector3 offset = selectedObject.transform.position - mousePosition;

            selectedObject.transform.position = mousePosition + offset;
        }

    }
}
