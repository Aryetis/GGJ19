using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject m_player;
    [Range(0,1)] public float m_cameraSpeed;
    [Range(0,100)] public float verticalTrigger = 30.0f;
    [Range(0, 100)] public float horizontalTrigger = 30.0f;

    private Camera Cam;
    private Vector3 m_playerScreenPos;
    private Vector2 m_playerOffset; // distance between center of screen and player in screen size percentage
                                    // [0;0] <=> center, [100;100] <=> at bot right corner of screen, [-100, xxx] <=> along the very left
    private Vector2 m_camDirection;
    //          |------------|     X->* describes the m_camDirection vector normalized
    //          *            |     => we only smoothly move the camera using this vector 
    //          |      X     |        until * is back in the offset
    //          |            |     (X : center of screen ; * : player ; borders : 
    //          |------------|      invisible screen portion triggering the camera movements, determined by verticalTrigger, horizontalTrigger)
    //private float m_cameraSpeed; // 1.0f - m_cameraSpeedInput;

    // Start is called before the first frame update
    void Start()
    {
        if (m_player == null)
            m_player = GameObject.Find("Player");
        if (m_player == null)
        {
            Debug.Log("!!! GIVE THE PLAYER TO THE CAMERA, OR NAME THE PLAYER \"Player\" !!!");
            Debug.Break();
        }

        Cam = GetComponent<Camera>();
        //m_cameraSpeed = 1.0f - m_cameraSpeedInput;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerScreenPos = Cam.WorldToScreenPoint(m_player.transform.position);
        //Debug.Log("player is " + m_playerScreenPos.x + " pixels from the left");
        m_playerOffset = new Vector2(
                                     (m_playerScreenPos.x - Cam.pixelWidth / 2.0f) / Cam.pixelWidth * 200.0f
                                   , (m_playerScreenPos.y - Cam.pixelHeight / 2.0f) / Cam.scaledPixelHeight * -200.0f
                                   );
Debug.Log("m_playerOffset : " + m_playerOffset);



        //if (Input.GetKeyDown("joystick button 0"))
        //{
        //    transform.position = transform.position + Vector3.left;
        //} 
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(m_playerOffset.x) > verticalTrigger || Mathf.Abs(m_playerOffset.y) > horizontalTrigger)
        {
            Vector2 camToPlayerScreenVectorNm = new Vector2(m_playerOffset.normalized.x,
                                                             m_playerOffset.normalized.y * -1.0f);
            Debug.Log("camToPlayerScreenVectorNm : " + camToPlayerScreenVectorNm);
            Vector3 camDesiredPosition = Cam.transform.position + new Vector3(camToPlayerScreenVectorNm.x, 0, camToPlayerScreenVectorNm.y);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, camDesiredPosition, m_cameraSpeed);
            transform.position = smoothedPosition;
            //Debug.Log("MOVING CAMERA");
        }
    }
}
