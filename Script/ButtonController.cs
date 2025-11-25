using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header(" Key Settings")]
    public KeyCode keyToPress = KeyCode.Space; // Key used to hit notes

    [Header(" Sprite Settings")]
    public Sprite defaultSprite;   // Default button sprite
    public Sprite pressedSprite;   // Sprite when button is pressed
    private SpriteRenderer sr;


    private List<GameObject> notes = new List<GameObject>(); // Stores all notes currently inside the button collider
    private GameObject currentNote; // The currently active note (the first note inside collider)
 



 
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
               
        
    }



    void Update()
    {
        // When the player presses the assigned key
        if (Input.GetKeyDown(keyToPress))
        {
            
            sr.sprite = pressedSprite;

            TriggerCurrentNote();
        }


        // When the player releases the key
        if (Input.GetKeyUp(keyToPress))
        {
            
            sr.sprite = defaultSprite;
        }


        // If the current note was destroyed or left the area
        if (currentNote == null && notes.Count > 0)
        {
            // Remove any null (destroyed) note references
            notes.RemoveAll(n => n == null);

            // Assign the next note in the list as the active one
            if (notes.Count > 0)
                currentNote = notes[0];
        }
    }





    // Called when the button is pressed — triggers the active note
    private void TriggerCurrentNote()
    {
        if (currentNote == null)
            return;

       currentNote.GetComponent<NoteBase>().ExternalTriggerHit();
    }

   




    // Called when a note enters the button's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
      
            // Add to the list if not already there
            if (!notes.Contains(other.gameObject))
                notes.Add(other.gameObject);

            // If no current note is assigned, set this one
            if (currentNote == null)
                currentNote = other.gameObject;
        
    }




    // Called when a note exits the button's collider
    private void OnTriggerExit2D(Collider2D other)
    {
        
            // Remove the note from the list
            notes.Remove(other.gameObject);

            // If the current note exited, clear reference
            if (currentNote == other.gameObject)
                currentNote = null;
        
    }



}
