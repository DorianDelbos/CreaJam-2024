using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarCameraController : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    Vector2 turn = Vector3.zero;
    [SerializeField] float rotationSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += direction.x * Time.deltaTime * rotationSpeed;
        turn.y += direction.y * Time.deltaTime * rotationSpeed;
        turn.y = Mathf.Clamp(turn.y, -20, 20);
        turn.x = Mathf.Clamp(turn.x, -90, 90);
        Quaternion targetRotation = Quaternion.Euler(-turn.y, turn.x, 0.0f);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 0.2f);
    }

    public void ReadDirection(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }
}
