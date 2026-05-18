using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public int initialAmount; 
    public float areaSize;
    public float spawnInterval = 2f; 

    private float timer;

    void Start()
    {
        for (int i = 0; i < initialAmount; i++)
        {
            SpawnFood();
        }

        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnFood();
            timer = spawnInterval;
        }
    }

    public void SpawnFood()
    {
        Vector3 pos = new Vector3(
            Random.Range(-areaSize, areaSize),
            0,
            Random.Range(-areaSize, areaSize)
        );
        Instantiate(foodPrefab, pos, Quaternion.identity);
    }
}
