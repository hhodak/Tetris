using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public Text endGameText;
    public Text pointsText;
    public AudioMixer mixer;
    bool mute = false;

    public void Mute()
    {
        mute = !mute;
        if (mute)
        {
            mixer.SetFloat("volume", -80f);
        }
        else
        {
            mixer.SetFloat("volume", 0f);
        }
    }

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
