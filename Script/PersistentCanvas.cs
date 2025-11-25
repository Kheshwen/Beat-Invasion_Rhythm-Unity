using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentCanvas : MonoBehaviour
{
    private static PersistentCanvas instance;

    
    public GameObject resultCanvas;
    void Awake()
    {
        if (instance == null)                               //make sure only this canvas that are used from any scene at the first place will be used for other scene also
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (resultCanvas != null)
                resultCanvas.SetActive(false);

             
        }
        else
        {
            Destroy(gameObject);
        }
    }

  
    public static void ShowMainCanvas()
    {
        if (instance == null) return;

        // Activate MainCanvas parent
        instance.gameObject.SetActive(true);

        // activate all child except result Canvas
        foreach (Transform child in instance.transform)
        {
            if (instance.resultCanvas == null || child.gameObject != instance.resultCanvas)
                child.gameObject.SetActive(true);
        }

        // make sure ResultCanvas off
        if (instance.resultCanvas != null)
            instance.resultCanvas.SetActive(false);

     
    }






    public static void ShowResultCanvas()
    {
        if (instance == null) return;

        instance.gameObject.SetActive(true);

        // hide all child except ResultCanvas
        foreach (Transform child in instance.transform)
        {
            if (child.gameObject != instance.resultCanvas)
                child.gameObject.SetActive(false);
        }

        // activate ResultCanvas
        if (instance.resultCanvas != null)
            instance.resultCanvas.SetActive(true);
        else
            Debug.LogWarning("!!!");

        // to update text
        ResultSystem result = Object.FindFirstObjectByType<ResultSystem>();
        if (result != null)
            result.UpdateResultTexts();

    
    }

   
    public static void HideAll()
    {
        if (instance == null) return;

       
        foreach (Transform child in instance.transform)
        {
            child.gameObject.SetActive(false);
        }

       
    }

  



    public static void ShowAll()
    {
        if (instance == null) return;

        instance.gameObject.SetActive(true);

        foreach (Transform child in instance.transform)
        {
            child.gameObject.SetActive(true);
        }

       
    }
}
