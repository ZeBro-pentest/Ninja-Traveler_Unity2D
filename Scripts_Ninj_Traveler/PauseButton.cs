using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu; // Ссылка на меню паузы

    private bool isPaused = false; // Флаг, указывающий, находится ли игра на паузе

    void Start()
    {
        // Скрываем меню паузы при запуске игры
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        // Инвертируем флаг паузы
        isPaused = !isPaused;

        if (isPaused)
        {
            // Если игра на паузе, останавливаем время и показываем меню паузы
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            // Если игра не на паузе, возобновляем время и скрываем меню паузы
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }
}
