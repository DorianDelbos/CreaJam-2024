using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntitieTest : MonoBehaviour, ISelection
{
    bool isHover = false;
    bool isSelected = false;
    Vector3 basePos;
    Quaternion baseRot;
    public bool IsHover { get => isHover; set => isHover = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Quaternion BaseRot { get => baseRot; set => baseRot = value; }

    private void Start()
    {
        basePos = this.transform.position;
        baseRot = this.transform.rotation;
    }
    public void OnClick()
    {
        isSelected = true;
        GameManager.instance.ChangeState(GameManager.State.LOOKING_OBJECT);
        (this as ISelection).ResetMat();
        ISelection.selectedObjet = this;
    }

    // Update is called once per frame
    void Update()
    {
        (this as ISelection).Update();
        GameManager gm = GameManager.instance;
        if(gm.GameState == GameManager.State.INGAME)
        {
            (this as ISelection).SelectUpdateOutline();
            isHover = false;
        }
        else if(isSelected && gm.GameState == GameManager.State.LOOKING_OBJECT)
        {
            
        }
        
    }
}
