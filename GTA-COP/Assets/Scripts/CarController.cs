using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public Rigidbody sphereRB;

    public float forwardAcc = 8f;
    public float reverseAcc = 4f; 
    public float maxSpeed = 50f;
    public float turnStrength = 180f;
    public float gravityForce = 10f;
    public float dragOnGround;
    public float speedCanTurn;
    

    private float speedInput;
    private float turnInput;

    private bool isGrounded;

    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn;

    // Start is called before the first frame update
    void Start()
    {
        sphereRB.transform.parent = null;

    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;

        // Get Input
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAcc * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAcc * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");

        // set cars position and rotation to that of the sphere.
        transform.position = sphereRB.transform.position;

        if (isGrounded)
        {
            Debug.Log(sphereRB.velocity.magnitude);
            if (sphereRB.velocity.magnitude > 2)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));
            }
            //NB - Multiply by RB speed to make it proportional to speed.
        }

        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z );
        rightFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z );
    }

    private void FixedUpdate()
    {
        isGrounded = false;

        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            isGrounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (isGrounded == true)
        {
            sphereRB.drag = dragOnGround;

            //Accelerate
            if (Mathf.Abs(speedInput) > 0)  //Mathf.Abs diregards pos or neg values
            {
                sphereRB.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            sphereRB.AddForce(Vector3.up * -gravityForce * 100f);
            sphereRB.drag = 0.1f;
        }
       
    }
}
