using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePower : MonoBehaviour
{
    public float CostPerSecond;

    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Button 2"))
        {
            pm.enabled = false;
            Time.timeScale = 0.2f;
        }
        else if (Input.GetButtonUp("Button 2"))
        {
            Time.timeScale = 1.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ramp") || collision.gameObject.CompareTag("DestructibleWall"))
        {

        }
    }
}
