using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlaneFinderBehaviour planeFinder;
    [SerializeField] private ContentPositioningBehaviour pfContent;
    [SerializeField] private AnchorBehaviour startAnchor;
    [SerializeField] private AnchorBehaviour endAnchor;

    [SerializeField] private MidAirPositionerBehaviour midAirFinder;
    [SerializeField] private ContentPositioningBehaviour maContent;
    [SerializeField] private AnchorBehaviour[] platforms;

    [SerializeField] private GameObject frog;

    [SerializeField] private Button startButton;

    public enum State
    {
        StartScreen,
        PlacingStart,
        PlacingFinish,
        PlacingPlatforms,
        Playing,
        GameOver
    }
    public State currentGameState;

    public int platformCount;
    void Start()
    {
        currentGameState = State.StartScreen;
        platformCount = 0;
    }
    void Update()
    {
        switch (currentGameState)
        {
            case State.StartScreen:
                //start button now does this:
                //currentGameState = State.PlacingStart;
                //StartGame(); 
                startButton.enabled = true;
                break;
            case State.PlacingStart:
                //wait for start to be placed
                frog.transform.position = startAnchor.gameObject.transform.position;
                break;
            case State.PlacingFinish:
                //wait for finish to be placed
                break;
            case State.PlacingPlatforms:
                //get touch, increase size like tutorial
                //in touch end statement check platform count, if == 4, currentgamestate = playing
                break;
            case State.Playing:
                //Frog.cs checks if state is playing and controls game and lives
                break;
            case State.GameOver:
                break;
        }
    }

    public void CurrentContentPlaced() //called from content positioning scripts, midair and plane
    {
        switch (currentGameState)
        {
            case State.PlacingStart:
                StartPlaced();
                break;
            case State.PlacingFinish:
                FinishPlaced();
                break;
            case State.PlacingPlatforms:
                PlatformPlaced();
                break;
        }
    }

    //planned to enable and disable whichever  game objects currently needed
    public void StartGame()
    {
        startButton.enabled = false;
        currentGameState = State.PlacingStart;
        startAnchor.gameObject.SetActive(true);
        planeFinder.gameObject.SetActive(true);
        pfContent.AnchorStage = startAnchor;
    }
    public void StartPlaced()
    {
        currentGameState = State.PlacingFinish;
        endAnchor.gameObject.SetActive(true);
    }
    public void FinishPlaced()
    {
        currentGameState = State.PlacingPlatforms;
        planeFinder.gameObject.SetActive(false);
    }
    public void PlatformPlaced()
    {
        platformCount++;
    }
}



