using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject Sample;
    public float Frequency = 0.3f;
    public int Amount = -1;
    float lastSpawn = 0f;

    // Update is called once per frame
    void Update()
    {
        lastSpawn -= Time.deltaTime;
        if(lastSpawn <= 0f)
        {
            if(Amount != 0)
            {
                Instantiate(Sample, transform.position, transform.rotation);

                if (Amount > 0)
                    Amount--;
            }
            lastSpawn = Frequency;
        }
    }
}
