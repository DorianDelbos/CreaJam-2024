using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Vector2 turn = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        GameManager gm = GameManager.instance;

        //----------------IN GAME------------------//
        if (gm.GameState == GameManager.State.INGAME)
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
            gm.ChangeState(GameManager.State.INGAME);
            ISelection.lerpTime = 0;
        }
    }
}
