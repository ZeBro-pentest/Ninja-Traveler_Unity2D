using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float pushForce; // Сила отталкивания по оси X

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRigidbody != null)
            {
                // Определяем направление отталкивания по оси X
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;

                // Применяем силу отталкивания
                playerRigidbody.velocity = Vector2.zero; // Обнуляем текущую скорость игрока
                playerRigidbody.AddForce(new Vector2(pushDirection.x * 1, 0) * pushForce, ForceMode2D.Impulse); // Добавляем силу отталкивания по оси X
            }
        }
    }
}
