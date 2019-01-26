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
    }
    
    void Update()
    {
        m_playerScreenPos = Cam.WorldToScreenPoint(m_player.transform.position);
        m_playerOffset = new Vector2(
                                     (m_playerScreenPos.x - Cam.pixelWidth / 2.0f) / Cam.pixelWidth * 200.0f
                                   , (m_playerScreenPos.y - Cam.pixelHeight / 2.0f) / Cam.scaledPixelHeight * -200.0f
                                   );
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(m_playerOffset.x) > verticalTrigger || Mathf.Abs(m_playerOffset.y) > horizontalTrigger)
        {
            Vector2 camToPlayerScreenVectorNm = new Vector2(m_playerOffset.x / horizontalTrigger,
                                                             m_playerOffset.y / verticalTrigger * -1.0f);
            Vector3 camDesiredPosition = Cam.transform.position + new Vector3(camToPlayerScreenVectorNm.x, 0, camToPlayerScreenVectorNm.y);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, camDesiredPosition, m_cameraSpeed);
            transform.position = smoothedPosition;
        }
    }
}
