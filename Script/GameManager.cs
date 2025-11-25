using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     //this is for normal score. Not accuracu score

    public static bool startPlaying;

    public BeatScroller theBeatScroller;

    public static GameManager instance;

    public static int currentScore;                    //combine score
    public int scorePerNote = 100;                      //score for first hit
    public int scorePerGoodNote = 125;                  //score for second hit
    public int scorePerPerfectNote = 150;                //score for third hit

    public static int currentMultiplier = 1;
    public static int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    // Start is called before the first frame update
    private void Awake()
    {
        //make sure the canvas display is reset
        Canvas mainCanvas = FindObjectOfType<Canvas>();

        if (mainCanvas != null)                                       
        {
            Text[] texts = mainCanvas.GetComponentsInChildren<Text>(true);           

            foreach (Text t in texts)
            {
                if (t.name == "ScoreText")
                {
                    t.text = "Score: 0";

                }
                else if (t.name == "MultiText")
                {
                    t.text = "Multiplier: x1";

                }
            }


        }   

       
        GlobalReset.Reset();                      //call reset method to reset all static value from "don't destroyed" script
        PersistentCanvas.ShowMainCanvas();           //only show related canvas to the game
      
    }
  
    void Start()
    {
        instance = this;                               //make sure other scripts only use "instance" to access this script's variable and methods

        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying && Input.anyKeyDown)
        {
            startPlaying = true;
            theBeatScroller.hasStarted = true;
           
        }

    }
    public void NormalHit()                  //method for first hit
    {
        Debug.Log("Ok");
        currentScore += scorePerNote * currentMultiplier;

        NoteHit();
    }

    public void GoodHit()                    //method for second hit
    {
        Debug.Log("Good");
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()                 //method for third hit
    {
        Debug.Log("Perfect");
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }
    public void NoteHit()
    {
        Debug.Log("Hit on time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }

        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        // currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NoteMissed()                            //method for note that missed
    {
        Debug.Log("Missed!");

        currentMultiplier = 1;                         //reset current multiplier if the note missed
        multiplierTracker = 0;

        multiText.text = "Multiplier = x" + currentMultiplier;
    }
}

