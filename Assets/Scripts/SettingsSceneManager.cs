using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsSceneManager : MonoBehaviour
{
    public GameObject Tutorial;


    
    public void GoToTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }


}
