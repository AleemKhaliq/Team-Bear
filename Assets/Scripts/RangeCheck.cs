using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    private Player player;
    private Drone drone;
    private ViewAreaRight right;
    private ViewAreaLeft left;
    private ViewRearRight rightBehind;
    private ViewRearLeft leftBehind;

    public bool inRange;
    public bool IsRight { get; set; }
    public bool IsLeft { get; set; }
    public bool IsRightBehind { get; set; }
    public bool IsLeftBehind { get; set; }

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        drone = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Drone>();
        right = GameObject.FindGameObjectWithTag("Vision").GetComponent<ViewAreaRight>();
        left = GameObject.FindGameObjectWithTag("Vision").GetComponent<ViewAreaLeft>();
        rightBehind = GameObject.FindGameObjectWithTag("Vision").GetComponent<ViewRearRight>();
        leftBehind = GameObject.FindGameObjectWithTag("Vision").GetComponent<ViewRearLeft>();

        inRange = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ViewCheck();
        drone.InRange = inRange;
	}

    void ViewCheck()
    {
        if (drone.faceLeft && IsLeft || drone.faceLeft && IsLeftBehind || drone.faceRight && IsRight || drone.faceRight && IsRightBehind)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
}
