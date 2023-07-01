using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    [SerializeField]
    private List<Transform> segmentsList;
    [SerializeField]
    private Transform segmentPrefab;

    [SerializeField]
    private bool isShield;

    [SerializeField]
    private Text scoreText;
    private int scoreValue;
    private int value = 1;

    private void Start()
    {
        scoreText.text = "Score: 0";
        segmentsList = new List<Transform>
        {
            this.transform
        };
    }

    private Dictionary<KeyCode, Vector2> keyDirectionMap = new Dictionary<KeyCode, Vector2>()
    {
        { KeyCode.W, Vector2.up },
        { KeyCode.S, Vector2.down },
        { KeyCode.A, Vector2.left },
        { KeyCode.D, Vector2.right }
    };

    private void Update()
    {
        foreach (var kvp in keyDirectionMap)
        {
            if (Input.GetKeyDown(kvp.Key) && _direction != -kvp.Value)
            {
                _direction = kvp.Value;
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        for(int i=segmentsList.Count-1; i>0; i--)
        {
            segmentsList[i].position = segmentsList[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segmentsList[segmentsList.Count - 1].position;

        segmentsList.Add(segment);

        if (segmentsList.Count > 5)
        {
            FoodSpawner.Instance.spawnBoth = true;
        }
    }

    private void Decrease()
    {
        GameObject lastSegment = segmentsList[segmentsList.Count - 1].gameObject;
        GameObject.Destroy(lastSegment);
        segmentsList.RemoveAt(segmentsList.Count - 1);

        if (segmentsList.Count < 5)
        {
            FoodSpawner.Instance.spawnBoth = false;
        }
    }

    public void setShieldBool(bool shieldValue)
    {
        isShield = shieldValue;
    }

    public IEnumerator xScore()
    {
        value = 2;

        yield return new WaitForSeconds(10f);

        value = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "GrowFood":
                UpdateScore(value);
                Grow();
                break;
            case "DecreaseFood":
                UpdateScore(value);
                Decrease();
                break;
            case "Player":
                if (!isShield)
                {
                    SceneManager.LoadScene(2);
                }
                isShield = false;
                break;
            case "Border":
                SceneManager.LoadScene(2);
                break;
            default: break;
        }
    }

    private void UpdateScore(int scoreIncrement)
    {
        scoreValue += scoreIncrement;
        scoreText.text = "Score: " + scoreValue.ToString();
    }

}
