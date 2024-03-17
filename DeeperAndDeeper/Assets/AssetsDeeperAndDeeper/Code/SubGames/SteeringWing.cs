using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SteeringWing : MonoBehaviour, ISelection
{
    enum Difficulty
    {
        HARD,
        MEDIUM,
        EASY
        
    }
    Vector3 rotationZ = Vector3.zero;

    private bool isHover = false;
    private bool isSelected = false;
    private Vector3 basePos;
    private Quaternion baseRot;
    [SerializeField] RectTransform arrowImage;
    [SerializeField] RectTransform indicatorImage;
    [SerializeField] List<Image> images;
    Difficulty difficulty = Difficulty.HARD;
    int[] angleDifficulty = new int[3] { 15, 30, 45 };
    float offsetCamera = 0;
    public bool IsHover { get => isHover; set => isHover = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Quaternion BaseRot { get => baseRot; set => baseRot = value; }
    public float OffsetCamera { get => offsetCamera; set => offsetCamera = value; }

    bool isRotating = false;
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
            if (Input.GetKey(KeyCode.D))
            {
                rotationZ += new Vector3(0, 0, 1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rotationZ -= new Vector3(0, 0, 1);
                
            }
            rotationZ.z = Mathf.Clamp(rotationZ.z, -90, 90);
            this.transform.localEulerAngles = rotationZ;
            arrowImage.transform.eulerAngles = -rotationZ;
            #endregion

            // Nique le POO sorry je fais n'importe quoi <3

            #region SUB_GAME1

            if (!isRotating) StartCoroutine(RotateIndicator());
            if(Quaternion.Angle(arrowImage.rotation, indicatorImage.rotation) > angleDifficulty[(int)difficulty]) 
            {
            }
            else
            {
            }
            #endregion
        }
        else
        {
            (this as ISelection).Update();
        }
    }

    IEnumerator RotateIndicator()
    {
        isRotating = true;
        float duringTime = Random.Range(0.5f, 1.5f);
        float currentTime = 0;
        bool isRight = Random.value < 0.5;
        Vector3 currentRotIndicator = indicatorImage.transform.localEulerAngles;
        if (currentRotIndicator.z > 180)
        {
            currentRotIndicator.z = 180 - currentRotIndicator.z;
        }
        while (currentTime < duringTime)
        {
            currentTime += Time.deltaTime;
            if (isRight)
                currentRotIndicator -= new Vector3(0,0,0.5f);
            else
                currentRotIndicator += new Vector3(0, 0, 0.5f);
            Debug.Log(currentRotIndicator.z);
            currentRotIndicator.z = Mathf.Clamp(currentRotIndicator.z, -70, 70);
            indicatorImage.transform.localEulerAngles = currentRotIndicator;
            yield return null;
        }
        isRotating = false;
    }

    void Lose()
    {
        arrowImage.rotation = Quaternion.Euler(Vector3.zero);
        indicatorImage.rotation = Quaternion.Euler(Vector3.zero);
        if(difficulty != Difficulty.EASY)
        {
            difficulty--;
        }
        // indicatorImage.GetComponent<Image>(). = images[(int)difficulty]
        // TODO : change source image difficulty
    }
}
