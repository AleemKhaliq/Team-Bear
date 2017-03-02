using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private int health;
    public int maximumHealth;

    public bool faceRight;
    public bool faceLeft;
    public float speed;

    private Player player;
    public Animator animate;    

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        faceLeft = true;
        faceRight = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
