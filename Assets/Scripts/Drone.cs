using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public bool faceRight;
    public bool faceLeft;
    public bool attacking;
    public bool inRange;
    public bool attackRange;
    public float speed;
    public float reverseSpeed;
    public float frontWakeRange;
    public float rearWakeRange;
    public float delayAttack;
    private float distance;
    private float yDistance;
    private float attackTimer = 0.2f;
    private float direction = 0;
    //public bool InRange { get; set; }
    
    private Player player;
    private Rigidbody2D myRigidBody;
    private SpriteRenderer SpriteRenderer;
    private Animator animate;    
    public Collider2D AttackTrigger;
    
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();        
        myRigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animate = GetComponent<Animator>();
        faceLeft = true;
        faceRight = false;        
        attacking = false;
        inRange = false;
        animate.SetBool("Attack", attacking);
        attacking = false;
        AttackTrigger.enabled = false;
        attackRange = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Behave();
        CheckPosition();        
	}

    /// <summary>
    /// Checks to see which side of the enemy the player is on
    /// </summary>
    void CheckPosition()
    {
        if (player.transform.position.x > transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    // Movement - base movement controls
    /// <summary>
    /// Moves the enemy to the right and flips the sprite to face the right
    /// </summary>
    void MoveRight()
    {
        if (faceLeft)
        {
            SpriteRenderer.flipX = true;
            faceLeft = false;
        }
        faceRight = true;        
        myRigidBody.AddForce(new Vector2(speed, 0));
    }

    /// <summary>
    /// Moves the enemy to the left and flips the sprite to face the left
    /// </summary>
    void MoveLeft()
    {
        if (faceRight)
        {
            SpriteRenderer.flipX = false;
            faceRight = false;
        }
        faceLeft = true;
        myRigidBody.AddForce(new Vector2(-speed, 0));
    }

    // Reversing
    /// <summary>
    /// Moves the enemy to the right but flips it to face the left as it is a reversing move
    /// </summary>
    void BackRight()
    {
        if (faceRight)
        {
            SpriteRenderer.flipX = false;
            faceRight = false;
        }
        faceLeft = true;
        myRigidBody.AddForce(new Vector2(speed, 0));
    }

    /// <summary>
    /// Moves the enemy to the left but flips it to face the right as it is a reversing move
    /// </summary>
    void BackLeft()
    {
        if (faceLeft)
        {
            SpriteRenderer.flipX = true;
            faceLeft = false;
        }
        faceRight = true;
        myRigidBody.AddForce(new Vector2(-speed, 0));
    }

    /// <summary>
    /// Makes the enemy chase after the player imperfectly
    /// </summary>
    void Chase()
    {
        if(direction == 1)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }        
    }

    /// <summary>
    /// Makes the enemy run away from the player in reverse
    /// </summary>
    void Run()
    {
        if (direction == 1)
        {
            BackLeft();
        }
        else
        {
            BackRight();
        }
    }

    // Attack logic
    void Swing()
    {
        CheckRange();
        if (/*faceRight && player.transform.position.x < transform.position.x + (direction * 1) || faceLeft && player.transform.position.x > transform.position.x + (direction * 1)*/attackRange)
        {
            Debug.Log("close");
            attacking = true;
            AttackTrigger.enabled = true;

            if (attacking)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    attacking = false;
                    AttackTrigger.enabled = false;
                }
            }
            player.Damage(1);
        }
    }

    // Combined Behaviour
    void Behave()
    {
        CheckRange();
        if (inRange)
        {
            Chase();
            //Swing();
        }

        //for(float i = 0; i <= delayAttack; i += Time.deltaTime)
        //{
        //    if(i == delayAttack)
        //    {
        //        Swing();
        //    }
        //}
    }

    void CheckRange()
    {
        distance = transform.position.x - player.transform.position.x;
        yDistance = transform.position.y - player.transform.position.y;
        if (faceRight)
        {
            if(distance < 0 && Math.Abs(distance) < frontWakeRange || distance > 0 && distance < rearWakeRange)
            {
                inRange = true;
                if(distance >= -1 && Math.Abs(yDistance) < 0.2f)
                {
                    attackRange = true;
                    Debug.Log("attack");
                }
                else
                {
                    attackRange = false;
                }
            }
            else
            {
                inRange = false;
                attackRange = false;
            }
        }
        if (faceLeft)
        {
            if(distance > 0 && distance < frontWakeRange || distance < 0 && Math.Abs(distance) < rearWakeRange)
            {
                inRange = true;
                if (distance <= 1 && Math.Abs(yDistance) < 0.2f)
                {
                    attackRange = true;
                    Debug.Log("attack");
                }
                else
                {
                    attackRange = false;
                }
            }
            else
            {
                inRange = false;
                attackRange = false;
            }
        }
    }
}
