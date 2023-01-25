using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverUI : MonoBehaviour
{
    public Text scoreResult;

    public void Awake()
    {
        scoreResult.text = "SCORE:" + ScoreScript.scoreValue.ToString();
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();

    }

    public void Retry()
    {
        ScoreScript.scoreValue = 0;
        PlayerController.balloons = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        //Time.timeScale = 1f;
        PlayerController.balloons = 0;
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene("Menu"); 
    }


}
