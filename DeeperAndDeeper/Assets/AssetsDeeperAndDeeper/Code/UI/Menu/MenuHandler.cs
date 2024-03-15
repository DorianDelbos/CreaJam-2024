using System.Collections.Generic;
using UnityEngine;

public abstract class MenuHandler : MonoBehaviour
{
    [SerializeField] private List<MenuItem> items = new List<MenuItem>();

    protected void CloseAll()
    {
        foreach (var item in items)
        {
            item.ToggleMenu(false);
        }
    }
}
