using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject direction;
    [SerializeField] private Projectile jumpArc;
    [SerializeField] private Transform lastJumpPosition;
    [SerializeField] private float jumpAngle = 60;
    [SerializeField] private GameManager gm;
    
    private Rigidbody rb;
    private float jumpSpeed;
    public bool canJump;
    public float distanceToTarget;
    private float distanceChange;
    public float lowPoint;

    private void Start()
    {
        lowPoint = float.MaxValue;
        distanceChange = 0.125f;
        canJump = false;   
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (gm.currentGameState == GameManager.State.Playing)
        {
            SetTargetWithAngle(target.position);
            distanceToTarget = (target.position - transform.position).magnitude;
            if (distanceToTarget > 20.0f || distanceToTarget <= 0.0f)
            {
                distanceChange *= -1;
            }

            if (canJump && lowVelocity()) //camera in range
            {
                transform.rotation = Quaternion.Euler(0.0f, cameraTransform.rotation.y, 0.0f);
                Jump();
            }

            if(transform.position.y < lowPoint)
            {
                transform.position = lastJumpPosition.position;
                gm.lives--;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        
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

    private void Jump()
    {
        if (Input.touchCount > 0)
        {
            Touch currentTouch = Input.GetTouch(0);

            switch (currentTouch.phase)
            {
                case (TouchPhase.Began):
                    target.position = transform.position;
                    break;

                case (TouchPhase.Moved): //move jump target forward up to a maximum distance
                    target.position += (transform.forward * distanceChange);
                    break;

                case (TouchPhase.Stationary):
                    target.position += (transform.forward * distanceChange);
                    break;

                case (TouchPhase.Ended): //construct vector at 60 degrees up facing forward, set vel to Math.LaunchSpeed along that vector
                    rb.velocity = direction.transform.forward * jumpSpeed;
                    break;
            }
        }
    }
    public bool lowVelocity()
    {
        if (rb.velocity.magnitude <= 0.5f)
        {
            return true;
        }
        return false;
    }
}
