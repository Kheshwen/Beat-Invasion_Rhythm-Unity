using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{

    public string nextSceneName;

    public void EndGame()     //to load second mode scene when hp still full
                             
    {
        SceneManager.LoadScene(nextSceneName);




    }
}
