using UnityEngine;

public class NoteTripleHit : NoteBase           //inherit from NoteBase script
{
    
    public Sprite spriteStage1;        //assign sprite because the sprite changed based on the specific condition
    public Sprite spriteStage2;
    public Sprite spriteStage3;

    private int hitCount = 0;
  

    // Triggered externally when player hits button
    public override  void ExternalTriggerHit()                                        //override method that call methods from other classes like for normal score management and accuracy score management 
    {
       

        hitCount++;


        // First Hit Stage
        if (hitCount == 1)
        {
            sr.sprite = spriteStage1;

            SpawnEffect(hitEffect);
            ResultSystem.RegisterEffect("HitEffect");
            AccuracySystem.NormalNote();
            AccuracySystem.AddNote();
            GameManager.instance.NormalHit();
        }

        // Second Hit Stage 
        else if (hitCount == 2)
        {
            sr.sprite = spriteStage2;

            SpawnEffect(goodEffect);
            ResultSystem.RegisterEffect("GoodEffect");
            AccuracySystem.GoodNote();
            AccuracySystem.AddNote();
            GameManager.instance.GoodHit();

            
        }

        // Third Hit Stage → complete
        else if (hitCount == 3)
        {
            hasHit = true;
            sr.sprite = spriteStage3;

            SpawnEffect(perfectEffect);
            ResultSystem.RegisterEffect("PerfectEffect");
            AccuracySystem.PerfectNote();
            AccuracySystem.AddNote();
            GameManager.instance.PerfectHit();

            gameObject.SetActive(false);

        }

    }


 

    protected override void MissNote()                                             //override method that call methods from other classes like if the note missed
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