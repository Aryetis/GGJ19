using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_speed = 1.0f;

    private Vector3 m_move;
    private CharacterController CC; 
    
    void Start()
    {
        CC = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        // Move 
        m_move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * m_speed * Time.deltaTime ;
        CC.Move(m_move);

        // Look forward
        if (m_move != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(m_move);
    }
}
