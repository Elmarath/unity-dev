using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Declearing variables
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform centerOfMass;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;


    [SerializeField] private ParticleSystem frontLeftWheelPS;
    [SerializeField] private ParticleSystem frontRightWheelPS;
    [SerializeField] private ParticleSystem rearLeftWheelPS;
    [SerializeField] private ParticleSystem rearRightWheelPS;

    [SerializeField] private ParticleSystem frontLeftWheelTrailsPS;
    [SerializeField] private ParticleSystem frontRightWheelTrailsPS;
    [SerializeField] private ParticleSystem rearLeftWheelTrailsPS;
    [SerializeField] private ParticleSystem rearRightWheelTrailsPS;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }


    // called every fixed frame (100 frames per sec)
    private void FixedUpdate()
    {
        // gets the keyboard Inputs for character
        GetInput();
        // handle motor power by Input
        HandleMotor();
        // handle the front rotation of wheels
        HandleSteering();
        // update the wheels in game
        UpdateWheels();
        // create the necessary particle affects 
        UpdateParticleEffects();
    }


    private void GetInput()
    {
        verticalInput = 0f;
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        //Restart
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Restart");
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(0, 60f, transform.position.z);
        }
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }


    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateParticleEffects()
    {
        // stop the all particle effects 
        frontLeftWheelPS.Stop();
        frontRightWheelPS.Stop();
        rearLeftWheelPS.Stop();
        rearRightWheelPS.Stop();
        frontLeftWheelTrailsPS.Stop();
        frontRightWheelTrailsPS.Stop();
        rearLeftWheelTrailsPS.Stop();
        rearRightWheelTrailsPS.Stop();

        // decide which particle effects to run
        if (rb.velocity.magnitude < 7 && verticalInput > 0.1f)
        {
            if (frontLeftWheelCollider.isGrounded)
            {
                frontLeftWheelPS.Play();
                frontLeftWheelTrailsPS.Play();
            }
            if (frontRightWheelCollider.isGrounded)
            {
                frontRightWheelPS.Play();
                frontRightWheelTrailsPS.Play();
            }
            if (rearLeftWheelCollider.isGrounded)
            {
                rearLeftWheelPS.Play();
                rearLeftWheelTrailsPS.Play();
            }
            if (rearRightWheelCollider.isGrounded)
            {
                rearRightWheelPS.Play();
                rearRightWheelTrailsPS.Play();
            }
        }


    }

    // this is not in use
    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    // set the wheels rotation and position accourding to wheels collider position
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
; wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
