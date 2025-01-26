using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject selectedObject;
    private Vector3 lastMousePosition;
    public float movementThreshold = 0.07f;
    public void OnTouch()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
        selectedObject = targetObject.gameObject;

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 offset = new Vector3();
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                var bubble = targetObject?.GetComponent<Bubble>();
                if ((bubble) &&
                    ((bubble.state == BubbleState.FLOATING || (bubble.state == BubbleState.STICKY))))
                {
                    // Debug.Log("bubble selected");
                    bubble.isDragged = true;
                    selectedObject = targetObject.transform.gameObject;
                    offset = selectedObject.transform.position - mousePosition;
                }
            }
        }

        if (selectedObject)
        {
            float distanceMoved = Vector3.Distance(mousePosition, lastMousePosition);
            // Debug.Log("moved this much in world space: " + distanceMoved + " left: " + selectedObject.transform.position.x);

            var leftOfMindBubble = selectedObject.transform.position.x < 0;
            var movingToLeft = mousePosition.x < lastMousePosition.x;
            var dropQuickly = (leftOfMindBubble && movingToLeft) || (!leftOfMindBubble && !movingToLeft);

            selectedObject.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, selectedObject.transform.position.z);

            if (
                Input.GetMouseButtonUp(0) || 
                (distanceMoved > movementThreshold && dropQuickly) || !selectedObject.GetComponent<Bubble>().isDraggable
            )
            {
                // Debug.Log("bubble released");
                selectedObject.GetComponent<Bubble>().isDragged = false;
                selectedObject = null;

            }
        }

        lastMousePosition = mousePosition;

    }

    Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in screen space
        Vector3 screenPosition = Input.mousePosition;

        // Convert the screen position to world space
        // Set the z-distance based on the camera's distance to the objects you're interacting with
        screenPosition.z = Mathf.Abs(Camera.main.transform.position.z); // Adjust as needed
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
}
