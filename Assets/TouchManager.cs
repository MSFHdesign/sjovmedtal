using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    void Update()
    {
        for(int i = 0; Input.touchCount > i; i++)
        {
            Touch touch = Input.GetTouch(i);
            if(touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch Began");

                touch
            }
            if(touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch Moved");
            }
            if(touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("Touch Stationary");
            }
            if(touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch Ended");
            }
            if(touch.phase == TouchPhase.Canceled)
            {
                Debug.Log("Touch Canceled");
            }
        }
    }
}
