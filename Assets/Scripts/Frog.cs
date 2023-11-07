using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Projectile jumpArc;
    [SerializeField] float jumpAngle = 60;

    private float jumpSpeed;

    public bool canJump;

    private void Start()
    {
        canJump = false;        
    }
    private void Update()
    {
        //Check
        if (canJump)
        {
            //set angle to camera angle
        }
    }

    public void SetTargetWithAngle(Vector3 point)
    { 
        Vector3 direction = point - transform.position;
        float yOffset = -direction.y;
        direction = Vector3.ProjectOnPlane(direction, Vector3.up);
        float distance = direction.magnitude;

        jumpSpeed = Math.LaunchSpeed(distance, yOffset, Physics.gravity.magnitude, jumpAngle * Mathf.Deg2Rad);

        jumpArc.UpdateArc(jumpSpeed, distance, Physics.gravity.magnitude, jumpAngle * Mathf.Deg2Rad, direction, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            canJump = false;
        }
    }
}
