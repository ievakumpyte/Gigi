using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    public GameObject GameOverUI;

    [SerializeField] private AudioSource gameover;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameover.Play();
            EndGame();
          
        }
      
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        GameOverUI.SetActive(true);
    }
}
