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
        inputMap.Camera.LookAt.performed += controller.ReadDirection;
        inputMap.Camera.LookAt.canceled += controller.ReadDirection;
    }
    private void OnDisable()
    {
        inputMap.Disable();
        inputMap.Camera.LookAt.performed -= controller.ReadDirection;
        inputMap.Camera.LookAt.canceled += controller.ReadDirection;
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
