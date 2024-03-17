using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSub2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        SceneManager.LoadScene("Trauma2");
    }
}
