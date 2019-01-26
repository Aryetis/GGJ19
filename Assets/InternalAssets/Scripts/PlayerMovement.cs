using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_speed = 1.0f;
    public float gravity = 1.0f;
    public static float staticGravity;

    private static Vector3 m_move;
    private static CharacterController CC;
    private static bool m_collidingTotem = false;
    
    void Start()
    {
        staticGravity = gravity;
        CC = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        // Move 
        m_move = new Vector3(Input.GetAxis("Horizontal"), -gravity, Input.GetAxis("Vertical")) * m_speed * Time.deltaTime ;
        CC.Move(m_move);

        // Look forward
        if (Input.GetAxis("Horizontal") != 0.0f && Input.GetAxis("Vertical") != 0.0f)
            transform.rotation = Quaternion.LookRotation(new Vector3(m_move.x, 0, m_move.z));

        // Totem fusion
        if (m_collidingTotem && Input.GetKeyUp("joystick button 0"))
        {
            // TODO Add FX & Animation
            gameObject.SetActive(false); // Turn off player
            TotemBehavior.PlayerFusioned = true; // Turn on totem
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Totem"))
        {
            m_collidingTotem = true;
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("Totem"))
        {
            m_collidingTotem = false;
        }
    }


}
