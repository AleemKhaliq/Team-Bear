using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speed = 35f;
    public float maxSpeed = 5;
    public float jumpStrength = 150f;
    public bool grounded;
    public bool crouched; 
    public bool dash;
    public bool noMove; //Prevent movement
    public int faceDirection;   //Direction Player is facing

    public bool attacking;
    private float attackCool = 0.3f; //Cooldown between swings
    private float attackTimer;
    public bool uppercut;
    public bool charged;    //Whether or not attck has been held long enough for charge moves
    public float heldTime;  //Ammount of time attack has been held

    public int maxHealth = 5;
    public int curHealth;

    private Rigidbody2D getBody;
    public Collider2D AttackTrigger;
    private Animator anim;

	// Use this for initialization
	void Start()
    {
        getBody = gameObject.GetComponent <Rigidbody2D> ();
        anim = gameObject.GetComponent<Animator>();
        curHealth = maxHealth;
        uppercut = true;
        attacking = false;
        //attackTrigger = gameObject.GetComponentInChildren<Collider2D>("Claw");
        AttackTrigger.enabled = false;
	}
	
	// Update is called once per frame
	void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(getBody.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsCrouched", crouched);
        anim.SetBool("Dash", dash);
        anim.SetBool("Attack", attacking);

        //Rotation
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            faceDirection = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            faceDirection = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            getBody.AddForce(Vector2.up * jumpStrength);
            //Debug.Log("Jump");
        }

        //Check if charged
        if (Input.GetButtonDown("Fire1"))
        {
            heldTime = Time.time;
        }
        if (Input.GetButton("Fire1"))
        {
            noMove = true;
            if (Time.time - heldTime > 1f)
            {
                charged = true;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            noMove = false;
            heldTime = 0;
            charged = false;
        }

        //Swing
        if (Input.GetButtonUp("Fire1") && !charged && uppercut && !attacking)
        { 
            attacking = true;
            AttackTrigger.enabled = true;
            attackTimer = attackCool;
        }
        if (Input.GetButtonUp("Fire1") && charged && uppercut && !attacking)
        {
            attacking = true;
            AttackTrigger.enabled = true;
            attackTimer = attackCool;
        }
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
        //Uppercut
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetButtonDown("Fire1") && uppercut)
        {
            uppercut = true;
            anim.SetTrigger("Uppercut");
            StartCoroutine(useUppercut());
            if (!grounded)
            {
                uppercut = false;
            }
        }

        if (grounded)
        {
            uppercut = true;
        }

        //Dash
        if (Input.GetKey("left shift"))
        {
            dash = true;
        }
        if (!Input.GetKey("left shift"))
        {
            dash = false;
        }
        if (dash && Input.GetAxisRaw("Horizontal") != 0)
        {
            getBody.AddForce((Vector2.right * 150f) * Input.GetAxis("Horizontal"));
        }
        

        //Crouch
        if (Input.GetAxisRaw("Vertical") < 0 && grounded && Input.GetAxis("Horizontal") == 0)
        {
            crouched = true;
        }
        else
        {
            crouched = false;
        }

        //Health
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if(curHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator useUppercut()
    {
        Vector2 temp = getBody.velocity;
        noMove = true;
        getBody.velocity = new Vector2(0f, 15f);
        yield return new WaitForSeconds (0.25f);
        getBody.velocity = new Vector2(0f, 0f);
        noMove = false;
    }

    void FixedUpdate()
    {
        Vector3 antiVelocity = getBody.velocity;
        antiVelocity.x *= 0.9f;
        antiVelocity.y = getBody.velocity.y;
        antiVelocity.z = 0.0f;

        //Friction
        if (grounded)
        {
            getBody.velocity = antiVelocity;
        }

        //Walking
        float input = Input.GetAxisRaw("Horizontal");

        if (!noMove)
        {
            getBody.AddForce((Vector2.right * speed) * input);
        }

        //Limiting Speed
        if (getBody.velocity.x > maxSpeed)
        {
            getBody.velocity = new Vector2(maxSpeed, getBody.velocity.y);
        }

        if (getBody.velocity.x < -maxSpeed)
        {
            getBody.velocity = new Vector2(-maxSpeed, getBody.velocity.y);
        }
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }

    public IEnumerator Knockback(float knockDur, float knockDir, float power)
    {
        float time = 0;
        while (knockDur > time)
        {
            time += Time.deltaTime;
            getBody.AddForce(new Vector2((power) * knockDir, power));
        }
        yield return 0;
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
