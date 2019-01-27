using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField]
    private float enabledRadius = 20;

    [SerializeField]
    private float targettingDuration = 4;

    private GameObject player;
    private float targettingTimer;

    private LineRenderer laser;

    private bool hasFired = false;
    private float interval = 1;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targettingTimer = targettingDuration;
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, transform.position);
        laser.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInSight() && !hasFired){
            targetting();
        } else
        {
            laser.enabled = false;
            forgettingPlayer();
        }
    }

    private bool isPlayerInSight()
    {
        RaycastHit hit;
        return (Vector3.Distance(transform.position, player.transform.position) <= enabledRadius && Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit) && hit.transform.gameObject == player);
    }

    private void targetting()
    {
        transform.LookAt(player.transform);

        interval = 0.2f + targettingTimer / targettingDuration * (0.8f - 0.2f);
        targettingTimer -= Time.fixedDeltaTime;
        laser.enabled = Mathf.PingPong(Time.time%targettingDuration, interval) > (interval / 2.0f);

        laser.SetPosition(1, player.transform.position);
        
        if(targettingTimer <= 0)
        {
            fire();
            targettingTimer = targettingDuration;
        }
    }

    private void fire()
    {
        hasFired = true;
        transform.GetComponentInChildren<ParticleSystem>().Play();
        player.GetComponent<PlayerDeath>().killPlayer();
    }

    public void forgettingPlayer()
    {
        targettingTimer = targettingDuration;
        /*if(targettingTimer < targettingDuration)
        {
            targettingTimer += Time.fixedDeltaTime;
            if (targettingTimer > targettingDuration)
                targettingTimer = targettingDuration;
        }*/
    }
}
