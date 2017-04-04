using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TunnelHome : MonoBehaviour
{
    public int levelNo;
    public int enemyCount;
    public int minEnemyCount;
    private int playerLevel;    
    private TunnelMessage TunnelMsg;    
        
	// Use this for initialization
	void Start ()
    {
        TunnelMsg = GameObject.FindGameObjectWithTag("T_Message").GetComponent<TunnelMessage>();
        playerLevel = PlayerPrefs.GetInt("levelReached", 1);
        enemyCount = 0;      
    }

    /// <summary>
    /// Checks to see if the player has entered over the area of the tunnel
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && enemyCount < minEnemyCount)
        {
            Debug.Log("Area crossed");
            // Sets Display boolean to true so that the tunnel message prompt is displayed
            TunnelMsg.Display = true;

            if (playerLevel <= levelNo)
            {
                PlayerPrefs.SetInt("levelReached", (levelNo + 1));
            }

            Debug.Log("Level complete");
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("Map");
            }

        }        
    }
    
    public void AddEnemy(int i)
    {
        enemyCount = enemyCount + i;
    }

    public void RemoveEnemy()
    {
        enemyCount--;
    }
}
