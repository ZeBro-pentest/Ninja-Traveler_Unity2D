using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Массив точек спавна

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        // Выбираем случайную точку спавна из массива
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Перемещаем объект на выбранную точку спавна
        transform.position = spawnPoint.position;
    }
}
