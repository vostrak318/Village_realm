using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float rideSpeed = 6f;
    private float currentSpeed;

    [SerializeField]
    float toggleRideCooldown = 1f;
    float currentToggleRideCooldown = 0;

    private Vector2 axisMovement;
    public Animator animator;

    private bool riding = false;
    [SerializeField]
    private GameObject rideObject;

    private void Start()
    {
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        axisMovement.x = Input.GetAxisRaw("Horizontal");
        axisMovement.y = Input.GetAxisRaw("Vertical");
        LowerCooldown();
        if (riding && Input.GetKeyDown(KeyCode.C))
        {
            ToggleRide();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + axisMovement, currentSpeed * Time.deltaTime);

        animator.SetBool("Walk", axisMovement.magnitude > 0);
        animator.SetBool("Ride", riding);

        CheckForFlipping();
    }

    private void CheckForFlipping()
    {
        if (axisMovement.x < 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else if (axisMovement.x > 0)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
    }

    public void ToggleRide()
    {
        if (currentToggleRideCooldown <= 0)
        {
            if (!riding)
            {
                riding = true;
                currentSpeed = rideSpeed;
            }
            else
            {
                riding = false;
                currentSpeed = walkSpeed;
                animator.SetBool("Ride", riding);
                Instantiate(rideObject, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            }
            currentToggleRideCooldown = toggleRideCooldown;
        }
        Debug.Log(riding);
    }

    void LowerCooldown()
    {
        if (currentToggleRideCooldown > 0)
        {
            currentToggleRideCooldown -= Time.deltaTime;
        }
    }
}
