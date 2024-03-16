using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScreenRight : MonoBehaviour, ISelection
{
    private bool isHover = false;
    private bool isSelected = false;
    private Vector3 basePos;
    private Quaternion baseRot;
    float offsetCamera = 0;
    public bool IsHover { get => isHover; set => isHover = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Quaternion BaseRot { get => baseRot; set => baseRot = value; }
    public float OffsetCamera { get => offsetCamera; set => offsetCamera = value; }

    public void OnClick()
    {
        InteriorVehicle vehicle = GameObject.FindFirstObjectByType<InteriorVehicle>();
        vehicle.ToggleSunScreenRight();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        (this as ISelection).Update();
    }
}
