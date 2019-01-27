using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_speed = 1.0f;
    public float gravity = 1.0f;
    public static float staticGravity;
    [HideInInspector] public GameObject arrowCharge;

    private static Vector3 m_move;
    private static CharacterController CC;
    private static bool m_collidingTotem;
    private GameObject Floor;
    private FloorSwitch linkedFloorSwitch;
    private int m_cameraType;
    private GameObject m_charaVisu;

    // HACK to prevent player from tp-ing on a quantic collision bug after fusioning/unfusioning
    private bool justUnfusioned = true;
    private float justUnfusionedTimer;
    private float justUnfusionedResetTimerValue = 0.05f;

    private GameObject totem;

    void Start()
    {
        arrowCharge = transform.Find("Arrow").gameObject;
        arrowCharge.SetActive(false);
        m_collidingTotem = false;
        m_cameraType = 0;
        staticGravity = gravity;
        CC = GetComponent<CharacterController>();
        Floor = GameObject.Find("Floor");
        justUnfusionedTimer = justUnfusionedResetTimerValue;
        m_charaVisu = transform.Find("CharaVisu").gameObject;
    }
    
    void Update()
    {
//if (CC.velocity.magnitude > 15.0f )
//Debug.Break();
        if (justUnfusioned)
        {
            justUnfusionedTimer -= Time.deltaTime;
            if (justUnfusionedTimer > 0.0f)
                return;
            else
                justUnfusioned = false;
        }


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

        // Look forward & play run animation
        if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
        {
            m_charaVisu.GetComponent<Animator>().SetBool("IsRun", true);
            transform.rotation = Quaternion.LookRotation(new Vector3(m_move.x, 0, m_move.z));
        }

        // Totem fusion
        if (m_collidingTotem && (Input.GetKeyUp("joystick button 0") || Input.GetKeyDown(KeyCode.W)))
        {

            // !!! TAKE CARE IF PLAYER IS ON SWITCH, THE SWITCH WON T RECEIVE THE OnTriggerExit EVENT !!!
            // => Remove interactor from floorswitch underneath (if any) manually
            RaycastHit hitinfo;
            Physics.Raycast(transform.position, Vector3.down, out hitinfo, 5.0f);
            bool floorSwitchUnderPlayer = hitinfo.collider.gameObject.GetComponent<FloorSwitch>();
            if (linkedFloorSwitch != null && floorSwitchUnderPlayer)
                linkedFloorSwitch.DecreaseInteractors();

            // TODO Add FX & Animation
            justUnfusioned = true;
            justUnfusionedTimer = justUnfusionedResetTimerValue;

            // to prevent the player from just popping right back in the totem after 
            // going out without even touching the damn totem again
            m_collidingTotem = false;

            GetComponent<DisplayHelp>().hideHelp('A');

            gameObject.SetActive(false); // Turn off player
            TotemBehavior.PlayerFusioned = true; // Turn on totem
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.GetComponent<FloorSwitch>() != null)
        {
            linkedFloorSwitch = hit.gameObject.GetComponent<FloorSwitch>();
        }
        if (hit.gameObject.CompareTag("Totem"))
        {
            m_collidingTotem = true;
            GetComponent<DisplayHelp>().showHelp('A');
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.GetComponent<FloorSwitch>() != null)
        {
            linkedFloorSwitch = null;
        }
        if (hit.gameObject.CompareTag("Totem"))
        {
            m_collidingTotem = false;
            GetComponent<DisplayHelp>().hideHelp('A');
        }
    }


}
