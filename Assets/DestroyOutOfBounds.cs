using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 1)  // Adjust as necessary
        {
            Destroy(gameObject);
        }
    }
}
