using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public bool isObjectSelected { get; private set; } = false; // Add public boolean to check if an object is selected
    private Transform selectedObject;
    private Vector3 offset;

    public Material selectedMaterial;
    public Material overDeleteZoneMaterial;

    private bool isOverDeleteZone = false;
    private Material defaultMaterial;

    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }
        else if (Input.GetMouseButton(0))
        {
            HandleMouseDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
    }

    private void HandleMouseDown()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.transform.CompareTag("SpawnedObject"))
        {
            if (selectedObject != null && selectedObject != hit.transform)
            {
                SetObjectMaterial(selectedObject, defaultMaterial);
            }

            selectedObject = hit.transform;
            isObjectSelected = true;
            offset = selectedObject.position - (Vector3)mousePosition;

            defaultMaterial = GetObjectMaterial(selectedObject);
            SetObjectMaterial(selectedObject, selectedMaterial);

            Debug.Log($"Object selected: {selectedObject.name}");
        }
    }

    private void HandleMouseDrag()
    {
        if (isObjectSelected && selectedObject != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = (Vector3)mousePosition + offset;
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

    private void HandleMouseUp()
    {
        if (isOverDeleteZone && selectedObject != null)
        {
            Debug.Log($"Object {selectedObject.name} is over delete zone, destroying.");
            Destroy(selectedObject.gameObject);
        }

        if (selectedObject != null)
        {
            SetObjectMaterial(selectedObject, defaultMaterial);
            selectedObject = null;
            isObjectSelected = false;
            isOverDeleteZone = false;
            Debug.Log("Mouse released, deselecting object.");
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
        Debug.Log($"SetObjectInDeleteZone called with isInZone: {isInZone}");

        if (selectedObject != null)
        {
            SetObjectMaterial(selectedObject, isOverDeleteZone ? overDeleteZoneMaterial : selectedMaterial);
            Debug.Log($"SetObjectInDeleteZone: {isInZone} for {selectedObject.name}");
        }
        else
        {
            Debug.Log("No object is currently selected.");
        }
    }
}
