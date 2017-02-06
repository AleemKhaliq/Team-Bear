using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 35f;
    public float maxSpeed = 5;
    public float jumpStrength = 150f;
    public bool grounded;
    public bool crouched;
    public bool uppercut;
    public bool dash;

    private Rigidbody2D getBody;
    private Animator anim;

	// Use this for initialization
	void Start()
    {
        getBody = gameObject.GetComponent <Rigidbody2D> ();
        anim = gameObject.GetComponent<Animator>();
        uppercut = true;
	}
	
	// Update is called once per frame
	void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(getBody.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsCrouched", crouched);
        anim.SetBool("Dash", dash);

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            getBody.AddForce(Vector2.up * jumpStrength);
            //Debug.Log("Jump");
        }

        //Swing
        if (Input.GetButtonDown("Fire1") && grounded)
        {
            anim.SetTrigger("Swing");
            //Debug.Log("Swing");
        }

        //Uppercut
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetButtonDown("Fire1") && uppercut)
        {
            anim.SetTrigger("Uppercut");
            Debug.Log("UpCut");
            StartCoroutine(useUppercut());
            if (!grounded)
            {
                uppercut = false;
            }
        }

        if (grounded)
        {
            uppercut = true;
            Debug.Log("1");
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
    }

    IEnumerator useUppercut()
    {
        Vector2 temp = getBody.velocity;
        getBody.velocity = new Vector2(0f, 7.5f);
        Debug.Log("1");
        yield return new WaitForSeconds (1);
        getBody.velocity = new Vector2(0f, 0f);
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

        getBody.AddForce((Vector2.right * speed) * input);

        if (input == 1)
        {
            //Debug.Log("Walk Right");
        }

        else if (input == -1)
        {
            //Debug.Log("Walk Left");
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
}
