using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GlobalReset : MonoBehaviour
{
   public static void Reset()
    {                     //TO RESET ALL STATIC VARIABLE BECAUSE THE SCRIPTS USE STATIC VARIABLE OR THERE ARE "DON'T DESTROYED" BUILT IN FUNCTION. MAKE SURE ALL RESET BEFORE PROCEED TO NEXT LEVEL
        
        
        GameManager.currentScore = 0;

        GameManager.currentMultiplier = 1;

        GameManager.multiplierTracker = 0;

        AccuracySystem.finalAccuracyScore = 0;

        AccuracySystem.accuracyScore = 0;

        AccuracySystem.totalNotes = 0;

        HealthSystem.currentHealth = HealthSystem.maxHealth;

        ResultSystem.normalHits = 0;

        ResultSystem.goodHits = 0;

        ResultSystem.perfectHits = 0;

        ResultSystem.missedHits = 0;

        ResultSystem.gameEnded = false;

      

       



        HealthSystem.UpdateHealthUI();

       

    }
}
 