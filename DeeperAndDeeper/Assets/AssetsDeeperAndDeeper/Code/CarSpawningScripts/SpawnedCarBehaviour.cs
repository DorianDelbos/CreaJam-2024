using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedCarBehaviour : MonoBehaviour
{
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, 0, -50f);
        if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }
}
