using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] private Material green;
    public Transform lastJump;
    public bool done;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        GetComponent<MeshRenderer>().material = green;
        done = true;
        lastJump.position = transform.position + new Vector3(0.0f, 0.15f, 0.0f);
    }
}
