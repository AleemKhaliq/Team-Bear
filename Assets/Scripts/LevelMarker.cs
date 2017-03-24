using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMarker : MonoBehaviour
{
    public int levelNo;
    public bool isOpen;
    public bool isDone;
    public LevelMarker nextLevel;
    public LevelMarker previousLevel;

    public Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = gameObject.GetComponent<Animator>();

        isOpen = false;
        isDone = false;

        if (isDone)
        {
            nextLevel.isOpen = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool("IsOpen", isOpen);
        anim.SetBool("IsDone", isDone);
    }

    public void StartLevel ()
    {
        if (isOpen)
        {
            Debug.Log("Starting level " + levelNo);
        }
    }
}
