using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
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

    public Transform tiroir;
    private Coroutine moveRoutine;
    private bool isOpen = false;

    public void OnClick()
    {
        Debug.Log(FindObjectOfType<PlayerInputHandler>().hasKey);
        if (FindObjectOfType<PlayerInputHandler>().hasKey)
        {
            if (moveRoutine != null)
                StopCoroutine(moveRoutine);

            moveRoutine = StartCoroutine(Move(new Vector3(isOpen ? 0.0f : -0.5f, 0f, 0f)));

            isOpen = !isOpen;
        }
    }

    private void Update()
    {
        (this as ISelection).Update();
    }

    private IEnumerator Move(Vector3 to, float timer = 1f)
    {
        float elapsed = 0f;
        Vector3 from = tiroir.localPosition;

        while (elapsed < timer)
        {
            elapsed = Mathf.Min(elapsed + Time.deltaTime, timer);
            float factor = elapsed / timer;

            tiroir.localPosition = Vector3.Lerp(from, to, factor);

            yield return null;
        }
    }
}
