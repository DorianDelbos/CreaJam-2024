using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public interface ISelection
{
    static float lerpTime = 0f;
    private const float offsetCameraObject = 4f;
    private const float lerpSpeed = 0.025f;
    private const string outLineShaderName = "OutlineShaderMat";
    static Material outlineMaterial = Resources.Load<Material>(outLineShaderName);
    static ISelection selectedObjet;
    bool IsHover { get; set; }
    bool IsSelected { get; set; }
    Vector3 BasePos { get; set; }
    Quaternion BaseRot { get; set; }
    void Update()
    {
        GameObject go = (this as MonoBehaviour).gameObject;
        Vector3 camPos = Camera.main.transform.position;
        Vector3 direction = camPos - BasePos;
        Vector3 desiredPos = camPos - direction.normalized * offsetCameraObject;
        if (GameManager.instance.GameState == GameManager.State.INGAME && this == selectedObjet && (go.transform.position != BasePos || go.transform.rotation != BaseRot))
        {
            BringBackToPos();
        }
        else if (GameManager.instance.GameState == GameManager.State.LOOKING_OBJECT && this == selectedObjet && go.transform.position != desiredPos)
        {
            BringToCam();
        }
    }
    void SelectUpdateOutline()
    {
        Renderer renderer = (this as MonoBehaviour).GetComponent<MeshRenderer>();
        List<Material> matList = new List<Material>();
        foreach (var mat in renderer.materials)
        {
            if (mat.name != outLineShaderName + " (Instance)")
                matList.Add(mat);
        }
        if (IsHover) matList.Add(outlineMaterial);
        renderer.SetMaterials(matList);
    }

    void BringToCam()
    {
        GameObject go = (this as MonoBehaviour).gameObject;
        Vector3 pointToGo = Camera.main.transform.position;
        Vector3 direction = pointToGo - BasePos;
        Vector3 desiredPos = pointToGo - direction.normalized * offsetCameraObject;
        if (Vector3.Angle(go.transform.position, pointToGo) > 1)
        {
            var targetRotation = Quaternion.LookRotation(go.transform.position - Camera.main.transform.position);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, targetRotation, 0.025f);
        }
        if (lerpTime < 1)
        {
            go.transform.position = Vector3.Lerp(BasePos, desiredPos, lerpTime);
            lerpTime += lerpSpeed;
        }

    }

    void BringBackToPos()
    {
        GameObject go = (this as MonoBehaviour).gameObject;
        Vector3 camPos = Camera.main.transform.position;
        Vector3 direction = camPos - BasePos;
        Vector3 desiredPos = BasePos + direction.normalized * offsetCameraObject;

        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, BaseRot, lerpSpeed);
        if (lerpTime < 1)
        {
            go.transform.position = Vector3.Lerp(desiredPos, BasePos, lerpTime);
            lerpTime += lerpSpeed;
        }
    }
    public void OnClick();
    public void ResetMat()
    {
        Renderer renderer = (this as MonoBehaviour).GetComponent<MeshRenderer>();
        List<Material> matList = new List<Material>();
        foreach (var mat in renderer.materials)
        {
            if (mat.name != outLineShaderName + " (Instance)")
                matList.Add(mat);
        }
        renderer.SetMaterials(matList);
    }
}
