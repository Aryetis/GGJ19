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
    private GameObject Floor;
    private FloorSwitch linkedFloorSwitch;
    private int m_cameraType = 0;
    
    void Start()
    {
        staticGravity = gravity;
        CC = GetComponent<CharacterController>();
        Floor = GameObject.Find("Floor");
    }
    
    void Update()
    {
        // Move
        if (m_cameraType == 0)
        {
            Vector3 foo = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
            m_move = (Input.GetAxis("Horizontal") * Camera.main.transform.right
                    + Vector3.down * gravity
                    + Input.GetAxis("Vertical") * foo) * m_speed * Time.deltaTime;
        }
        else
        {
            m_move = new Vector3
            (
            Input.GetAxis("Horizontal"),
            -gravity,
            Input.GetAxis("Vertical")
            ) * m_speed * Time.deltaTime;
            // <=>
            //m_move = (Input.GetAxis("Horizontal") * Floor.transform.right 
            //        + Vector3.down * gravity
            //        + Input.GetAxis("Vertical") * Floor.transform.forward ) * m_speed * Time.deltaTime;
        }
        CC.Move(m_move);

        // Look forward
        if (Input.GetAxis("Horizontal") != 0.0f && Input.GetAxis("Vertical") != 0.0f)
            transform.rotation = Quaternion.LookRotation(new Vector3(m_move.x, 0, m_move.z));

        // Totem fusion
        if (m_collidingTotem && Input.GetKeyUp("joystick button 0"))
        {
            // Remove an interactor from floorswitch underneath (if any)
            if (linkedFloorSwitch != null)
                linkedFloorSwitch.DecreaseInteractors();

            // TODO Add FX & Animation
            gameObject.SetActive(false); // Turn off player
            TotemBehavior.PlayerFusioned = true; // Turn on totem
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("FloorSwitch"))
        {
            linkedFloorSwitch = hit.gameObject.GetComponent<FloorSwitch>();
        }
        if (hit.gameObject.CompareTag("Totem"))
        {
            m_collidingTotem = true;
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("FloorSwitch"))
        {
            linkedFloorSwitch = null;
        }
        if (hit.gameObject.CompareTag("Totem"))
        {
            m_collidingTotem = false;
        }
    }


}
