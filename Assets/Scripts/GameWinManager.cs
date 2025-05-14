using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinManager : MonoBehaviour
{
    [SerializeField] GameObject gameWinUI;

    void Start()
    {
        gameWinUI.SetActive(false); // T?t UI game over khi b?t ??u
    }

    public void ShowGameWin()
    {
        gameWinUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
