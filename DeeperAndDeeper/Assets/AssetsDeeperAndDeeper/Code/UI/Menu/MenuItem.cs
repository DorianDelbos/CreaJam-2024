using UnityEngine;

public class MenuItem : MonoBehaviour
{
    public void ToggleMenu(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
