using UnityEngine;

public class NoteSingleHit : NoteBase     //inherit from NoteBase script
{
   

  

    public override void ExternalTriggerHit()             //override method that call methods from other classes like for normal score management and accuracy score management
    {
        hasHit = true;
     

        SpawnEffect(hitEffect);                           

        GameManager.instance.NormalHit();
        ResultSystem.RegisterEffect("HitEffect");
        AccuracySystem.NormalNote();
        AccuracySystem.AddNote();

        gameObject.SetActive(false);

    }

  

    protected override void MissNote()                                   //override method that call methods from other classes like if the note missed
    {
        {

            SpawnEffect(missEffect);

            ResultSystem.RegisterEffect("MissEffect");
            AccuracySystem.MissNote();
            AccuracySystem.AddNote();
            healthSystem?.LoseHealth(100);
            GameManager.instance.NoteMissed();
            gameObject.SetActive(false);

        }

    }





   
}