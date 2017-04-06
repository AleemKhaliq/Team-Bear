using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandDEX : MonoBehaviour {

    //public GameObject player;
    //public GameObject bossHand;
    //private Vector3 playerPos;
    //private Vector3 bossHandPos;


	//// Use this for initialization
	//void Start ()
 //   {
 //       playerPos = player.transform.position;
 //       bossHandPos = bossHand.transform.position;
	//}
	
	//// Update is called once per frame
	//void Update ()
 //   {
 //       //bossHand.transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
 //       //if(bossHandPos.y <= -2.14)
 //       //{
 //       //    bossHand.transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
 //       //}

 //       while (bossHandPos.y != -2.14)
 //       {
 //           bossHand.transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
 //       }
 //   }
  
    public bool Move = true;    ///gives you control in inspector to trigger it or not
    public Vector3 MoveVector = Vector3.up; 
    public float MoveRange = 2.0f; 
    public float MoveSpeed = 0.5f; 

    private BossHandDEX bossHandUpDown; 

    Vector3 startPosition; 
    void Start()
    {
        bossHandUpDown = this;
        startPosition = bossHandUpDown.transform.position;
    }
    void Update()
    {
        if (Move) 
            bossHandUpDown.transform.position = startPosition + MoveVector * (MoveRange * Mathf.Sin(Time.timeSinceLevelLoad * MoveSpeed));

    }
}

