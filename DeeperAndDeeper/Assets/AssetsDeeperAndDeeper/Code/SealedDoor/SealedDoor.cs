using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SealedDoor : MonoBehaviour, ISelection
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
    bool isActive = false;
    bool isGenerated = false;
    int[] password = new int[3];
    int[] givenPassword = new int[3] { -1, -1, -1 };
    bool isValidate = false;
    public void OnClick()
    {
        isActive = true;
        if (!isGenerated) GenerateRiddle();
    }

    public void OnResolve()
    {
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void GenerateRiddle()
    {
        isGenerated = true;
        for (int i = 0; i < password.Length; i++)
        {
            password[0] = Random.Range(0, 9);
        }
    }
    // Update is called once per frame
    void Update()
    {
        (this as ISelection).Update();
        if (isActive)
        {

        }
    }

    void Validate()
    {
        for (int i = 0; i < password.Length; i++)
        {
            if (password[i] != givenPassword[i]) 
            {
                isValidate = false;
                return; 
            }
        }
        isValidate = true;
    }

    public void AddPasswordNumber(byte number)
    {
        Debug.Assert(number > 9, "Number invalid, it has to be between 0 - 9 ");
        int index = -1;
        for(byte i = 0; i < password.Length; i++) 
        {
            if (givenPassword[i] == -1)
            {
                index = i;
                break;
            }
        }
        if(index != 1)
        {
            givenPassword[index] = number;
        }
        else
        {
            return;
        }
    }

    public void RemovePasswordNumber()
    {
        int index = -1;
        for (int i = password.Length-1; i >= 0; i--)
        {
            if (givenPassword[i] != -1)
            {
                index = i;
                break;
            }
        }
        if (index != 1)
        {
            givenPassword[index] = -1;
        }
        else
        {
            return;
        }
    }
}
