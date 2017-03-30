using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public LevelMarker marker;
    public bool moving;
    public int count;
    private bool up;

	// Use this for initialization
	void Start ()
    {
        marker = GameObject.FindGameObjectWithTag("Marker").GetComponent<LevelMarker>();
        while (marker.previousLevel != null)
        {
            marker = marker.previousLevel;
        }
        transform.position = marker.transform.position + new Vector3(0, 0.35f);
        moving = false;
        up = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (((Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0) || (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)) && marker.nextLevel != null)
        {
            moving = true;
            transform.position = marker.nextLevel.transform.position + new Vector3(0, 0.35f);
            marker = marker.nextLevel;
        }
        if (((Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0) || (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)) && marker.previousLevel != null)
        {
            moving = true;
            transform.position = marker.previousLevel.transform.position + new Vector3(0, 0.35f);
            marker = marker.previousLevel;
        }
        if (Input.GetButtonDown("Jump"))
        {
            moving = true;
            marker.StartLevel();
        }

        //Reset idle loop on action
        if (moving)
        {
            up = false;
            count = 0;
        }

        //Idle loop
        /*if (count < 20)
        {
            count++;
        }
        else
        {
            if(!up)
            {
                transform.position = transform.position + new Vector3(0, 0.25f);
                count = 0;
                up = true;
            }
            else
            {
                transform.position = transform.position + new Vector3(0, -0.25f);
                count = 0;
                up = false;
            }
        }*/
        moving = false;
    }

    void Print()
    {
        Debug.Log("No: " + marker.levelNo + " " + "X: " + marker.transform.position.x + ",    y: " + marker.transform.position.y);
    }
}
