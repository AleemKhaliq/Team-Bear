using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public LevelMarker marker;

	// Use this for initialization
	void Start ()
    {
        marker = GameObject.FindGameObjectWithTag("Marker").GetComponent<LevelMarker>();
        while (marker.previousLevel != null)
        {
            marker = marker.previousLevel;
        }
        transform.position = marker.transform.position + new Vector3(0, 0.5f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (((Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0) || (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)) && marker.nextLevel != null)
        {
            transform.position = marker.nextLevel.transform.position + new Vector3(0, 0.5f);
            marker = marker.nextLevel;
        }
        if (((Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0) || (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)) && marker.previousLevel != null)
        {
            transform.position = marker.previousLevel.transform.position + new Vector3(0, 0.5f);
            marker = marker.previousLevel;
        }
        if (Input.GetButtonDown("Jump"))
        {
            marker.StartLevel();
        }
    }

    void Print()
    {
        Debug.Log("No: " + marker.levelNo + " " + "X: " + marker.transform.position.x + ",    y: " + marker.transform.position.y);
    }
}
