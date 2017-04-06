using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneView : MonoBehaviour
{
    public Drone drone;
    private Player player;

	// Use this for initialization
	void Start ()
    {
		drone = gameObject.GetComponentInParent<Drone>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Player"))
        {
            drone.attackRange = true;
            drone.attacking = true;
            drone.AttackTrigger.enabled = true;
            Debug.Log("1");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Player"))
        {
            drone.attackRange = true;
            drone.attacking = true;
            drone.AttackTrigger.enabled = true;
            Debug.Log("1");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Player"))
        {
            drone.attackRange = false;
            drone.attacking = false;
            drone.AttackTrigger.enabled = false;
            Debug.Log("1");
        }
    }
}
