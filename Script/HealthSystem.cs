using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // For switching to GameOver scene

public class HealthSystem : MonoBehaviour
{
    private HealthSystem instance;
    [Header(" Health Settings")]
    public static int maxHealth = 1000;          // Maximum HP
    public static int currentHealth;           // Current HP

    [Header(" UI Reference")]
    public static TextMeshProUGUI healthText;  // UI text that shows HP


   
    void Start()
    {
        // Set full health when the game starts
        if (healthText == null)
            healthText = FindObjectOfType<TextMeshProUGUI>();

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Called when the player loses HP
    public void LoseHealth(int amount)
    {
        currentHealth -= amount;

        // Make sure HP never goes below 0
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

       
        // If HP is zero → go to GameOver scene
        if (currentHealth <= 0)
        {
            PersistentCanvas.HideAll();
            SceneManager.LoadScene("GameOver"); // Load scene named "GameOver"
        }
    }

    // Update the HP text on screen
    public static void UpdateHealthUI()
    {
        if (healthText)
            healthText.text = currentHealth.ToString();
    }
}