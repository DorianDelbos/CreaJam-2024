using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        IN_GAME1,
        IN_GAME2,
        IN_GAME3,
        LOOKING_OBJECT,
        SUB_GAME_1,
        SUB_GAME_2,
        SUB_GAME_3,

        UNLOCK_DIGIT
    }
    public static GameManager instance;
    public State GameState = State.IN_GAME1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ChangeState(State state)
    {
        ISelection.lerpTime = 0;
        GameState = state;
    }
}
