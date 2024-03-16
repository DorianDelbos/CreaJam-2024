using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

public class CameraController : MonoBehaviour
{
    Vector2 turn = Vector3.zero;
    [Header("1st game")]
    [SerializeField] float rotationSpeed = 2;
    [SerializeField] float xMaxRot = 45;
    [SerializeField] float yMaxRot = 20;

    [Header("2nd game")]
    [SerializeField] float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameManager.instance.GameState)
        {
            case GameManager.State.IN_GAME1:
                RotationBehaviour();
                break;
            case GameManager.State.IN_GAME2:
                RotationBehaviour();
                MovementBehaviour();
                break;
        }

    }

    private void RotationBehaviour()
    {
        turn.x += Input.GetAxisRaw("Mouse X") * rotationSpeed;
        turn.y += Input.GetAxisRaw("Mouse Y") * rotationSpeed;
        turn.y = Mathf.Clamp(turn.y, -yMaxRot, yMaxRot);
        turn.x = Mathf.Clamp(turn.x, -xMaxRot, xMaxRot);
        Quaternion targetRotation = Quaternion.Euler(-turn.y, turn.x, 0.0f);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 0.2f);
    }

    private void MovementBehaviour()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.position += this.transform.forward * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= this.transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += this.transform.right * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= this.transform.right * Time.deltaTime * speed;
        }
    }
}
