using System.Collections;
using UnityEngine;

public class RandomGameEvents : MonoBehaviour
{
    public GameObject windAnimation;
    public GameObject border;
    public float windForce = 5f;
    public Transform respawnPoint1, respawnPoint2;
    public GameObject player1, player2;

    private Rigidbody2D rb1, rb2;

    void Start()
    {
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();

        int randomEvent = Random.Range(0, 2); // Randomly choose between 0 (Wind) or 1 (Border)

        if (randomEvent == 0)
        {
            StartCoroutine(WindEvent());
        }
        else
        {
            ActivateBorderEvent();
        }
    }

    IEnumerator WindEvent()
    {
        windAnimation.SetActive(true); // Play wind animation
        yield return new WaitForSeconds(1f); // Wait for animation to start

        if (player1.CompareTag("Player1"))
        {
            rb1.AddForce(Vector2.right * windForce, ForceMode2D.Impulse);
        }
        if (player2.CompareTag("Player2"))
        {
            rb2.AddForce(Vector2.right * windForce, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(3f); // Let wind effect persist
        windAnimation.SetActive(false);
    }

    void ActivateBorderEvent()
    {
        border.SetActive(true); // Enable the border that destroys players
    }
}

public class BorderCollision : MonoBehaviour
{
    public Transform respawnPoint1, respawnPoint2;
    public GameObject player1, player2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            Respawn(player1, respawnPoint1);
        }
        else if (collision.CompareTag("Player2"))
        {
            Respawn(player2, respawnPoint2);
        }
    }

    void Respawn(GameObject player, Transform respawnPoint)
    {
        player.transform.position = respawnPoint.position;
    }
}
