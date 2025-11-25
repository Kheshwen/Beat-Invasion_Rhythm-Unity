using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSystem : MonoBehaviour
{
    public static ResultSystem instance;

    [Header(" Note Tags to Detect")]
    private readonly string[] noteTags = { "NoteHold", "Note", "Note2hit", "Note3hit" };

    [Header(" Player Health")]
    private HealthSystem healthSystem;

    [Header(" Results Panel")]
    public GameObject resultsPanel;

    [Header(" UI Text References")]
    public Text normalHitsValue;
    public Text goodHitsValue;
    public Text perfectHitsValue;
    public Text missedHitsValue;
    public Text percentHitValue;
    public Text rankValue;
    public Text finalScoreValue;

    [Header(" Score Source")]
    public Text scoreText;

    [Header(" Settings")]
    public float checkInterval = 0.5f;




    // Static counters
    public static int normalHits, goodHits, perfectHits, missedHits;

    public static bool gameEnded = false;
    private float timer = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        if (healthSystem == null)
            healthSystem = FindObjectOfType<HealthSystem>();

        if (resultsPanel != null)
            resultsPanel.SetActive(false);

        normalHits = goodHits = perfectHits = missedHits = 0;
    }

    void Update()
    {
        if (gameEnded || healthSystem == null)
            return;

        if (HealthSystem.currentHealth <= 0)
            return;

        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            UpdateResultTexts();

            if (AllNotesCleared()) { 
             
             gameEnded = true;
             Time.timeScale = 1f;
             PersistentCanvas.HideAll();
             LoadSceneManager loadScene = FindObjectOfType<LoadSceneManager>();
              loadScene.EndGame();
            }

         }
    }

    public static void RegisterEffect(string tag)
    {
        switch (tag)
        {
            case "HitEffect": normalHits++; break;
            case "GoodEffect": goodHits++; break;
            case "PerfectEffect": perfectHits++; break;
            case "MissEffect": missedHits++; break;
        }
    }

    private bool AllNotesCleared()
    {
        foreach (string tag in noteTags)
        {
            if (GameObject.FindGameObjectsWithTag(tag).Length > 0)
                return false;
        }
        return true;
    }

   

    public void UpdateResultTexts()
    {
        if (normalHitsValue) normalHitsValue.text = normalHits.ToString();
        if (goodHitsValue) goodHitsValue.text = goodHits.ToString();
        if (perfectHitsValue) perfectHitsValue.text = perfectHits.ToString();
        if (missedHitsValue) missedHitsValue.text = missedHits.ToString();

        float percent = AccuracySystem.finalAccuracyScore ;
        if (percentHitValue) percentHitValue.text = percent.ToString("F1") + "%";

        string rank = "F";
        if (percent >= 95) rank = "S";
        else if (percent >= 85) rank = "A";
        else if (percent >= 70) rank = "B";
        else if (percent >= 50) rank = "C";
        else rank = "D";

        if (rankValue) rankValue.text = rank;
        if (finalScoreValue && scoreText) finalScoreValue.text = GameManager.currentScore.ToString();
    }
}
