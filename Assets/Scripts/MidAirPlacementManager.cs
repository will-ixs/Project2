using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

public class MidAirPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject myPrefab;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform lastJump;

    private int nameCount = 0;
    private float scaleFactor = 0.005f;
    private GameObject currentPlatform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch currentTouch = Input.GetTouch(0);

            switch (currentTouch.phase)
            {
                case (TouchPhase.Began):
                    scaleFactor = 0.005f; 
                    break;

                case (TouchPhase.Moved):
                    scaleFactor *= 1.001f;
                    currentPlatform.transform.localScale += new Vector3(scaleFactor, 0f, scaleFactor);
                    break;

                case (TouchPhase.Stationary):
                    scaleFactor *= 1.001f;
                    currentPlatform.transform.localScale += new Vector3(scaleFactor, 0f, scaleFactor);
                    break;

                case (TouchPhase.Ended):
                    break;
            }       
        }
    }

    public void AnchorCreator(Transform worldPositioning)
    {
        GameObject ObjectToAnchor = Instantiate(myPrefab, worldPositioning.position, Quaternion.Euler(0,0,0));
        AnchorBehaviour myAnchor = ObjectToAnchor.AddComponent<AnchorBehaviour>();
        myAnchor.ConfigureAnchor("Anchor" + nameCount.ToString(), worldPositioning.position, Quaternion.Euler(0, 0, 0));
        nameCount += 1;

        currentPlatform = ObjectToAnchor;
        currentPlatform.GetComponent<PlatformCollision>().lastJump = lastJump;
        gameManager.platforms.Add(currentPlatform);
        gameManager.platformCount++;
    }
}
