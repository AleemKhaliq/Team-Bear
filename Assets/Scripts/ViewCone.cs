using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour {

    public Turret turret;
    public bool isRight;

    void Awake()
    {
        turret = gameObject.GetComponentInParent<Turret>();
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isRight)
            {
                turret.attack(true);
            }
            else
            {
                turret.attack(false);
            }
        }
    }
}
