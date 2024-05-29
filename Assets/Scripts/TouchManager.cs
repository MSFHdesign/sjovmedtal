using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private int firstFingerId = -1;
    private int secondFingerId = -1;
    private int thirdFingerId = -1;

    private Vector2 startDirection;
    private Vector2 currentDirection;
    private float initialDistance;

    private Transform selectedObject;
    private bool isObjectSelected = false;
    private Vector3 offset;

    public Material selectedMaterial;
    public Material overDeleteZoneMaterial;

    private bool isOverDeleteZone = false;
    private Material defaultMaterial;

    void Update()
    {
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

    private void HandleTouchBegan(Touch touch)
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        if (hit.collider != null && hit.transform.CompareTag("SpawnedObject"))
        {
            if (selectedObject != null && selectedObject != hit.transform)
            {
                SetObjectMaterial(selectedObject, defaultMaterial);
            }

            selectedObject = hit.transform;
            isObjectSelected = true;
            offset = selectedObject.position - (Vector3)touchPosition;

            defaultMaterial = GetObjectMaterial(selectedObject);
            SetObjectMaterial(selectedObject, selectedMaterial);
        }

        if (firstFingerId == -1)
        {
            firstFingerId = touch.fingerId;
        }
        else if (secondFingerId == -1)
        {
            secondFingerId = touch.fingerId;
            startDirection = (Vector2)Input.GetTouch(firstFingerId).position - touch.position;
        }
        else if (thirdFingerId == -1)
        {
            thirdFingerId = touch.fingerId;
            initialDistance = CalculateTrianglePerimeter(Input.GetTouch(firstFingerId).position, Input.GetTouch(secondFingerId).position, touch.position);
        }
    }

    private void HandleTouchMoved(Touch touch)
    {
        if (firstFingerId != -1 && secondFingerId != -1 && thirdFingerId == -1)
        {
            currentDirection = (Vector2)Input.GetTouch(firstFingerId).position - (Vector2)Input.GetTouch(secondFingerId).position;
            float angleDifference = Vector2.SignedAngle(startDirection, currentDirection);

            if (selectedObject != null)
            {
                selectedObject.Rotate(Vector3.forward, angleDifference);
            }

            startDirection = currentDirection;
        }
        else if (isObjectSelected && Input.touchCount == 1)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (selectedObject != null)
            {
                Vector3 newPosition = (Vector3)touchPosition + offset;
                newPosition.z = selectedObject.position.z; // Ensure Z position remains unchanged
                Rigidbody2D rb = selectedObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.MovePosition(new Vector2(newPosition.x, newPosition.y));
                }
                else
                {
                    selectedObject.position = new Vector3(newPosition.x, newPosition.y, selectedObject.position.z);
                }
            }
        }
        else if (firstFingerId != -1 && secondFingerId != -1 && thirdFingerId != -1)
        {
            float currentDistance = CalculateTrianglePerimeter(Input.GetTouch(firstFingerId).position, Input.GetTouch(secondFingerId).position, Input.GetTouch(thirdFingerId).position);
            float scaleFactor = currentDistance / initialDistance;

            if (selectedObject != null)
            {
                selectedObject.localScale *= scaleFactor;
            }

            initialDistance = currentDistance;
        }
    }

    private void HandleTouchEnded(Touch touch)
    {
        if (isOverDeleteZone && selectedObject != null)
        {
            Destroy(selectedObject.gameObject);
        }

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

        if (firstFingerId == -1 || secondFingerId == -1)
        {
            startDirection = Vector2.zero;
        }
        if (thirdFingerId == -1)
        {
            initialDistance = 0f;
        }

        if (Input.touchCount == 0 && selectedObject != null)
        {
            SetObjectMaterial(selectedObject, defaultMaterial);
            selectedObject = null;
            isObjectSelected = false;
            isOverDeleteZone = false;
        }
    }

    private void SetObjectMaterial(Transform obj, Material material)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }

    private Material GetObjectMaterial(Transform obj)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        return renderer != null ? renderer.material : null;
    }

    public void SetObjectInDeleteZone(bool isInZone)
    {
        isOverDeleteZone = isInZone;

        if (selectedObject != null)
        {
            SetObjectMaterial(selectedObject, isOverDeleteZone ? overDeleteZoneMaterial : selectedMaterial);
        }
    }

    private float CalculateTrianglePerimeter(Vector2 point1, Vector2 point2, Vector2 point3)
    {
        return Vector2.Distance(point1, point2) + Vector2.Distance(point2, point3) + Vector2.Distance(point3, point1);
    }
}
