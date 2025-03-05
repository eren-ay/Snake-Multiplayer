using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private float baseSpeed = 5f;
    //[SerializeField]//for unity controled variables
    private float speedIncreasePerDifficulty = 5f;
    private int difficultyLevel = 1;
    private float currentSpeed;
    private Vector2 direction = Vector2.right;

    private float nextMoveTime;
    private float moveInterval = 0.1f;

    void Start()
    {
        UpdateCurrentSpeed();

        nextMoveTime = Time.time;
    }

    void Update()
    {
        HandleInput();

        if (Time.time >= nextMoveTime)
        {
            Move();
            nextMoveTime = Time.time + moveInterval;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction != Vector2.down)
            {
                direction = Vector2.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction != Vector2.up)
            {
                direction = Vector2.down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction != Vector2.right) 
            {
                direction = Vector2.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction != Vector2.left) 
            {
                direction = Vector2.right;
            }
        }
    }

    
    void Move()
    {
        transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
    }

    public void SetDifficultyLevel(int level = 1)
    {
        difficultyLevel = level;
        UpdateCurrentSpeed();
    }

    void UpdateCurrentSpeed()
    {
        currentSpeed = baseSpeed + ((difficultyLevel-1) * speedIncreasePerDifficulty);
        moveInterval = 1f / currentSpeed; 

        //Debug.Log("difficultyLevel: " + difficultyLevel + ",speedIncreasePerDifficulty: " + speedIncreasePerDifficulty); 
        //Debug.Log("Current Speed: " + currentSpeed + ", Move Interval: " + moveInterval); 
    }

    public void SetSpeed(float newSpeed)
    {
        baseSpeed = newSpeed;
        UpdateCurrentSpeed();
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public int GetDifficultyLevel()
    {
        return difficultyLevel;
    }
}