using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnObject : MonoBehaviour
{
    public GameObject[] objectsToRespawn; // Массив объектов для выбора случайного
    private GameObject respawnedObject; // Переменная для текущего экземпляра объекта
    private bool hasRespawned; // Флаг для отслеживания состояния восстановления

    void Start()
    {
        Respawn(); // Вызываем метод для создания объекта при запуске
    }

    void FixedUpdate()
    {
        // Проверяем, если объект удален и он еще не был восстановлен
        if (respawnedObject == null && !hasRespawned)
        {
            Invoke("Respawn", 8f); // Запускаем восстановление через 5 секунд
            hasRespawned = true; // Устанавливаем флаг в true, чтобы не восстанавливать больше одного раза
        }
    }

    void Respawn()
    {
        // Выбираем случайный объект из массива objectsToRespawn
        int randomIndex = Random.Range(0, objectsToRespawn.Length);
        GameObject objectToSpawn = objectsToRespawn[randomIndex];

        // Создаем новый экземпляр выбранного объекта для восстановления
        respawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
