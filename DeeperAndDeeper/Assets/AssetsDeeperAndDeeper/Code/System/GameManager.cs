using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        INGAME,
        LOOKING_OBJECT,
        SUB_GAME_1
    }
    public static GameManager instance;
    public State GameState { get; private set; } = State.INGAME;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            transform.parent = null;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ChangeState(State state)
    {
        ISelection.lerpTime = 0;
        GameState = state;
    }
}
