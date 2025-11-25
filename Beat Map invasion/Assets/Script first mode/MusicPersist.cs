using UnityEngine;

public class MusicPersist : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasStarted = false;

    void Start()                                                    
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    void Update()
    {
        if (!hasStarted && Input.anyKeyDown)           //make sure the song played only when we click any key
        {
            audioSource.Play();
            hasStarted = true;
        }
    }
}