using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    private List<Transform> _segments;
    public Transform segmentPrefab;

    void Start()
    {
        UpdateCurrentSpeed();

        nextMoveTime = Time.time;

        _segments = new List<Transform>();
        _segments.Add(this.transform);
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
        Vector3 tempPosition = transform.position;
        Vector3 tempPositionPrev = Vector3.zero;
        transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        for (int i = 1; i < _segments.Count; i++)
        {
            tempPositionPrev = tempPosition;
            tempPosition = _segments[i].position;
            _segments[i].position = tempPositionPrev;
        }
        
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

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        //TO DO Multiplayer feature: change segment tag for which snake hit which one 
        segment.position = new Vector3(99999, 99999, 0);
        _segments.Add(segment);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food"){
            Grow();
        }else if (collision.tag == "Wall") //|| collision.tag == "Player")
        {
            GameOver();
        }else if (collision.tag == "Obstacle")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        for (int i = 1; i<_segments.Count; i++){
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        this.transform.position = Vector3.zero;
        direction = Vector2.right;
    }
}