using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_speed = 1.0f;
    public float gravity = 1.0f;
    public static float staticGravity;

    private Vector3 m_move;
    private CharacterController CC; 
    
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

        //transform.position = new Vector3(transform.position.x, transform.position.y-(gravity*Time.deltaTime), transform.position.z);

        // Look forward
        if (m_move != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(new Vector3(m_move.x, 0, m_move.z));
    }
}
