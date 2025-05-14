using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider musicSlider;

    private float previousVolume;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

        // Lưu giá trị âm lượng và giảm về 0
        previousVolume = musicSlider.value;
        musicSlider.value = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        // Khôi phục lại âm lượng
        musicSlider.value = previousVolume;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
