using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

    public Animator anim;
    public Player player;
    public bool flagTriggered;
    public bool triggeredOnce;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = gameObject.GetComponent<Animator>();
        flagTriggered = false;
        triggeredOnce = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool("PlayerTouch", flagTriggered);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Player") && triggeredOnce == false)
        {
            flagTriggered = true;
            triggeredOnce = true;
            player.spawnPoint = transform.position;
        }
    }
}
