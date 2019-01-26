using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemBehavior : MonoBehaviour
{
    public float FieldRadius;
    public float gravity = 1.0f;
    public float m_speed = 1.0f;

    private int m_cameraType = 0;

    public static bool PlayerFusioned
        {
            get
            {
                return m_playerFusioned;
            }
            set
            {
                m_exitPlayerCapsule.SetActive(value);
                m_playerFusioned = value;
            }
        }
    public GameObject m_player;
    [HideInInspector] public bool allowUnfusion = true;

    private static bool m_playerFusioned = false;
    private GameObject m_field;
    private static Vector3 m_move;
    private static CharacterController CC;
    private static GameObject m_exitPlayerCapsule;

    void Start()
    {
        m_field = transform.Find("Field").gameObject;
        m_field.transform.localScale = new Vector3(FieldRadius, FieldRadius, FieldRadius);
        if (m_player == null)
            m_player = GameObject.Find("Player");
        if (m_player == null)
        {
            Debug.Log("!!! GIVE THE PLAYER TO THE TOTEM, OR NAME THE PLAYER \"Player\" !!!");
            Debug.Break();
        }
        CC = GetComponent<CharacterController>();
        m_exitPlayerCapsule = transform.Find("ExitPlayerCapsule").gameObject;
        m_exitPlayerCapsule.SetActive(false);
    }
    
    void Update()
    {
        if (PlayerFusioned)
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

            // Totem Unfusion
            if (Input.GetKeyUp("joystick button 1"))
            {
                //Vector3 spawnPosition = transform.position + transform.forward * 1.5f + Vector3.up * 0.5f

                if (allowUnfusion)
                {
                    // TODO Add FX & Animation
                    PlayerFusioned = false; // Turn on totem
                    m_player.SetActive(true); // Turn on player
                    m_player.transform.position = m_exitPlayerCapsule.transform.position;
                    m_player.transform.rotation = Quaternion.LookRotation(new Vector3(m_move.x, 0, m_move.z));
                }
            }
        }
        else
        {
            m_move = new Vector3(0, -gravity, 0) * m_speed * Time.deltaTime;
            CC.Move(m_move);
        }
    }
}
