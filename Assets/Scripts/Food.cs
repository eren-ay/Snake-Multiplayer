using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        randomizePosition();
        //Destroy(gameObject);
    }

    public void randomizePosition()
    {
        Bounds bounds = gridArea.bounds;
    
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }
}
