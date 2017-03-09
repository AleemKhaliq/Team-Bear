using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public int dmg;
    public string attack;
    public bool airFloat = false;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    	
	// Update is called once per frame
	void Update ()
    {
		if (player.charged)
        {
            dmg = 2;
        }
        else if (player.uppercut)
        {
            dmg = 2;
        }
        else if (player.slam)
        {
            dmg = 2;
        }
        else
        {
            dmg = 1;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Enemy"))
        {
            col.SendMessageUpwards("Damage", dmg);
            col.SendMessageUpwards("KnockBack");
        }
    }
}
