using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject levelCompletePanel;

    [Header("Text")]
    [SerializeField] private TextMeshPro[] tips;
    [SerializeField] private TextMeshPro attempText;
    
    PlayerController controller;


    bool initialTips = true;
    int attempCount = 1;
    private void Start()
    {
        ObstacleCheck.ObstacleTouched += SetUI;
        GameManager.onLevelFinished += LevelComplete;
        controller = FindObjectOfType<PlayerController>();

    }
    private void OnDestroy()
    {
        ObstacleCheck.ObstacleTouched -= SetUI;
        GameManager.onLevelFinished -= LevelComplete;

    }
    public void ClosePanel()
    {
        menuPanel.SetActive(false);
        GameManager.instance.SetGameMode(GameMode.Move);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    void SetUI()
    {
        attempCount++;
        attempText.text = "Attempt " + attempCount.ToString();
        if (controller.gravityZoneTouched)
        {
            for (int i = 0; i < tips.Length; i++)
            {
                tips[i].gameObject.SetActive(false);
            }
        }
       
    }

    void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
    }
}
