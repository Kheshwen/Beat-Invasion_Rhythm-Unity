using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Elements")]
    public GameObject backgroundImage;     // Custom image shown on hover
    [Header("Audio")]
    public AudioSource hoverAudioSource;   // Audio to play on hover

    // When mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (backgroundImage)
            backgroundImage.SetActive(true);

        if (hoverAudioSource && !hoverAudioSource.isPlaying)
            hoverAudioSource.Play();
    }

    // When mouse leaves the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (backgroundImage)
            backgroundImage.SetActive(false);

        if (hoverAudioSource && hoverAudioSource.isPlaying)
            hoverAudioSource.Stop();
    }
}
