using System.Drawing;
using UnityEngine;

public class DoorButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject DoorObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyDoor();
        }
    }

    private void DestroyDoor()
    {
        if (DoorObject != null)
        {
            Destroy(DoorObject);
        }
    }
}
