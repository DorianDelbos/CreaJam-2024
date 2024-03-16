using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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

    [SerializeField] GameObject DigitUi;
    [SerializeField] TMP_Text DigitVisualPassword;

    [SerializeField] Transform firstDigitButton;
    [SerializeField] GameObject pfDigitButton;

    [SerializeField] Transform historyLayout;
    [SerializeField] GameObject pfNumberCase;

    private void Start()
    {
        DigitUi.SetActive(true);
        DigitUi.SetActive(false);

        for (int i = 1; i < 10; i++)
        {
            int index = i;
            GameObject go = GameObject.Instantiate(pfDigitButton, firstDigitButton);
            go.GetComponentInChildren<TMP_Text>().text = i.ToString();
            go.GetComponent<Button>().onClick.AddListener(() => { AddPasswordNumber(index); });
        }
        GameObject undo = GameObject.Instantiate(pfDigitButton, firstDigitButton);
        undo.GetComponentInChildren<TMP_Text>().text = "<--";
        undo.GetComponent<Button>().onClick.AddListener(() => { RemovePasswordNumber(); });

        GameObject zero = GameObject.Instantiate(pfDigitButton, firstDigitButton);
        zero.GetComponentInChildren<TMP_Text>().text = 0.ToString();
        zero.GetComponent<Button>().onClick.AddListener(() => { AddPasswordNumber(0); });

        GameObject valid = GameObject.Instantiate(pfDigitButton, firstDigitButton);
        valid.GetComponentInChildren<TMP_Text>().text = "Valid";
        valid.GetComponent<Button>().onClick.AddListener(() => { Validate(); });
        valid.GetComponent<Image>().color = Color.green;
    }
    public void OnClick()
    {
        Cursor.lockState = CursorLockMode.None;
        isActive = true;
        if (!isGenerated) GenerateRiddle();
        DigitUi.SetActive(true);
        UpdateDisplayPassword();
        GameManager.instance.ChangeState(GameManager.State.UNLOCK_DIGIT);
    }

    private void UpdateDisplayPassword()
    {
        string displayedPassword = "";
        for (int i = 0; i < givenPassword.Length; i++)
        {
            displayedPassword += givenPassword[i] == -1 ? "_ " : givenPassword[i] + " ";
        }
        DigitVisualPassword.text = displayedPassword;
    }

    public void OnResolve()
    {
    }

    // Update is called once per frame
    void Update()
    {
        (this as ISelection).Update();
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.instance.ChangeState(GameManager.State.IN_GAME2);
                ISelection.lerpTime = 0;
                QuitDigit();
            }
        }
    }

    void GenerateRiddle()
    {
        isGenerated = true;
        for (int i = 0; i < password.Length; i++)
        {
            password[i] = UnityEngine.Random.Range(0, 9);
        }
    }

    void Validate()
    {
        if (givenPassword[^1] == -1)
            return;
        List<int> toIgnore = new List<int>();
        List<int> toCheck = new();
        isValidate = true;
        for (int i = 0; i < password.Length; i++)
        {
            toCheck.Add(givenPassword[i]);
            if (password[i] == givenPassword[i])
            {
                toIgnore.Add(i);
            }
        }

        for (int i = 0; i < password.Length; i++)
        {
            toCheck.Add(givenPassword[i]);
            if (password[i] == givenPassword[i])
            {
                GameObject resultCase = GameObject.Instantiate(pfNumberCase, historyLayout);
                resultCase.GetComponentInChildren<TMP_Text>().text = givenPassword[i].ToString();
                resultCase.GetComponent<Image>().color = Color.green;
            }
            else if (isInPassword(givenPassword[i], toIgnore))
            {
                GameObject resultCase = GameObject.Instantiate(pfNumberCase, historyLayout);
                resultCase.GetComponentInChildren<TMP_Text>().text = givenPassword[i].ToString();
                resultCase.GetComponent<Image>().color = Color.yellow;
                isValidate = false;
            }
            else
            {
                GameObject resultCase = GameObject.Instantiate(pfNumberCase, historyLayout);
                resultCase.GetComponentInChildren<TMP_Text>().text = givenPassword[i].ToString();
                resultCase.GetComponent<Image>().color = Color.red;
                isValidate = false;
            }
        }
        if (isValidate)
        {
            OnResolve();
            QuitDigit();
        }
        else
        {
            ResetPasswordGiven();
        }
    }

    private void ResetPasswordGiven()
    {
        for (int i = 0; i < givenPassword.Length; i++)
        {
            givenPassword[i] = -1;
        }
        UpdateDisplayPassword();
    }

    public void AddPasswordNumber(int number)
    {
        Debug.Assert(!(number > 9), $"Number invalid, it has to be between 0 - 9, currently its {number}");
        int index = -1;
        for (int i = 0; i < password.Length; i++)
        {
            if (givenPassword[i] == -1)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            Debug.Log(index);
            givenPassword[index] = number;
            UpdateDisplayPassword();
        }
    }

    public void RemovePasswordNumber()
    {
        int index = -1;
        for (int i = password.Length - 1; i >= 0; i--)
        {
            if (givenPassword[i] != -1)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            givenPassword[index] = -1;
            UpdateDisplayPassword();
        }
        else
        {
            return;
        }
    }

    public void QuitDigit()
    {
        isActive = false;
        DigitUi.SetActive(false);
        GameManager.instance.ChangeState(GameManager.State.IN_GAME2);
    }

    private bool isInPassword(int number, List<int> indexToIgnore)
    {
        for (int j = 0; j < password.Length; j++)
        {
            if (!indexToIgnore.Contains(j) && number == password[j]) return true;
        }
        return false;
    }
}
