using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    private float countUp = 0;
    private float waitTime = 5.0f;

    [SerializeField]
    private string activePowerUp = null;

    private void Update()
    {
        countUp += Time.deltaTime;

        if(countUp >= waitTime)
        {
            FoodSpawner.Instance.SpawnFoodAndPowerUp();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(activePowerUp)
        {
            case "SHIELD":
                collision.gameObject.GetComponent<SnakeController>().setShieldBool(true);
                break;
            case "SCORE":
                collision.gameObject.GetComponent<SnakeController>().xScore();
                break;
            case "SPEED":
                StartCoroutine(ChangeTimeStamp());
                break;
            default:
                break;
        }


        if(collision.CompareTag("Player"))
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
