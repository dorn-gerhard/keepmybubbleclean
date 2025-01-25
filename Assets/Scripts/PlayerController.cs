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
        Vector3 offset = new Vector3();
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if ((targetObject is not null))
            {
                var bubble = targetObject.GetComponent<Bubble>();
                if ((bubble is not null) &
                    ((bubble.state == BubbleState.FLOATING || (bubble.state == BubbleState.STICKY))))
                {
                    Debug.Log("bubble selected");
                    selectedObject = targetObject.transform.gameObject;
                    offset = selectedObject.transform.position - mousePosition;
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;

        }
        if (selectedObject)
        {
            

            selectedObject.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, selectedObject.transform.position.z);
        }

    }
}
