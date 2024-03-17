using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Desk : MonoBehaviour, ISelection
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

    public GameObject subScene1;
    public GameObject subScene2;
    public PlayableDirector seq;

    public void OnClick()
    {
        if (FindObjectOfType<PlayerInputHandler>().hasKey)
        {
            subScene1.SetActive(false);
            subScene2.SetActive(true);
            seq.Play();
        }
    }

    private void Update()
    {
        (this as ISelection).Update();
    }
}
