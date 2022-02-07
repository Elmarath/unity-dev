using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform centerOfMass;

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


    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        UpdateParticleEffects();
    }


    private void GetInput()
    {
        verticalInput = 0f;
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalInput = -1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }

        //Restart
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("Restart");
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0, 60f, transform.position.z);
        }
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
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
        frontLeftWheelPS.Stop();
        frontRightWheelPS.Stop();
        rearLeftWheelPS.Stop();
        rearRightWheelPS.Stop();
        frontLeftWheelTrailsPS.Stop();
        frontRightWheelTrailsPS.Stop();
        rearLeftWheelTrailsPS.Stop();
        rearRightWheelTrailsPS.Stop();

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

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
