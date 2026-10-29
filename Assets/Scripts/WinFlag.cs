using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinFlag : MonoBehaviour
{
    [SerializeField] private GameObject WinMenuPanel;
    [SerializeField] private Image starImage1;
    [SerializeField] private Image starImage2;
    [SerializeField] private Image starImage3;
    [SerializeField] private Sprite filledStarSprite;
    [SerializeField] private AudioClip victoryHurraySound;
    [SerializeField] private float starFillDelay = 0.4f;
    private bool hasWon = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasWon)
        {
            hasWon = true;

            PlayerMovement movement = collision.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.StopMovementForWin();
            }

            AudioManager.instance.PlaySound(victoryHurraySound);

            CoinCollector collector = collision.GetComponent<CoinCollector>();
            int finalCoins = 0;
            if (collector != null)
            {
                finalCoins = collector.coinCount;
            }

            StartCoroutine(AnimateWinMenu(finalCoins));
        }
    }

    private IEnumerator AnimateWinMenu(int score)
    {
        WinMenuPanel.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        if (score >= 1)
        {
            starImage1.sprite = filledStarSprite;

            yield return new WaitForSeconds(starFillDelay);
        }

        if (score >= 2)
        {
            starImage2.sprite = filledStarSprite;

            yield return new WaitForSeconds(starFillDelay);
        }

        if (score >= 3)
        {
            starImage3.sprite = filledStarSprite;

            yield return new WaitForSeconds(starFillDelay);
        }



        Debug.Log("StarAnimationComplete,Innit!");
    }
}
