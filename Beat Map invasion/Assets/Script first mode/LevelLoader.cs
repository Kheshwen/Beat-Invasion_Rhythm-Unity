using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadLevel(string sceneName)          //simple script to load scene
    {
        SceneManager.LoadScene(sceneName);
    }
}

