using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneView : MonoBehaviour
{
    public Drone drone;
    private Player player;
    private float attackTimer;
    private bool cool;

	// Use this for initialization
	void Start ()
    {
		drone = gameObject.GetComponentInParent<Drone>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drone.attackRange && !drone.attacking && !cool)
        {
            drone.attacking = true;
            drone.AttackTrigger.enabled = true;
            attackTimer = 0.2f;
        }
        if (drone.attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                drone.attacking = false;
                drone.AttackTrigger.enabled = false;
                cool = true;
                attackTimer = 1;
            }
        }
        if (cool)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                cool = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Player"))
        {
            drone.attackRange = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Player"))
        {
            drone.attackRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Player"))
        {
            drone.attackRange = false;
        }
    }
}
