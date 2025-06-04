using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Значение монетки
    public AudioClip coinCollectSound; // Звук при сборе монетки

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, если столкнулись с игроком
        if (collision.CompareTag("Player"))
        {
            // Воспроизводим звук сбора монетки, если есть
            if (coinCollectSound != null)
            {
                AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
            }

            // Добавляем очки, уничтожаем монетку
            // GameManager.instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
