using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform doorTransform;
    [SerializeField] private bool inverse = false;
    [SerializeField, Range(0.1f, 5f)] private float timeToOpen = 1f;
    [SerializeField, Range(0.1f, 5f)] private float timeToClose = 1f;
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
        movementRoutine = StartCoroutine(RotateDoor(Vector3.up * 105 * (inverse ? -1 : 1), timeToOpen));

    }

    public void GoToCredits()
    {
        StartCoroutine(GoCredits());
    }
    public void Close()
    {
        if (!isOpen)
            return;

        if (movementRoutine != null)
            StopCoroutine(movementRoutine);

        isOpen = true;
        movementRoutine = StartCoroutine(RotateDoor(Vector3.zero, timeToClose));
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

    IEnumerator GoCredits()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Credits");
    }
}
