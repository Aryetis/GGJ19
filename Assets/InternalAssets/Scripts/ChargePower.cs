﻿using System.Collections;
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
    private GameObject m_charaVisu;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        CC = GetComponent<CharacterController>();
        m_charaVisu = transform.Find("CharaVisu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Look forward
        if (!chargeActive && Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f && (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.C)))
        {
            Vector3 foo = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
            Vector3 bar = (Input.GetAxis("Horizontal") * Camera.main.transform.right
                    + Vector3.down * pm.gravity
                    + Input.GetAxis("Vertical") * foo) * pm.m_speed * Time.deltaTime;

            Vector3 tur = new Vector3(bar.x, 0, bar.z);
            transform.rotation = Quaternion.LookRotation(tur);
        }

        if ((Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.C)) && Energy.Value > 0.0f && !chargeActive)
        {
            // Disable usual player movements
            pm.enabled = false;
            pm.arrowCharge.SetActive(true);
            Time.timeScale = 0.2f;
        }
        else if ((Input.GetKeyUp("joystick button 2") || Input.GetKeyUp(KeyCode.C) )&& !chargeActive )
        {
            Time.timeScale = 1.0f;
            pm.arrowCharge.SetActive(false);
            // TODO spawn FX
            // TODO tweak stock camera speed value (to avoid catching up the cam while dashing)
            chargeActive = true;
        }

        if (chargeActive)
        {
            if (Energy.Value <= 0.0f && CC.isGrounded)
                StopCharge();
            else
            {
                // Play anim
                m_charaVisu.GetComponent<Animator>().SetBool("IsRun", true);

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
        if (hit.gameObject.CompareTag("DestructibleWall") && chargeActive)
        {
            hit.gameObject.transform.Find("CFX2_RockHit").GetComponent<ParticleSystem>().Play();
            hit.gameObject.GetComponent<Renderer>().enabled = false;
            hit.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(hit.gameObject, hit.transform.GetComponentInChildren<ParticleSystem>().main.duration);
        }
        else if (hit.gameObject.CompareTag("Ramp") || hit.gameObject.CompareTag("Floor") 
            || hit.gameObject.CompareTag("FloorSwitch") || hit.gameObject.CompareTag("IgnoredByTotemExit") )
            return;
        else
        {
            StopCharge();
        }

    }

    private void StopCharge()
    {
        // TODO restore stock camera speed value
        m_charaVisu.GetComponent<Animator>().SetBool("IsRun", false);
        chargeActive = false;
        pm.enabled = true;
    }
}
