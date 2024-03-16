using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarCameraController : MonoBehaviour
{
    Vector2 turn = Vector3.zero;
    [SerializeField] float rotationSpeed = 2;
    [SerializeField] float xMaxRot = 45;
    [SerializeField] float yMaxRot = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GameState == GameManager.State.INGAME)
        {
            turn.x += Input.GetAxisRaw("Mouse X") * rotationSpeed;
            turn.y += Input.GetAxisRaw("Mouse Y") * rotationSpeed;
            turn.y = Mathf.Clamp(turn.y, -yMaxRot, yMaxRot);
            turn.x = Mathf.Clamp(turn.x, -xMaxRot, xMaxRot);
            Quaternion targetRotation = Quaternion.Euler(-turn.y, turn.x, 0.0f);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 0.2f);
        }
        
    }
}
