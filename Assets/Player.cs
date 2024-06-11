using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController gameController;
    private Vector3 originalSize;
    public float shrinkScale = 0.5f;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        originalSize = transform.localScale;
    }

    public void StartShrink(float duration)
    {
        transform.localScale = originalSize * shrinkScale;
        Invoke("ResetSize", duration);
    }

    void ResetSize()
    {
        transform.localScale = originalSize;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            gameController.GameOver();
        }
    }
}
