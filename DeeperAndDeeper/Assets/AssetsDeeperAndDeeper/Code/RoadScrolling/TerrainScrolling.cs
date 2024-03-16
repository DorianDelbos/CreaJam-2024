using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScrolling : MonoBehaviour
{
    [SerializeField]private float scrollSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - scrollSpeed * Time.deltaTime);
        if (transform.position.z < -600f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -100f);
        }
    }
}
