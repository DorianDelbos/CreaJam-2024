using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Vector2 turn = Vector2.zero;
    [SerializeField] float speed;
    CharacterController cc;
    float xRot = 0;
    [SerializeField] float rotationSpeed = 0;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        GameManager gm = GameManager.instance;

        //----------------IN GAME------------------//
        if (gm.GameState == GameManager.State.IN_GAME1 || gm.GameState == GameManager.State.IN_GAME2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit info))
            {
                if (info.collider.gameObject.TryGetComponent<ISelection>(out ISelection selection))
                {
                    selection.IsHover = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        selection.OnClick();
                    }

                }
            }
        }

        if (gm.GameState == GameManager.State.IN_GAME2)
        {
            MovementBehaviour();
            RotationBehaviour();
        }

        //----------------LOOKING OBJECT------------------//
        if (gm.GameState == GameManager.State.LOOKING_OBJECT && Input.GetMouseButton(0))
        {
            GameObject go = (ISelection.selectedObjet as MonoBehaviour).gameObject;
            turn.x += Input.GetAxisRaw("Mouse X") * 10;
            turn.y += Input.GetAxisRaw("Mouse Y") * 10;
            Quaternion targetRotation = Quaternion.Euler(-turn.y, -turn.x, 0.0f);
            go.transform.rotation = Quaternion.Slerp(go.transform.rotation, targetRotation, 0.2f);
        }

        if (gm.GameState == GameManager.State.LOOKING_OBJECT && Input.GetKeyDown(KeyCode.Escape))
        {
            gm.ChangeState(GameManager.State.IN_GAME1);
            ISelection.lerpTime = 0;
        }

    }
    private void MovementBehaviour()
    {
        Vector3 result = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            result += this.transform.forward * Time.deltaTime * speed;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            result -= this.transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            result += this.transform.right * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            result -= this.transform.right * Time.deltaTime * speed;
        }
        cc.Move(result);
    }

    private void RotationBehaviour()
    {
        xRot += Input.GetAxisRaw("Mouse X") * rotationSpeed;
        Quaternion targetRotation = Quaternion.Euler(0, xRot, 0.0f);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 0.2f);

    }
}
