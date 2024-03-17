using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Letter : MonoBehaviour, ISelection
{
    public bool isHover = false;
    public bool isSelected = false;
    public Vector3 basePos;
    public Quaternion baseRot; 
    public float offsetCamera = 0.5f;
    [SerializeField] PlayableDirector sequence;
    public GameObject subScene1;
    public GameObject subScene2;
    public float OffsetCamera { get => offsetCamera; set => offsetCamera = value; }

    public bool IsHover { get => isHover; set => isHover = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Quaternion BaseRot { get => baseRot; set => baseRot = value; }

    public void OnClick()
    {
        isSelected = true;
        GameManager.instance.ChangeState(GameManager.State.LOOKING_OBJECT);
        (this as ISelection).ResetMat();
        ISelection.selectedObjet = this;
    }

    public void OnUnselect()
    {
        sequence.Play();
        StartCoroutine(BeginSubGame1(2));
        GameManager.instance.ChangeState(GameManager.State.IN_GAME2);
        subScene1.SetActive(false);
        subScene2.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        basePos = this.transform.position;
        baseRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        (this as ISelection).Update();
    }

    IEnumerator BeginSubGame1(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        GameManager.instance.ChangeState(GameManager.State.SUB_GAME_2);
        StartCoroutine(ResetCamera());
    }
    IEnumerator ResetCamera()
    {
        float timer = 0f;
        Quaternion currentCamRot = Camera.main.transform.rotation;
        Quaternion wantedRotation = Quaternion.Euler(0, 0, 0);
        while(timer < 1)
        {
            Camera.main.transform.rotation = Quaternion.Slerp(currentCamRot, wantedRotation, timer);
            timer += 0.01f;
            yield return null;
        }
    }
}
