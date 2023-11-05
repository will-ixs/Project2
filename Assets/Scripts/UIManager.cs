using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI gameStateText;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentGameState == GameManager.State.StartScreen)
        {
            //button is disabled in GameManager
            gameStateText.enabled = false;
        }
        if (gameManager.currentGameState == GameManager.State.PlacingStart)
        {
            gameStateText.enabled = true;
            gameStateText.text = "Place Start Line";
        }
        if (gameManager.currentGameState == GameManager.State.PlacingFinish)
        {
            gameStateText.text = "Place Finish Line";
        }
        if (gameManager.currentGameState == GameManager.State.PlacingPlatforms)
        {
            gameStateText.text = "Place" + (5 - (gameManager.platformCount)) + "Platforms";
        }
        if (gameManager.currentGameState == GameManager.State.Playing)
        {
            gameStateText.text = "Jump on platforms!";
        }
        if (gameManager.currentGameState == GameManager.State.GameOver)
        {
            gameStateText.enabled = false;
            gameOver.enabled = true;
        }
    }
}
