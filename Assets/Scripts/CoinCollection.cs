using System.Collections;
using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private float colorFadeSpeed = 2f;
    private int coinCount = 0;
    private Coroutine colorRoutine;

    private void Start()
    {
        coinText.text = "Coins: " + coinCount + "/3";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            AudioManager.instance.PlaySound(coinSound);
            Destroy(other.gameObject);
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        coinCount++;
        coinText.text = "Coins: " + coinCount + "/3";

        if (colorRoutine != null)
        {
            StopCoroutine(colorRoutine);
        }
        colorRoutine = StartCoroutine(FlashAndFadeTextColor());
    }

    private IEnumerator FlashAndFadeTextColor()
    {
        coinText.color = Color.green;

        if(coinCount < 3)
        {
            yield return new WaitForSeconds(0.1f);

            while (coinText.color != Color.white)
            {
                coinText.color = Color.Lerp(coinText.color, Color.white, colorFadeSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}