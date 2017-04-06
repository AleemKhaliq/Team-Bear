using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGameOnClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
