using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatSpawner : MonoBehaviour
{
    [Tooltip("Drag your Beatmap asset file here")]
    public BeatmapData currentBeatmap;
    public GameObject beat;

    // --- 1. NEW: Judgment Prefab Variables ---
    [Header("Judgment Prefabs")]
    public GameObject perfectPrefab;
    public GameObject goodPrefab;
    public GameObject hitPrefab;
    public GameObject missPrefab;
    // ----------------------------------------

    
   

    private void Awake()
    {
        
    }
    // ---------------------------------

    void Start()
    {
        // Check if a beatmap is assigned
        if (currentBeatmap == null)
        {
            Debug.LogError("FATAL ERROR: No BeatmapData asset assigned to the BeatSpawner!");
            return; // Stop everything
        }

        // Check for AudioSource
        AudioSource audio = GetComponent<AudioSource>();
        if (audio == null)
        {
            Debug.LogError("No AudioSource component on the BeatSpawner!");
            return;
        }

        audio.clip = currentBeatmap.song;
        audio.Play();
        StartCoroutine(StartSpawn());
    }

    // --- 3. NEW: Update to Catch "Miss" Clicks ---
    private void Update()
    {
        // Check if the left mouse button was just clicked
        if (Input.GetMouseButtonDown(0) && !ScoreManager.instance.noteWasHitThisFrame)
        {
            // We need to check if we clicked on a note or on empty space.
            // This requires your "beat" prefab to have a 2D Collider (like CircleCollider2D).
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            // If the raycast didn't hit anything...
            if (hit.collider == null)
            {
                // ... we clicked on empty space! This is a "Miss".
                // Get the click position
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPosition.z = 0; // Make sure it's on the 2D plane

                // Call the SpawnJudgment function
                SpawnJudgment(missPrefab, clickPosition);
            }
        }
    }
    // ---------------------------------------------

    public IEnumerator StartSpawn()
    {
        // Check for invalid BPM
        if (currentBeatmap.bpm <= 0)
        {
            Debug.LogError("BPM must be greater than 0!");
            yield break; // Stop the coroutine
        }

        float secondsPerBeat = 60.0f / currentBeatmap.bpm;
        float elapsedTime = 0f;

        // Loop through every beat number in our beatmap
        for (int i = 0; i < currentBeatmap.beatsToSpawn.Length; i++)
        {
            float beatNumber = currentBeatmap.beatsToSpawn[i];
            int hp = currentBeatmap.hitPoints[i];

            float targetSpawnTime = beatNumber * secondsPerBeat;
            float waitTime = targetSpawnTime - elapsedTime;

            if (waitTime > 0)
            {
                yield return new WaitForSeconds(waitTime);
            }

            // --- Spawn Logic ---
            Debug.Log("Spawning beat " + beatNumber + " at time: " + targetSpawnTime);
            GameObject newbeat = Instantiate(beat);

            // Use the smarter spawn position logic (inside the screen)
            float padding = 1.0f;
            Vector2 camPos = Camera.main.transform.position;
            float camHeight = Camera.main.orthographicSize;
            float camWidth = camHeight * Camera.main.aspect;
            Vector2 screenMin = new Vector2(camPos.x - camWidth + padding, camPos.y - camHeight + padding);
            Vector2 screenMax = new Vector2(camPos.x + camWidth - padding, camPos.y + camHeight - padding);

            float randomX = Random.Range(screenMin.x, screenMax.x);
            float randomY = Random.Range(screenMin.y, screenMax.y);
            newbeat.transform.position = new Vector2(randomX, randomY);

            // Get the direction
            MovementDirection dir = currentBeatmap.moveDirections[i];

            // Get the Hit script
            Hit hitScript = newbeat.GetComponent<Hit>();

            // Pass ALL data to the note
            if (hitScript != null)
            {
                hitScript.Initialize(hp, dir);
            }

            // The "miss" timer (how long the note lives)
            Destroy(newbeat, 1.5f); // You can adjust this "1.5f"

            

            elapsedTime = targetSpawnTime;
        }

        yield return new WaitForSeconds(2f);


        Canvas targetCanvas = FindObjectOfType<Canvas>();

        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false);
        }


      

        yield return new WaitForSeconds(3.5f);
        PersistentCanvas.ShowResultCanvas();
        yield return new WaitUntil(() => Input.anyKeyDown);
        PersistentCanvas.HideAll();
        SceneManager.LoadScene("LevelSelectMenu");
       
       


    }

    // --- 4. NEW: Judgment Spawning Function ---
    public void SpawnJudgment(GameObject prefabToSpawn, Vector3 spawnPosition)
    {
        // Safety check in case you forgot to assign a prefab
        if (prefabToSpawn == null)
        {
            Debug.LogWarning("Tried to spawn a judgment, but its prefab is not assigned in BeatSpawner!");
            return;
        }

        // Spawn the prefab at the specified position
        GameObject judgment = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Destroy the new prefab after 1 second
        Destroy(judgment, 1f);
    }
    // ----------------------------------------
}  