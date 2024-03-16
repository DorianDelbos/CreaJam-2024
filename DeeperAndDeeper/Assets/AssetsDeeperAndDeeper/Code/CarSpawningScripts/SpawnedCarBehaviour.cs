using UnityEngine;

public class SpawnedCarBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private float timerToDelete = 5.0f;
    private float speed = 50f;
    public float Speed { get; set; }

    private void Start()
    {
        Destroy(gameObject, timerToDelete);
    }

    void Update()
    {
        rb.velocity = -transform.forward * speed;
    }
}
