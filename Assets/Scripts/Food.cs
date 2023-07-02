using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    private float countUp = 0;
    private float waitTime = 5.0f;

    [SerializeField] private string activePowerUp = null;

    private void Update()
    {
        countUp += Time.deltaTime;

        if (countUp >= waitTime)
        {
            FoodSpawner.Instance.SpawnFoodAndPowerUp();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController snakeController = collision.gameObject.GetComponent<SnakeController>();
        SecondSnakeController secondSnakeController = collision.gameObject.GetComponent<SecondSnakeController>();

        switch (activePowerUp)
        {
            case "SHIELD":
                if (snakeController != null)
                    snakeController.SetShieldBool(true);
                else if (secondSnakeController != null)
                    secondSnakeController.SetShieldBool(true);
                break;
            case "SCORE":
                if (snakeController != null)
                    snakeController.StartCoroutine(snakeController.XScore());
                else if (secondSnakeController != null)
                    secondSnakeController.StartCoroutine(secondSnakeController.XScore());
                break;
            case "SPEED":
                StartCoroutine(ChangeTimeStamp());
                break;
            default:
                break;
        }

        if (collision.CompareTag("Player"))
        {
            FoodSpawner.Instance.SpawnFoodAndPowerUp();
            Destroy(this.gameObject);
        }
    }

    IEnumerator ChangeTimeStamp()
    {
        Time.fixedDeltaTime = 0.04f;

        yield return new WaitForSeconds(10f);

        Time.fixedDeltaTime = 0.08f;
    }
}
