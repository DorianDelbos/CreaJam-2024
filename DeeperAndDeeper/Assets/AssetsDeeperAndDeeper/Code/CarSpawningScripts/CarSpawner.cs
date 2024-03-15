using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject car;
    [SerializeField]private float minSpawnRate = 2f;
    [SerializeField]private float maxSpawnRate = 6f;
    private float nextSpawn = 0f;

    private void SpawnCar()
    {
        Instantiate(car, transform.position, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + Random.Range(minSpawnRate, maxSpawnRate);
            SpawnCar();
        }
    }
}
