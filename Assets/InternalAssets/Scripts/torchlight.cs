using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchlight : MonoBehaviour
{
    private void Start()
    {
        Random.seed = Random.RandomRange(1, 50);
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Light>().intensity = Mathf.PerlinNoise(Time.unscaledTime/10000, Random.value);
    }
}
