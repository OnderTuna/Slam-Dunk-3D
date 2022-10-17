using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenu_Manager : MonoBehaviour
{
    void Start()
    {
        if(PlayerPrefs.HasKey("SonLevel"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SonLevel"));
        }
        else
        {
            PlayerPrefs.SetInt("SonLevel", 1);
            SceneManager.LoadScene(1);
        }
    }
}
