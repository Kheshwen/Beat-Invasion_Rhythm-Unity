using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection
{
    Static,
    Right, UpRight, Up, UpLeft, Left, DownLeft, Down, DownRight
}

public class Hit : MonoBehaviour
{
    private Vector2 currentVelocity;
    private Vector2 screenMin;
    private Vector2 screenMax; 

    private bool wasHit = false;


    public int hitPoints;
    public float moveSpeed = 3f;

    private SpriteRenderer Renderer;
    private MovementDirection moveDirection;
    private float timeAlive = 0f;
    private int startingHitPoints;
    private int clickCount = 0;

    public Color OneHit;
    public Color TwoHit;
    public Color ThreeHit;
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();

        if (Camera.main == null)
        {
            Debug.LogError("ERROR: You MUST tag your main camera as 'MainCamera' in the Inspector.");
            return;
        }

        Vector2 camPos = Camera.main.transform.position;
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        screenMin = new Vector2(camPos.x - camWidth, camPos.y - camHeight);
        screenMax = new Vector2(camPos.x + camWidth, camPos.y + camHeight);
    }

    public void Initialize(int hp, MovementDirection dir)
    {
        timeAlive = 0f;
        this.hitPoints = hp;
        this.startingHitPoints = hp;
        this.moveDirection = dir;

        Renderer = GetComponent<SpriteRenderer>();
        if (Renderer != null)
        {
            if (hp == 1) Renderer.color = OneHit;
            else if (hp == 2) Renderer.color = TwoHit;
            else if (hp >= 3) Renderer.color = ThreeHit;
        }

        Vector2 directionVector = Vector2.zero;
        switch (dir)
        {
            case MovementDirection.Right: directionVector = Vector2.right; break;
            case MovementDirection.UpRight: directionVector = new Vector2(1, 1).normalized; break;
            case MovementDirection.Up: directionVector = Vector2.up; break;
            case MovementDirection.UpLeft: directionVector = new Vector2(-1, 1).normalized; break;
            case MovementDirection.Left: directionVector = Vector2.left; break;
            case MovementDirection.DownLeft: directionVector = new Vector2(-1, -1).normalized; break;
            case MovementDirection.Down: directionVector = Vector2.down; break;
            case MovementDirection.DownRight: directionVector = new Vector2(1, -1).normalized; break;
        }

        currentVelocity = directionVector * moveSpeed;
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        if (moveDirection == MovementDirection.Static)
            return;

        Vector3 newPos = transform.position + (Vector3)currentVelocity * Time.deltaTime;

        float noteHalfWidth = Renderer.bounds.size.x / 2;
        float noteHalfHeight = Renderer.bounds.size.y / 2;

        if (newPos.x + noteHalfWidth > screenMax.x)
        {
            newPos.x = screenMax.x - noteHalfWidth;
            currentVelocity.x *= -1;
        }
        else if (newPos.x - noteHalfWidth < screenMin.x)
        {
            newPos.x = screenMin.x + noteHalfWidth;
            currentVelocity.x *= -1;
        }

        if (newPos.y + noteHalfHeight > screenMax.y)
        {
            newPos.y = screenMax.y - noteHalfHeight;
            currentVelocity.y *= -1;
        }
        else if (newPos.y - noteHalfHeight < screenMin.y)
        {
            newPos.y = screenMin.y + noteHalfHeight;
            currentVelocity.y *= -1;
        }

        transform.position = newPos;
    }

    private void OnMouseDown()
    {
        wasHit = true;

        ScoreManager.instance.noteWasHitThisFrame = true;
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;
        clickCount++;

        switch (startingHitPoints)
        {
            
            case 1:
                if (clickCount == 1)
                {
                    FindObjectOfType<BeatSpawner>().SpawnJudgment(FindObjectOfType<BeatSpawner>().hitPrefab, clickPosition);
                    ResultSystem.normalHits++;
                 
                    ScoreManager.instance.AddPoint(100);


                    //Accuracy system
                    AccuracySystem.NormalNote();
                    AccuracySystem.AddNote();
                    Destroy(gameObject);

                }
                break;

            
            case 2:
                if (clickCount == 1)
                {
                    FindObjectOfType<BeatSpawner>().SpawnJudgment(FindObjectOfType<BeatSpawner>().hitPrefab, clickPosition);
                    ResultSystem.normalHits++;
                   
                    ScoreManager.instance.AddPoint(100);
                    Renderer.color = OneHit;

                    AccuracySystem.NormalNote();
                    AccuracySystem.AddNote();
                }
                else if (clickCount == 2)
                {
                    FindObjectOfType<BeatSpawner>().SpawnJudgment(FindObjectOfType<BeatSpawner>().goodPrefab, clickPosition);
                    ResultSystem.goodHits++;
                   
                    ScoreManager.instance.AddPoint(125);



                    AccuracySystem.GoodNote();
                    AccuracySystem.AddNote();
                    Destroy(gameObject);
                }
                break;

            

            case 3:
                if (clickCount == 1)
                {
                    FindObjectOfType<BeatSpawner>().SpawnJudgment(FindObjectOfType<BeatSpawner>().hitPrefab, clickPosition);
                    ResultSystem.normalHits++;
                   
                    ScoreManager.instance.AddPoint(100);
                    Renderer.color = TwoHit;

                    AccuracySystem.NormalNote();
                    AccuracySystem.AddNote();
                }
                else if (clickCount == 2)
                {
                    FindObjectOfType<BeatSpawner>().SpawnJudgment(FindObjectOfType<BeatSpawner>().goodPrefab, clickPosition);
                    ResultSystem.goodHits++;
                 
                    ScoreManager.instance.AddPoint(125);
                    Renderer.color = OneHit;

                    AccuracySystem.GoodNote();
                    AccuracySystem.AddNote();
                }
                else if (clickCount == 3)
                {
                    FindObjectOfType<BeatSpawner>().SpawnJudgment(FindObjectOfType<BeatSpawner>().perfectPrefab, clickPosition);
                    ResultSystem.perfectHits++;
                   
                    ScoreManager.instance.AddPoint(150);



                    AccuracySystem.PerfectNote();
                    AccuracySystem.AddNote();
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnDestroy()
    {
       
        if (!wasHit)
        {
            ResultSystem.missedHits++;
            AccuracySystem.MissNote();
            AccuracySystem.AddNote();
        }
    }

}
