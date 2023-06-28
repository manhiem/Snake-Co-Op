using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector3 moveVector = Vector3.left;

    private void Update()
    {
        MoveController();
        transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void MoveController()
    {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0f)
            {
                moveVector = new Vector3(horizontalInput, 0f, 0f);
            }
            else if (verticalInput != 0f)
            {
                moveVector = new Vector3(0f, verticalInput, 0f);
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
