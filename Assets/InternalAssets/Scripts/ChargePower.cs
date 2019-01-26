using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePower : MonoBehaviour
{
    public float CostPerSecond;
    public float Speed;
    public float gravityWhileDashing = 0.5f;

    private PlayerMovement pm;
    private bool chargeActive;
    private Vector3 m_move;
    private CharacterController CC;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 2") && Energy.Value > 0.0f && !chargeActive)
        {
            pm.enabled = false;
            Time.timeScale = 0.2f;
        }
        else if (Input.GetKeyUp("joystick button 2") && !chargeActive)
        {
            Time.timeScale = 1.0f;
            // TODO spawn FX
            // TODO tweak stock camera speed value (to avoid catching up the cam while dashing)
            chargeActive = true;
        }

        if (chargeActive)
        {
            if (Energy.Value <= 0.0f)
                StopCharge();
            else
            {
                // Move 
                m_move = new Vector3(transform.forward.x, -gravityWhileDashing, transform.forward.z) * Speed * Time.deltaTime;
                CC.Move(m_move);

                // Drain energy
                Energy.RemoveEnergy(CostPerSecond * Time.deltaTime);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("DestructibleWall"))
        {
            // TODO Spawn FX
            Destroy(hit.gameObject);
        }
        else if (hit.gameObject.CompareTag("Ramp") || hit.gameObject.CompareTag("Floor"))
            return;
        else
        {
            StopCharge();
        }
    }

    private void StopCharge()
    {
        // TODO restore stock camera speed value
        chargeActive = false;
        pm.enabled = true;
    }
}
