using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour {

    public int maxHealth = 1;
    public int curHealth;

	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Health
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }
}
