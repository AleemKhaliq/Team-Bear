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

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && marker.levelNo != 11)
        {
            transform.position = marker.nextLevel.transform.position + new Vector3(0, 0.5f);
            marker = marker.nextLevel;
            Debug.Log("Right");
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && marker.levelNo != 1)
        {
            transform.position = marker.previousLevel.transform.position + new Vector3(0, 0.5f);
            marker = marker.previousLevel;
            Debug.Log("Left");
        }
        transform.position = marker.transform.position;
    }
}
