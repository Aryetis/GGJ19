using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_speed = 1.0f;

    private Vector3 m_move;
    private CharacterController CC; 

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * m_speed * Time.deltaTime ;
        CC.Move(m_move);
    }
}
