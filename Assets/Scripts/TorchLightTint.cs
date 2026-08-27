using UnityEngine;

public class TorchLightTint : MonoBehaviour
{
    [SerializeField] private Transform playerIconObject;
    [SerializeField] private Color peakTorchColor = new Color(1f, 0.6f, 0.2f, 1f);
    [SerializeField] private float glowRadius = 5f;

    private SpriteRenderer iconRenderer;
    private Color originalColor;
    private GameObject[] allTorches;

    private void Start()
    {
        if (playerIconObject != null)
        {
            iconRenderer = playerIconObject.GetComponent<SpriteRenderer>();
            if (iconRenderer != null)
            {
                originalColor = iconRenderer.color;
            }
        }
    }

    private void Update()
    {
        if (iconRenderer == null || allTorches == null || allTorches.Length == 0) return;

        Vector2 playerPos = transform.position;
        float closestDistance = float.MaxValue;

        foreach (GameObject torch in allTorches)
        {
            if (torch == null) continue;

            float distance = Vector2.Distance(playerPos, torch.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }

        if (closestDistance <= glowRadius)
        {
            float lightIntensity = 1f - (closestDistance / glowRadius);

            iconRenderer.color = Color.Lerp(originalColor, peakTorchColor, lightIntensity);
        }
        else
        {
            iconRenderer.color = originalColor;
        }
    }
}