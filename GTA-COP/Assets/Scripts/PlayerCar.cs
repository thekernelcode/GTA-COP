using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public Transform centreOfMass; 
    float motorTorque = 2500f;
    float maxSteer = 40f;

    public float Steer { get; set; }

    public float Throttle { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;
    
    // Start is called before the first frame update
    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centreOfMass.localPosition;
    }

    private void Update()
    {
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
