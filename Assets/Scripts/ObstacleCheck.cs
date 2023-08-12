using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCheck : MonoBehaviour
{
    public static Action ObstacleTouched;

    PlayerController controller;
    private void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ObstacleTouched?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Zone2"))
        {
            GameManager.instance.ChangeGameStatus(gameStatus.Gravity);
            controller.ChangeCameraColor();
        }
        else if(collision.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.SetGameMode(GameMode.Finish);

        }
    }
}
