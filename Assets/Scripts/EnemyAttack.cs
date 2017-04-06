using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Player"))
        {
            col.SendMessageUpwards("Damage", 1);
            col.SendMessageUpwards("KnockBack");
        }
    }
}
