using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    Vector2 turn = Vector2.zero;
    [SerializeField] float speed;
    CharacterController cc;
    float xRot = 0;
    [SerializeField] float rotationSpeed = 0;
    private Vector3 movement;
    public bool hasKey = false;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        GameManager gm = GameManager.instance;

        //----------------IN GAME------------------//
        if (gm.GameState == GameManager.State.IN_GAME1 || gm.GameState == GameManager.State.IN_GAME2 || gm.GameState == GameManager.State.SUB_GAME_2 || gm.GameState == GameManager.State.IN_GAME3)
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

        if (gm.GameState == GameManager.State.IN_GAME2 ||
            gm.GameState == GameManager.State.SUB_GAME_2 ||
            gm.GameState == GameManager.State.IN_GAME3 ||
            gm.GameState == GameManager.State.SUB_GAME_3)
        {
            MovementHandle();
            GravityHandle();
            RotationHandle();
            ApplyMovement();
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
            ISelection.lerpTime = 0;
            ISelection.selectedObjet.OnUnselect();
        }

    }
    private void MovementHandle()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movement += this.transform.forward * Time.deltaTime * speed;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement -= this.transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement += this.transform.right * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement -= this.transform.right * Time.deltaTime * speed;
        }
    }

    private void RotationHandle()
    {
        xRot += Input.GetAxisRaw("Mouse X") * rotationSpeed;
        Quaternion targetRotation = Quaternion.Euler(0, xRot, 0.0f);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 0.2f);
    }

    private void GravityHandle()
    {
        float previousYVelocity = movement.y;
        movement.y += -9.81f * Time.deltaTime;
        movement.y = Mathf.Max((previousYVelocity + movement.y) * 0.5f, -20f);
    }

    private void ApplyMovement()
    {
        cc.Move(movement);
        movement = Vector3.zero;
    }
}
