using UnityEngine;
using UnityEngine.UI;

public class FadeSystem : MonoBehaviour
{
    private static FadeSystem _i;
    public static FadeSystem instance
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load("FadeSystem") as GameObject).GetComponent<FadeSystem>();
                DontDestroyOnLoad(_i.gameObject);
            }

            return _i;
        }
    }

    [SerializeField] private Image fade;
    private Animator animator;
    private int fadeHash;

    private void Start()
    {
        animator = GetComponent<Animator>();

        fadeHash = Animator.StringToHash("Fade");
    }

    public void ToggleFade(bool fade)
    {
        animator.SetBool(fadeHash, fade);
    }
}
