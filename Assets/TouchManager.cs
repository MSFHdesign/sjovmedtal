using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    // Save finger id for first three fingers
    private int firstFingerId = -1;
    private int secondFingerId = -1;
    private int thirdFingerId = -1;

    // Start direction between two positions = rotation start
    private Vector2 startDirection;

    // Difference direction between two positions = rotation difference - start
    private Vector2 currentDirection;

    // Initial distance between three fingers for scaling
    private float initialDistance;

    // Reference to the object that needs to be rotated, moved, and scaled
    private Transform selectedObject;

    // Bool to check if an object is selected
    private bool isObjectSelected = false;

    // Initial offset between touch point and object position
    private Vector3 offset;

    // Materials for visual effect
    public Material selectedMaterial;
    public Material overDeleteZoneMaterial;

    private bool isOverDeleteZone = false;
    private Material defaultMaterial;

    void Update()
    {
        // Loop through all touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchBegan(touch);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                HandleTouchMoved(touch);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                HandleTouchEnded(touch);
            }
        }
    }

    // Handle touch began phase
    private void HandleTouchBegan(Touch touch)
    {
        // Check if the touch is inside the object's bounds
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("SpawnedObject"))
        {
            if (selectedObject != null && selectedObject != hit.transform)
            {
                // Revert the material of the previously selected object
                SetObjectMaterial(selectedObject, defaultMaterial);
            }

            selectedObject = hit.transform;
            isObjectSelected = true;
            offset = selectedObject.position - hit.point;

            // Store the default material from the mesh renderer
            defaultMaterial = GetObjectMaterial(selectedObject);

            // Change the material of the newly selected object
            SetObjectMaterial(selectedObject, selectedMaterial);
        }

        // Save the finger id for the first three touches
        if (firstFingerId == -1)
        {
            firstFingerId = touch.fingerId;
        }
        else if (secondFingerId == -1)
        {
            secondFingerId = touch.fingerId;

            // Calculate the initial direction vector between the first two touches
            startDirection = Input.GetTouch(firstFingerId).position - touch.position;
        }
        else if (thirdFingerId == -1)
        {
            thirdFingerId = touch.fingerId;

            // Calculate the initial distance between the three touches for scaling
            initialDistance = CalculateTrianglePerimeter(Input.GetTouch(firstFingerId).position, Input.GetTouch(secondFingerId).position, touch.position);
        }
    }

    // Handle touch moved phase
    private void HandleTouchMoved(Touch touch)
    {
        if (firstFingerId != -1 && secondFingerId != -1 && thirdFingerId == -1)
        {
            // Calculate the current direction vector between the first two touches
            currentDirection = Input.GetTouch(firstFingerId).position - Input.GetTouch(secondFingerId).position;

            // Calculate the angle difference between the start direction and the current direction
            float angleDifference = Vector2.SignedAngle(startDirection, currentDirection);

            // Apply rotation to the object around the z-axis
            if (selectedObject != null)
            {
                selectedObject.Rotate(Vector3.forward, angleDifference);
            }

            // Update the start direction for the next frame
            startDirection = currentDirection;
        }
        else if (isObjectSelected && Input.touchCount == 1)
        {
            // Move the object with touch
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hit) && selectedObject != null)
            {
                selectedObject.position = hit.point + offset;
            }
        }
        else if (firstFingerId != -1 && secondFingerId != -1 && thirdFingerId != -1)
        {
            // Calculate the current distance between the three touches for scaling
            float currentDistance = CalculateTrianglePerimeter(Input.GetTouch(firstFingerId).position, Input.GetTouch(secondFingerId).position, Input.GetTouch(thirdFingerId).position);

            // Calculate the scale factor
            float scaleFactor = currentDistance / initialDistance;

            // Apply scaling to the object
            if (selectedObject != null)
            {
                selectedObject.localScale *= scaleFactor;
            }

            // Update the initial distance for the next frame
            initialDistance = currentDistance;
        }
    }

    // Handle touch ended phase
    private void HandleTouchEnded(Touch touch)
    {
        // If the object is over the delete zone and touch ends, destroy the object
        if (isOverDeleteZone && selectedObject != null)
        {
            Destroy(selectedObject.gameObject);
        }

        // Reset the finger ids when the touch ends or is canceled
        if (touch.fingerId == firstFingerId)
        {
            firstFingerId = -1;
        }
        else if (touch.fingerId == secondFingerId)
        {
            secondFingerId = -1;
        }
        else if (touch.fingerId == thirdFingerId)
        {
            thirdFingerId = -1;
        }

        // Reset start direction and initial distance if any finger is lifted
        if (firstFingerId == -1 || secondFingerId == -1)
        {
            startDirection = Vector2.zero;
        }
        if (thirdFingerId == -1)
        {
            initialDistance = 0f;
        }

        // Deselect the object if no more touches are present
        if (Input.touchCount == 0 && selectedObject != null)
        {
            // Revert the material of the previously selected object
            SetObjectMaterial(selectedObject, defaultMaterial);
            selectedObject = null;
            isObjectSelected = false;
            isOverDeleteZone = false;
        }
    }

    // Set the material of the object
    private void SetObjectMaterial(Transform obj, Material material)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }

    // Get the material of the object
    private Material GetObjectMaterial(Transform obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        return renderer != null ? renderer.material : null;
    }

    // Set whether the object is in the delete zone
    public void SetObjectInDeleteZone(bool isInZone)
    {
        isOverDeleteZone = isInZone;

        if (selectedObject != null)
        {
            SetObjectMaterial(selectedObject, isOverDeleteZone ? overDeleteZoneMaterial : selectedMaterial);
        }
    }

    // Helper method to calculate the perimeter of a triangle formed by three points
    private float CalculateTrianglePerimeter(Vector2 point1, Vector2 point2, Vector2 point3)
    {
        return Vector2.Distance(point1, point2) + Vector2.Distance(point2, point3) + Vector2.Distance(point3, point1);
    }
}
