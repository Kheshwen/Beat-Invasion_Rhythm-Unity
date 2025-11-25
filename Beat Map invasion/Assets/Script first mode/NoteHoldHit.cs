using UnityEngine;

public class NoteHoldHit : NoteBase          //inherit from NoteBase script  
{
    
    public KeyCode keyToPress = KeyCode.Space;     //what kind of key need to click
    public float holdTime = 1.5f; // how long to hold before destroy

   


    private bool canBePressed = false;
    private bool isHolding = false;
    private bool hasRegistered = true;
    private float holdTimer = 0f;

 

    void Update()                                                               
    {
        if (!canBePressed) return;

        // Start holding
        if (Input.GetKeyDown(keyToPress))
        {

            if (hasRegistered)                                         //this block of code only triggered if player manage to click for only once
            {
                ResultSystem.RegisterEffect("HitEffect");
                AccuracySystem.NormalNote();
                AccuracySystem.AddNote();
                hasRegistered = false;
            }

            isHolding = true;
            holdTimer = 0f;
           
            SpawnEffect(hitEffect);
        }
       
        // Keep holding
        if (isHolding && Input.GetKey(keyToPress))                     //if player manage to hold the key until the note destroyed
        {
            holdTimer += Time.deltaTime;
            
            if (holdTimer >= holdTime)
            {
                hasHit = true;
                GameManager.instance.NormalHit();
                gameObject.SetActive(false);
            }
        }

        // Released early → reset timer
        if (isHolding && Input.GetKeyUp(keyToPress))
        {
            isHolding = false;
            holdTimer = 0f;
        }


    }








    void OnTriggerEnter2D(Collider2D other)            // if the note enter collider from gameobject with tag "Activator"
    {
        if (other.CompareTag("Activator"))
            canBePressed = true;
    }







    public override void ExternalTriggerHit()
    {
       

    }





    protected override void MissNote()                                          //override method that call methods from other classes like if the note missed
    {
        

        SpawnEffect(missEffect);
        healthSystem?.LoseHealth(100);
        GameManager.instance.NoteMissed();
        AccuracySystem.MissNote();
        AccuracySystem.AddNote();
        ResultSystem.RegisterEffect("MissEffect");
        gameObject.SetActive(false);
    }




   
}
