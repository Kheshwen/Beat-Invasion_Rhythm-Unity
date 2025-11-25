using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class ScoreManager : MonoBehaviour

{  
   


    //function that final points
   
    public static float FinalScore = 0f;




    ///////////////////////////////////
   
    public static ScoreManager instance;
    // declare score 
   
    public  TMP_Text scoreText;
    //declare accuracy score
   
    public  TMP_Text scoreAccuracyText;

    //////////////////////////////////////////
    



    public void AddPoint(int PointsToAdd)
    {
          GameManager.currentScore  += PointsToAdd;
          FinalScore  = GameManager.currentScore ;
          scoreText.text = FinalScore.ToString();
    }

   


    //function that adds accuracy points
  

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = GameManager.currentScore.ToString();
    }
    

    
    [HideInInspector] // Hides this from the Inspector
    public bool noteWasHitThisFrame = false;


    // Update is called once per frame
    void Update()
    {
        scoreAccuracyText.text = AccuracySystem.finalAccuracyScore.ToString("F1");
      
    }
    private void LateUpdate()
    {
        // Reset the flag at the very end of the frame,
        // so we're ready for the next frame.
        noteWasHitThisFrame = false;
    }


}


    

