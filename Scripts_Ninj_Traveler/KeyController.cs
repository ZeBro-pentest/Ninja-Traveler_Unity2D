using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float distanceToRemove = 1f; // Расстояние, на котором ключ будет удален
    public KeyCode removeKeyButton = KeyCode.Mouse0; // Кнопка мыши для удаления ключа
    public AudioClip removeSound; // Звуковой эффект для воспроизведения при удалении ключа
    public GameObject gate1;

    private bool keyInRange1 = false;// Флаг для определения нахождения ключа в зоне действия
    private AudioSource audioSource; // Компонент для воспроизведения звуков

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
    }

    void Update()
    {
        // Проверяем расстояние между ключом и игроком
        if (Vector2.Distance(transform.position, player.position) <= distanceToRemove)
        {
            keyInRange1 = true;
        }
        else
        {
            keyInRange1 = false;
        }

        // Удаляем ключ при нажатии на кнопку мыши, если игрок находится в зоне действия
        if (keyInRange1 && Input.GetKeyDown(removeKeyButton))
        {
            // Проигрываем звук удаления объекта
            if (removeSound != null)
            {
                AudioSource.PlayClipAtPoint(removeSound, transform.position);
            }

            // Удаляем объект
            Destroy(gameObject);
            Destroy(gate1);
        }
    }
}
