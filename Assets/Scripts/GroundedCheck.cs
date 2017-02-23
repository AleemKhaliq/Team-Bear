﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour {

    private Player player;

	// Use this for initialization
	void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            player.grounded = true;

        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            player.grounded = true;

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            player.grounded = false;

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
