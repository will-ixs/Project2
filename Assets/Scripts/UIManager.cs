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
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private EndGame endScript;


    // Start is called before the first frame update
    void Start()
    {
        title.enabled = true;
        gameOver.enabled = false;
        winText.enabled = false;
        charge.fillAmount = 0;
        chargeBar.enabled = false;
        charge.enabled = false;
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
            charge.enabled = false;
        }
        if (gameManager.currentGameState == GameManager.State.PlacingStart)
        {
            gameStateText.enabled = true;
            title.enabled = false;
            gameStateText.text = "Place Start Line";
        }
        if (gameManager.currentGameState == GameManager.State.PlacingFinish)
        {
            gameStateText.text = "Place Finish Line";
        }
        if (gameManager.currentGameState == GameManager.State.PlacingPlatforms)
        {
            gameStateText.text = "Place " + (5 - (gameManager.platformCount)) + " Platforms";
        }
        if (gameManager.currentGameState == GameManager.State.Playing)
        {
            gameStateText.text = "Jump on platforms!";
            heart.enabled = true;
            livesCounter.enabled = true;
            inRange.enabled = true;
            chargeBar.enabled = true;
            charge.enabled = true;
        }
        if (gameManager.currentGameState == GameManager.State.GameOver)
        {
            gameStateText.enabled = false;
            
            if (endScript.win == true)
            {
                winText.enabled = true;
            }
            else if (endScript.win == false && (gameManager.lives == 0))
            {
                gameOver.enabled = true;
            }
            
            inRange.enabled = false;
            chargeBar.enabled = false;
            charge.enabled = false;
        }

        //update lives counter
        livesCounter.text = "" + gameManager.lives;

        if (frog.canJump)
        {
            inRange.text = "In range of frog";
            chargeBar.enabled = true;
            charge.fillAmount = frog.distanceToTarget/15.0f;
        } 
        else
        {
            inRange.text = "Not in range of frog. Input ignored.";
            chargeBar.enabled = false;
        }
    }
}
