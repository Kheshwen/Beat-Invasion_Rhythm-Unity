using UnityEngine;
using UnityEngine.UI;

public class AccuracySystem : MonoBehaviour
{
  
    public  static float accuracyScore = 0f;     
    public static int totalNotes = 0;        
   
    public static float finalAccuracyScore = 0f;    //the only valid final accuracy score

    void Update()
    {
        
    }

 
    public static void PerfectNote()  //accuracu score for third hit 
    {
        accuracyScore += 100f;
        UpdateAccuracy();
    }


    public static void GoodNote()   //accuracy score for second hit 
    {
        accuracyScore += 97.5f;
        UpdateAccuracy();
    }

   
    public static void NormalNote()     //accuracy score for first hit
    {
        accuracyScore += 95f;
        UpdateAccuracy();
    }

   
    public static void MissNote()  //accuracy score for missnote
    {
        accuracyScore += 0f;
        UpdateAccuracy();
    }

   
    public static void AddNote()   //to add amount of effects spawned in the game
    {
        totalNotes++;
        UpdateAccuracy();
    }


    public static void UpdateAccuracy()
    {
        if (totalNotes > 0) {             
       
        finalAccuracyScore = accuracyScore  / totalNotes;          //total accuracy score for each hit and missed note divide by total effects spawned to get valid Final Accuracy Score
        }
        else
            finalAccuracyScore = 0f;
    }
}
