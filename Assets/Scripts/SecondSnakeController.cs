using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondSnakeController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private Transform _transform;
    private Text _scoreText;
    private bool isCoop = false;
    [SerializeField] private GameObject winsModal;
    [SerializeField] private Text winsText;

    [SerializeField] private List<Transform> segmentsList;
    [SerializeField] private Transform segmentPrefab;

    [SerializeField] private bool isShield;
    [SerializeField] private int scoreValue;
    [SerializeField] private Text scoreText;
    private int value = 1;

    private readonly Dictionary<KeyCode, Vector2> keyDirectionMap = new Dictionary<KeyCode, Vector2>()
    {
        { KeyCode.W, Vector2.up },
        { KeyCode.S, Vector2.down },
        { KeyCode.A, Vector2.left },
        { KeyCode.D, Vector2.right }
    };

    private void Start()
    {
        _transform = transform;
        _scoreText = scoreText.GetComponent<Text>();
        _scoreText.text = "Score: 0";
        segmentsList = new List<Transform> { _transform };

        isCoop = SceneManager.GetActiveScene().buildIndex == 6;
    }

    private void Update()
    {
        foreach (var kvp in keyDirectionMap)
        {
            if (Input.GetKeyDown(kvp.Key) && _direction != -kvp.Value)
            {
                _direction = kvp.Value;
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = segmentsList.Count - 1; i > 0; i--)
        {
            segmentsList[i].position = segmentsList[i - 1].position;
        }

        Vector2 newPosition = new Vector2(
            Mathf.Round(_transform.position.x) + _direction.x,
            Mathf.Round(_transform.position.y) + _direction.y
        );

        _transform.position = newPosition;
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segmentsList[segmentsList.Count - 1].position;
        segmentsList.Add(segment);
        CheckSpawnBoth();
    }

    private void Decrease()
    {
        GameObject lastSegment = segmentsList[segmentsList.Count - 1].gameObject;
        Destroy(lastSegment);
        segmentsList.RemoveAt(segmentsList.Count - 1);
        CheckSpawnBoth();
    }

    private void CheckSpawnBoth()
    {
        FoodSpawner.Instance.spawnBoth = segmentsList.Count > 5;
    }

    public void SetShieldBool(bool shieldValue)
    {
        isShield = shieldValue;
    }

    public IEnumerator XScore()
    {
        value += 1;
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
                if (isCoop && !isShield)
                {
                    winsModal.SetActive(true);
                    winsText.text = "Player1 Wins!!!";
                }
                else if (!isCoop && !isShield)
                {
                    SceneManager.LoadScene(7);
                }
                isShield = false;
                break;
            case "Border":
                if (isCoop)
                {
                    winsModal.SetActive(true);
                    winsText.text = "Player1 Wins!!!";
                }
                SceneManager.LoadScene(7);
                break;
            default:
                break;
        }
    }

    private void UpdateScore(int scoreIncrement)
    {
        scoreValue += scoreIncrement;
        _scoreText.text = "Score: " + scoreValue.ToString();
    }
}
