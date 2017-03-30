﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (previousLevel == null)
        {
            isOpen = true;
        }

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
            switch (levelNo)
            {
                //Outdoor1
                case 1:
                    SceneManager.LoadScene("OutdoorLevel1");
                    break;
                //Cave 1
                case 2:

                    break;
                //Outdoor 2
                case 3:
                    SceneManager.LoadScene("OutdoorLevel2");
                    break;
                //Cave 2
                case 4:

                    break;
                //Outdoor 3
                case 5:

                    break;
                //Cave 3
                case 6:

                    break;
                //Outdoor 4
                case 7:

                    break;
                //Cave 4
                case 8:

                    break;
                //Outdoor 5
                case 9:

                    break;
                //Cave 5
                case 10:

                    break;
                //Final Level
                case 11:

                    break;
            }
        }
    }
}
