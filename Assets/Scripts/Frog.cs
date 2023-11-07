using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform target;
    [SerializeField] private Projectile jumpArc;
    [SerializeField] private Transform lastJumpPosition;
    [SerializeField] private float jumpAngle = 60;
    [SerializeField] private GameManager gm;
    
    private Rigidbody rb;
    private float jumpSpeed;
    public bool canJump;
    public float distanceToTarget;
    private float distanceChange;

    private void Start()
    {
        distanceChange = 0.125f;
        target = transform;
        canJump = false;   
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(gm.currentGameState == GameManager.State.Playing)
        { 
            distanceToTarget = (target.position - transform.position).magnitude;
            if (canJump) //camera in range
            {
                if (lowVelocity())//and frog not currently jumping
                {
                    transform.forward = cameraTransform.forward;
                    jumpArc.transform.forward = transform.forward;
                    target.position = transform.position;
                    Jump();
                    SetTargetWithAngle(target.position);
                }
            }
            if(transform.position.y < -10.0f)
            {
                transform.position = lastJumpPosition.position;
                gm.lives--;
            }
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

    private void Jump()
    {
        if (Input.touchCount > 0)
        {
            Touch currentTouch = Input.GetTouch(0);

            switch (currentTouch.phase)
            {

                case (TouchPhase.Moved): //move jump target forward up to a maximum distance
                    if(distanceToTarget > 15.0f)
                    {
                        distanceChange = -0.125f;
                    }else if(distanceToTarget < 1.0f)
                    {
                        distanceChange = 0.125f;
                    }
                    target.position = transform.position + (transform.forward * (distanceToTarget + distanceChange));
                    break;

                case (TouchPhase.Stationary):
                    if (distanceToTarget > 15.0f)
                    {
                        distanceChange = -0.125f;
                    }
                    else if (distanceToTarget < 1.0f)
                    {
                        distanceChange = 0.125f;
                    }
                    target.position = transform.position + (transform.forward * (distanceToTarget + distanceChange));
                    break;

                case (TouchPhase.Ended): //construct vector at 60 degrees up facing forward, set vel to Math.LaunchSpeed along that vector
                    Quaternion rot = new Quaternion();
                    rot.eulerAngles = new Vector3(0, 60, 0);
                    transform.rotation = rot;
                    rb.velocity = transform.forward * jumpSpeed;
                    lastJumpPosition = transform;
                    break;
            }
        }
    }

    private bool lowVelocity()
    {
        if (rb.velocity.magnitude <= 0.1f)
        {
            return true;
        }
        return false;
    }
}
