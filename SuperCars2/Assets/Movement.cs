using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float ballSpeed = 10.0f;
    private Vector3 moveInput;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
      //  rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        rb.velocity = moveInput * ballSpeed;
    }
}
