using System.Collections.Generic;
using UnityEngine;

public interface ISelection
{
    static float lerpTime = 0f;
    float OffsetCamera { get; set; }
    private const float lerpSpeed = 0.025f;
    private const string outLineShaderName = "OutlineShaderMat";
    static Material outlineMaterial = Resources.Load<Material>(outLineShaderName);
    static ISelection selectedObjet = null;
    bool IsHover { get; set; }
    bool IsSelected { get; set; }
    Vector3 BasePos { get; set; }
    Quaternion BaseRot { get; set; }
    void Update()
    {
        GameObject go = (this as MonoBehaviour).gameObject;
        Vector3 camPos = Camera.main.transform.position;
        Vector3 direction = camPos - BasePos;
        Vector3 desiredPos = camPos - direction.normalized * OffsetCamera;
        GameManager gm = GameManager.instance;
        if (gm.GameState == GameManager.State.IN_GAME1 || gm.GameState == GameManager.State.SUB_GAME_1 || gm.GameState == GameManager.State.IN_GAME2 || gm.GameState == GameManager.State.SUB_GAME_2)
        {
            SelectUpdateOutline();
            IsHover = false;
        }
        if ((GameManager.instance.GameState == GameManager.State.IN_GAME1 || GameManager.instance.GameState == GameManager.State.SUB_GAME_1 || GameManager.instance.GameState == GameManager.State.IN_GAME2 || GameManager.instance.GameState == GameManager.State.SUB_GAME_2) && this == selectedObjet && (go.transform.position != BasePos || go.transform.rotation != BaseRot))
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
        Renderer renderer = (this as MonoBehaviour).GetComponentInChildren<MeshRenderer>();
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
        Vector3 desiredPos = pointToGo - direction.normalized * OffsetCamera;
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
        Vector3 desiredPos = BasePos + direction.normalized * OffsetCamera;

        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, BaseRot, lerpSpeed);
        if (lerpTime < 1)
        {
            go.transform.position = Vector3.Lerp(desiredPos, BasePos, lerpTime);
            lerpTime += lerpSpeed;
        }
    }
    public void OnClick();
    public virtual void OnUnselect()
    {
    }
    public void ResetMat()
    {
        Renderer renderer = (this as MonoBehaviour).GetComponentInChildren<MeshRenderer>();
        List<Material> matList = new List<Material>();
        foreach (var mat in renderer.materials)
        {
            if (mat.name != outLineShaderName + " (Instance)")
                matList.Add(mat);
        }
        renderer.SetMaterials(matList);
    }
}
