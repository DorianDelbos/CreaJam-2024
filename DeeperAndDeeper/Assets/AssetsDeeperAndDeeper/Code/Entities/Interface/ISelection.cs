using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public interface ISelection
{
    static float lerpTime = 0f;
    private static float offsetCameraObject = 4f;
    private static float lerpSpeed = 0.025f;
    static Material outlineMaterial = Resources.Load<Material>("OutlineShaderMat");
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
        if (GameManager.instance.GameState == GameManager.State.INGAME && (go.transform.position != BasePos || go.transform.rotation != BaseRot))
        {
            BringBackToPos();
        }
        else if (GameManager.instance.GameState == GameManager.State.LOOKING_OBJECT && go.gameObject.transform.position != desiredPos)
        {
            BringToCam();
        }
    }
    void SelectUpdateOutline()
    {
        Renderer renderer = (this as MonoBehaviour).GetComponent<MeshRenderer>();
        List<Material> matList = new List<Material>();
        foreach(var mat in renderer.materials)
        {
            matList.Add(mat);
        }
        if (IsHover) matList.Add(outlineMaterial);
        renderer.SetMaterials(matList);
    }

    void BringToCam()
    {
        GameObject go = (this as MonoBehaviour).gameObject;
        Vector3 camPos = Camera.main.transform.position;
        Vector3 direction = camPos - BasePos;
        Vector3 desiredPos = camPos - direction.normalized * offsetCameraObject;
        go.transform.LookAt(Camera.main.transform);
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
            matList.Add(mat);
        }
        renderer.SetMaterials(matList);
    }
}
