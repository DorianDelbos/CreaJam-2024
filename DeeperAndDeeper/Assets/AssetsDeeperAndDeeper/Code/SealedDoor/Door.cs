using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform doorTransform;
    private bool isOpen = false;
    private Coroutine movementRoutine;

    public void ToggleDoor()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    public void Open()
    {
        if (isOpen)
            return;

        if (movementRoutine != null)
            StopCoroutine(movementRoutine);

        isOpen = false;
        movementRoutine = StartCoroutine(RotateDoor(Vector3.up * 105, 5f));
    }

    public void Close()
    {
        if (!isOpen)
            return;

        if (movementRoutine != null)
            StopCoroutine(movementRoutine);

        isOpen = true;
        movementRoutine = StartCoroutine(RotateDoor(Vector3.zero));
    }

    private IEnumerator RotateDoor(Vector3 to, float timer = 1f)
    {
        float elapsed = 0f;
        Vector3 from = doorTransform.localEulerAngles;

        while (elapsed < timer)
        {
            elapsed = Mathf.Min(elapsed + Time.deltaTime, timer);
            float factor = elapsed / timer;

            doorTransform.localEulerAngles = Vector3.Lerp(from, to, factor);

            yield return null;
        }
    }
}
