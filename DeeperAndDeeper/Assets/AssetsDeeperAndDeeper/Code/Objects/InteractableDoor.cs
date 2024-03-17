using UnityEngine;

public class InteractableDoor : MonoBehaviour, ISelection
{
    #region ISelection
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
    #endregion

    [SerializeField] private Door[] doors;

    public void OnClick()
    {
        foreach (Door door in doors)
        {
            door.ToggleDoor();
        }
    }

    void Update()
    {
        (this as ISelection).Update();
    }
}
