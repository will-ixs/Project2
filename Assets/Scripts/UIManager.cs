using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] Frog frog;
    [SerializeField] private TextMeshProUGUI gameOver;
    [SerializeField] private TextMeshProUGUI gameStateText;
    [SerializeField] private Image heart;
    [SerializeField] private TextMeshProUGUI livesCounter;
    [SerializeField] private TextMeshProUGUI inRange;
    [SerializeField] private Image charge;
    [SerializeField] private Image chargeBar;


    // Start is called before the first frame update
    void Start()
    {
        gameOver.enabled = false;
        charge.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentGameState == GameManager.State.StartScreen)
        {
            //button is disabled in GameManager
            gameStateText.enabled = false;
            heart.enabled = false;
            livesCounter.enabled = false;
            inRange.enabled = false;
            chargeBar.enabled = false;
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
            heart.enabled = true;
            livesCounter.enabled = true;
            inRange.enabled = true;
        }
        if (gameManager.currentGameState == GameManager.State.GameOver)
        {
            gameStateText.enabled = false;
            gameOver.enabled = true;
            inRange.enabled = false;
            chargeBar.enabled = false;
        }

        //update lives counter
        livesCounter.text = "" + gameManager.lives;

        if (frog.canJump)
        {
            inRange.text = "In range of frog";
            chargeBar.enabled = true;
            //charge.fillAmount = (float)distanceToTarget/15;
        } 
        else
        {
            inRange.text = "Not in range of frog. Input ignored.";
            chargeBar.enabled = false;
        }
    }
}
