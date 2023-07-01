using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private BoxCollider2D spawnArea;
    [SerializeField]
    private GameObject[] foodPrefab;
    [SerializeField]
    private GameObject[] powerUpPrefab;

    public bool spawnBoth = false;

    private void Start()
    {
        StartCoroutine(SpawnFoodAndPowerUp());
    }

    public IEnumerator SpawnFoodAndPowerUp()
    {
        while (true)
        {
            SpawnFood();

            if (ShouldSpawnPowerUp())
            {
                yield return new WaitForSeconds(Random.Range(3f, 7f));
                SpawnPowerUp();
            }

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    private void SpawnFood()
    {
        Vector2 randomPosition = GetRandomPosition();
        int foodIndex = spawnBoth ? Random.Range(0, 2) : 0;
        Instantiate(foodPrefab[foodIndex], randomPosition, Quaternion.identity);
    }

    private void SpawnPowerUp()
    {
        Vector2 randomPosition = GetRandomPosition();
        int powerUpIndex = spawnBoth ? Random.Range(0, 3) : 0;
        Instantiate(powerUpPrefab[powerUpIndex], randomPosition, Quaternion.identity);
    }

    private Vector2 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }

    private bool ShouldSpawnPowerUp()
    {
        int probability = Random.Range(0, 11);
        return probability >= 7;
    }
}
