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
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform jumpTarget;
    [SerializeField] private Transform lastJump;

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
        if(lives == 0)
        {
            GameOver();
        }
        switch (currentGameState)
        {
            case State.StartScreen:
                startButton.enabled = true;
                break;
            case State.PlacingStart:
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
        startButton.gameObject.SetActive(false);
    }
    public void StartPlaced()
    {
        jumpTarget.gameObject.SetActive(true);
        projectile.gameObject.SetActive(true);
        lastJump.gameObject.SetActive(true);
        frog.gameObject.SetActive(true);
        frog.transform.position = startAnchor.gameObject.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
        planeFinder.gameObject.SetActive(false);
        currentGameState = State.PlacingPlatforms;
        midAirFinder.gameObject.SetActive(true);
        midAirPlacementManager.gameObject.SetActive(true);
    }
    public void FinishPlaced()
    {

        frog.GetComponent<Frog>().lowPoint = startAnchor.gameObject.transform.position.y - 10.0f;
        jumpTarget.transform.position = frog.transform.position;
        lastJump.transform.position = frog.transform.position;
        planeFinder.gameObject.SetActive(false);
        currentGameState = State.Playing;
    }
    public void GameOver()
    {
        currentGameState = State.GameOver;
    }
}



