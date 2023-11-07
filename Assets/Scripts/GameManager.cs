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
    [SerializeField] private MidAirPlacementManager midAirPlacementManager;
    public List<GameObject> platforms;

    [SerializeField] private GameObject frog;

    [SerializeField] private Button startButton;

    public int lives;
    public int platformCount;
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

    void Start()
    {
        lives = 7;
        currentGameState = State.StartScreen;
        platformCount = 0;
    }
    void Update()
    {
        switch (currentGameState)
        {
            case State.StartScreen:
                startButton.enabled = true;
                break;
            case State.PlacingStart:
                frog.transform.position = startAnchor.gameObject.transform.position;
                break;
            case State.PlacingPlatforms:
                if(platformCount > 4)
                {
                    currentGameState = State.PlacingFinish;
                    midAirFinder.gameObject.SetActive(false);
                    endAnchor.gameObject.SetActive(true);
                    planeFinder.gameObject.SetActive(true);
                    midAirPlacementManager.gameObject.SetActive(false);
                    pfContent.AnchorStage = endAnchor;
                }
                break;
            case State.PlacingFinish:
                //
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
        frog.gameObject.SetActive(true);
        planeFinder.gameObject.SetActive(false);
        currentGameState = State.PlacingPlatforms;
        midAirFinder.gameObject.SetActive(true);
        midAirPlacementManager.gameObject.SetActive(true);
    }
    public void FinishPlaced()
    {
        currentGameState = State.Playing;
        planeFinder.gameObject.SetActive(false);
    }
}



