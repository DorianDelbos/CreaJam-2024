using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        IN_GAME1,
        LOOKING_OBJECT,
        SUB_GAME_1,

        IN_GAME2
    }
    public static GameManager instance;
    public State GameState = State.IN_GAME1;

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
