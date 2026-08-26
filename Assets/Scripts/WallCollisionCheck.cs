using UnityEngine;

public class WallCollisionCheck : MonoBehaviour
{
    private int colliderCounter;
    public bool onWall;
    public int wallDirection;

    private void Update()
    {
        if (colliderCounter > 0)
        {
            onWall = true;
        }
        else
        {
            onWall = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Unhuggable"))
        {
            colliderCounter++;

            if (collision.transform.position.x > transform.position.x) //Hvis væggen vi rammer, har en større X-værdi end spilleren
            {
                wallDirection = 1; //Så er væggen til højre for spilleren, og wallDirection er derfor 1
            }
            else
            {
                wallDirection = -1; //Hvis ikke x-værdien på væggen er højere end spillerens, må væggen være til venstre, og derfor er wallDirection -1
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Unhuggable"))
        {
            colliderCounter--;
        }
    }
}
