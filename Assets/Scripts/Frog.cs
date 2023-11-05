using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Projectile jumpArc;
    [SerializeField] float jumpAngle = 45;

    private float jumpSpeed;


    private void Update()
    {
        //Check
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
}
