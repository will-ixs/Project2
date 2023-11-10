using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] Transform lastjump;
    public bool win;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Frog")
        {
            win = true;
            foreach(GameObject go in gm.platforms)
            {
                if(go.GetComponent<PlatformCollision>().done == false)
                {
                    other.transform.position = lastjump.position;
                    other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    win = false;
                    break;
                }
            }
            if (win) { 
                gm.currentGameState = GameManager.State.GameOver;
            }
        }
    }
}
