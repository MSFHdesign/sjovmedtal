using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    // Save finger id for first two fingers
    // Check for those ids when checking all touch inputs
    // Direction between two positions = rotation start
    // Direction between two positions = rotation difference - start
    // Vector2.Angle(start, difference)

    void Update()
    {
        for(int i = 0; Input.touchCount > i; i++)
        {
            Touch touch = Input.GetTouch(i);
            if(touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch Began");
                
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
            if (touch.phase == TouchPhase.Canceled) ;
            {
                Debug.Log("Touch Canceled");
            }
        }
    }
}
