using System;
using UnityEngine;

public class DoorButtonScript : MonoBehaviour
{
    [SerializeField] private AudioClip PressSound;
    [SerializeField] private GameObject DoorObject;
    [SerializeField] private float pushDistance = 0.2f;

    private bool isPressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;

            ChangeAllChildrenColors();
            PushButtonRight();
            DestroyDoor();
        }
    }

    private void ChangeAllChildrenColors()
    {
        SpriteRenderer[] childRenderes = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in childRenderes)
        {
            if (sprite.gameObject.name.Contains("Light"))
            {
                sprite.color = Color.green;
            }
            else if (sprite.gameObject.name.Contains("Dark"))
            {
                sprite.color = new Color(0f, 0.4f, 0f);
            }
            else
            {
                sprite.color = Color.green;
            }
        }
    }

    private void PushButtonRight()
    {
        transform.position += Vector3.right * pushDistance;
    }

    private void DestroyDoor()
    {
        if (DoorObject != null)
        {
            AudioManager.instance.PlaySound(PressSound);
            Destroy(DoorObject);
        }
    }
}
