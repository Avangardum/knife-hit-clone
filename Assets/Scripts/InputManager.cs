using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    
    public event Action Tap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Created multiple input managers");
        }
    }

    private void Update()
    {
        bool tap = false;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            tap = true;
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                tap = true;
            }
        }

        if (tap)
        {
            Tap?.Invoke();
        }
    }
}
