using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    [SerializeField]
    private List<Transform> segmentsList;
    [SerializeField]
    private Transform segmentPrefab;

    private void Start()
    {
        segmentsList = new List<Transform>();
        segmentsList.Add(this.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        { 
            _direction = Vector2.right;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        } 
        else if (collision.CompareTag("Border"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
