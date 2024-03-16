using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SteeringWing : MonoBehaviour, ISelection
{
    Vector3 rotationZ = Vector3.zero;

    private bool isHover = false;
    private bool isSelected = false;
    private Vector3 basePos;
    private Quaternion baseRot;

    [SerializeField] RectTransform arrowImage;
    float offsetCamera = 0;
    public bool IsHover { get => isHover; set => isHover = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Quaternion BaseRot { get => baseRot; set => baseRot = value; }
    public float OffsetCamera { get => offsetCamera; set => offsetCamera = value; }

    public void OnClick()
    {
        InteriorVehicle vehicle = GameObject.FindFirstObjectByType<InteriorVehicle>();
        vehicle.ToggleSteering();
    }

    // Start is called before the first frame update
    void Start()
    {
        rotationZ = this.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GameState == GameManager.State.SUB_GAME_1)
        {
            #region Wing & Arrow Rotation
            if (Input.GetKey(KeyCode.D) && this.transform.rotation.z < 90f)
            {
                rotationZ += new Vector3(0, 0, 1);
            }
            else if (Input.GetKey(KeyCode.A) && this.transform.rotation.z > -90f)
            {
                rotationZ -= new Vector3(0, 0, 1);
                
            }
            rotationZ.z = Mathf.Clamp(rotationZ.z, -90, 90);
            this.transform.localEulerAngles = rotationZ;
            arrowImage.transform.eulerAngles = -rotationZ;
            #endregion
        }
        else
        {
            (this as ISelection).Update();
        }
    }
}
