using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TheEnd : MonoBehaviour
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
        SceneManager.LoadScene("SampleScene");
    }

    public void Menu()
    {
        ScoreScript.scoreValue = 0;
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
