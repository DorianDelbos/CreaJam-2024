using System;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject car;
    [SerializeField]private float minSpawnRate = 2f;
    [SerializeField]private float maxSpawnRate = 6f;
    private float nextSpawn = 0f;
    private float time = 0f;
    private float spawnDuration = 3f;
    private float pauseDuration = 5f;
    bool isOn = true;

    private void SpawnCar()
    {
        GameObject go = Instantiate(car, transform.position, transform.rotation);
        go.GetComponent<SpawnedCarBehaviour>().Speed = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            time += Time.deltaTime;
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + UnityEngine.Random.Range(minSpawnRate, maxSpawnRate);
                SpawnCar();
            }
        }
        else
        {
            time += Time.deltaTime;
            if (time > pauseDuration)
            {
                SwitchState();
            }
        }
        
    }

    void SwitchState()
    {
        time = 0;
        isOn =! isOn;
    }

    public void StopCar()
    {
        pauseDuration = 10f;
        time = 0f;
        isOn = false;
        
    }
}
