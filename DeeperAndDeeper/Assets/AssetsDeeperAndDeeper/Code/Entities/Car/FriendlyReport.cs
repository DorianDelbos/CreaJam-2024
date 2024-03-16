using UnityEngine;

public class FriendlyReport : MonoBehaviour, ISelection
{
    public bool isHover = false;
    public bool isSelected = false;
    public Vector3 basePos;
    public Quaternion baseRot; 
    public float offsetCamera = 0.5f;
    public float OffsetCamera { get => offsetCamera; set => offsetCamera = value; }

    public bool IsHover { get => isHover; set => isHover = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Quaternion BaseRot { get => baseRot; set => baseRot = value; }

    public void OnClick()
    {
        isSelected = true;
        GameManager.instance.ChangeState(GameManager.State.LOOKING_OBJECT);
        (this as ISelection).ResetMat();
        ISelection.selectedObjet = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        basePos = this.transform.position;
        baseRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        (this as ISelection).Update();
    }
}
