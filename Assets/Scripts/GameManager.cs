using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public Text endGameText;
    public Text pointsText;

    public void GameOver()
    {
        endGamePanel.SetActive(true);
        ShowEndGameMessage();
    }

    void ShowEndGameMessage()
    {
        endGameText.text = "Game over!\nYour score: " + pointsText.text;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
