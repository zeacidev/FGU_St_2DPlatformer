using UnityEngine;

public class GroundCollisionCheck : MonoBehaviour
{
    private int colliderCounter;
    public bool isGrounded;
    private void Update()
    {
        if (colliderCounter > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderCounter++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        colliderCounter--;
    }
}
