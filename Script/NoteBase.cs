using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class NoteBase : MonoBehaviour           //base script
{
    [Header("Effects")]
    public GameObject hitEffect;                        //assign all effects that are spawned based on specific condition
    public GameObject goodEffect;
    public GameObject perfectEffect;
    public GameObject missEffect;


    [Header("Spawn Settings")]
    protected bool useManualPosition = true;
    protected Vector2 manualEffectPosition = Vector2.zero;
    protected Transform effectSpawnPoint;


    protected SpriteRenderer sr;
    protected HealthSystem healthSystem;

    protected bool hasHit = false;
 


   
    protected void Start()
    {
        sr = GetComponent<SpriteRenderer>() ?? GetComponentInChildren<SpriteRenderer>();      //find the sprite renderer for note
        healthSystem = FindObjectOfType<HealthSystem>();                                      //find health system script in the scene and assigned to healthSystem variable
    } 


  




    protected void OnTriggerExit2D(Collider2D other)   
    {
        if (hasHit) return;
        MissNote();
    }






    public abstract void ExternalTriggerHit();  //method to trigger specific note's behavior if the note in the button controller





    protected abstract void MissNote();     //method if the note missed 






    protected void SpawnEffect(GameObject effect)     // method to spawn effect at specific location
    {
        if (!effect) return;

        Vector3 spawnPos;

        if (useManualPosition)
            spawnPos = new Vector3(manualEffectPosition.x, manualEffectPosition.y, 0);
        else if (effectSpawnPoint)
            spawnPos = effectSpawnPoint.position;
        else
            spawnPos = transform.position;

        Instantiate(effect, spawnPos, Quaternion.identity);
    }



}
