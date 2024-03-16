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
    public float offsetCamera = 0;
    public bool IsHover { get => isHover; set => isHover = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Quaternion BaseRot { get => baseRot; set => baseRot = value; }
    public float OffsetCamera { get => offsetCamera; set => offsetCamera = value; }

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
    }
}
