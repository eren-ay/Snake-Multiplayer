using UnityEditor;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab; 
    public int maxFoodCount = 3;   
    public float spawnAreaRadius = 5f; 
    public BoxCollider2D gridArea;
    void Start()
    {
        SpawnRandomFood(); 
    }

    public void SpawnRandomFood()
    {
        
        GameObject[] existingFoods = GameObject.FindGameObjectsWithTag("Food"); 
        int currentFoodCount = existingFoods.Length;


        if (currentFoodCount < maxFoodCount)
        {
            int foodToSpawn = Random.Range(1, maxFoodCount - currentFoodCount + 1); 

            for (int i = 0; i < foodToSpawn; i++)
            {
                SpawnFood();
            }
        }
    }

    void SpawnFood()
    {
        if (foodPrefab != null)
        {
            Vector3 randomizedPosition = randomizePosition();
            
            GameObject newFood = Instantiate(foodPrefab, randomizedPosition, Quaternion.identity);

            newFood.tag = "Food";
            newFood.GetComponent<Food>().gridArea = gridArea;
        }
    }

    public Vector3 randomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
    
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }
}