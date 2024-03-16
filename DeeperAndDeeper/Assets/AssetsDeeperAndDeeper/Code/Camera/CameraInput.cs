using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    PlayerInputMap inputMap;
    CarCameraController controller;

    private void OnEnable()
    {
        inputMap.Enable();
    }
    private void OnDisable()
    {
        inputMap.Disable();
    }
    private void Awake()
    {
        inputMap = new PlayerInputMap();
        controller = GetComponent<CarCameraController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
