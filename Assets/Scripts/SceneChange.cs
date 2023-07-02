using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [Header("遷移先のシーン"),SerializeField]string NextScene;
    bool SceneChangeFlag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && !SceneChangeFlag)
        {
            SceneChangeFlag = true;
            SceneManager.LoadScene(NextScene);
        }
    }
}
