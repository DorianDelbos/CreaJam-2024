using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SteeringWing : MonoBehaviour, ISelection
{
    enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD,

    }
    Vector3 rotationZ = Vector3.zero;

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

    [SerializeField] private RectTransform arrowImage;
    [SerializeField] private RectTransform indicatorImage;
    [SerializeField] private List<Sprite> images;
    private Difficulty difficulty = Difficulty.HARD;
    private int[] angleDifficulty = new int[3] { 90, 60, 30 };
    private Coroutine rotationIndicatorRoutine;
    private float indicatorRotationZ;
    private float speed = 2f;

    private float TimeBeforeWin = 10f;
    private float TimeGood = 0f;
    
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

            // Nique la POO sorry je fais n'importe quoi <3

            #region SUB_GAME1
            TimeGood += Time.deltaTime;
            if(TimeGood > TimeBeforeWin)
            {
                Win();
            }
            if (rotationIndicatorRoutine == null) 
                StartCoroutine(RotateIndicator());
            #endregion
        }
        else
        {
            (this as ISelection).Update();
        }
    }

    IEnumerator RotateIndicator()
    {
        float duringTime = Random.Range(0.5f, 1.5f);
        float currentTime = 0;
        bool isRight = Random.value < 0.5;
        float angleClamp = 90f - angleDifficulty[(int)difficulty] / 2f;

        while (currentTime < duringTime /*&& !CheckArrowInIndicator()*/)
        {
            float dt = Time.deltaTime;
            currentTime += dt;
            indicatorRotationZ = Mathf.Clamp(indicatorRotationZ + dt * speed * (isRight ? -1 : 1), -angleClamp, angleClamp);
            indicatorImage.localEulerAngles = Vector3.forward * indicatorRotationZ;

            yield return null;
        }

        if (CheckArrowInIndicator())
        {
            Lose();
           
        }

        rotationIndicatorRoutine = null;
    }

    private bool CheckArrowInIndicator()
    {
        return Quaternion.Angle(arrowImage.rotation, indicatorImage.rotation) > angleDifficulty[(int)difficulty] / 2f;
    }

    void Lose()
    {
        arrowImage.rotation = Quaternion.Euler(Vector3.zero);
        indicatorImage.rotation = Quaternion.Euler(Vector3.zero);
        indicatorRotationZ = 0;
        rotationZ = Vector3.zero;
        if (difficulty != Difficulty.EASY)
        {
            difficulty--;
        }

        indicatorImage.GetComponent<Image>().sprite = images[(int)difficulty];
        TimeGood = 0f;
    }

    void Win()
    {
        SceneManager.LoadScene("Trauma2");
    }
}
