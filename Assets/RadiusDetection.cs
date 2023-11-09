using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusDetection : MonoBehaviour
{

    [SerializeField] private GameManager gm;
    [SerializeField] private Frog frog;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") && gm.currentGameState == GameManager.State.Playing)
        {
            frog.canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera") && gm.currentGameState == GameManager.State.Playing)
        {
            frog.canJump = false;
        }
    }
}
