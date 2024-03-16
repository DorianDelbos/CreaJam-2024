using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SteeringWing : MonoBehaviour
{
    Vector3 rotationZ = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rotationZ = this.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GameState == GameManager.State.SUB_GAME_1)
        {
            if(Input.GetKey(KeyCode.D) && this.transform.rotation.z < 90f)
            {
                rotationZ += new Vector3(0, 0, 1);
            }
            else if (Input.GetKey(KeyCode.A) && this.transform.rotation.z > -90f)
            {
                rotationZ -= new Vector3(0, 0, 1);
            }
            rotationZ.z = Mathf.Clamp(rotationZ.z, -90, 90);
            this.transform.localEulerAngles = rotationZ;
        }
    }
}
