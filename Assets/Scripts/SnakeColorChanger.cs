using UnityEngine;

public class SnakeColorChanger : MonoBehaviour
{
    private SpriteRenderer snakeRenderer;

    private void Start()
    {
        snakeRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeSnakeColor", 0f, 1f);
    }

    private void ChangeSnakeColor()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        snakeRenderer.color = randomColor;
    }
}
