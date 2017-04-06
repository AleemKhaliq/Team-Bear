using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelTrigger : MonoBehaviour
{
    public int levelNo;
    private int playerLevel;
    private bool canLeave;

    // Use this for initialization
    void Start()
    {
        playerLevel = PlayerPrefs.GetInt("levelReached", 1);
        canLeave = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Vertical") || Input.GetAxisRaw("Vertical") > 0) && canLeave)
        {
            if (playerLevel <= levelNo)
            {
                PlayerPrefs.SetInt("levelReached", (levelNo + 1));
            }
            Debug.Log("Level complete");
            SceneManager.LoadScene("Map");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            canLeave = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            canLeave = false;
        }
    }
}