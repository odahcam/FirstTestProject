using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCarController : MonoBehaviour
{
    private float HorizontalInput { get; set; }
    private float VerticalInput { get; set; }
    private float SteeringAngle { get; set; }

    public WheelCollider wheelFrontDriver;
    public WheelCollider wheelFrontPassenger;
    public WheelCollider wheelRearDriver;
    public WheelCollider wheelRearPassenger;

    public Transform transformFrontDriver;
    public Transform transformFrontPassenger;
    public Transform transformRearDriver;
    public Transform transformRearPassenger;

    public float maxSteeringAngle = 30;
    public float motorForce = 50; // torque


    public void GetInput() 
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
    }

    private void Steer() 
    {
        SteeringAngle = maxSteeringAngle * HorizontalInput;
        wheelFrontDriver.steerAngle = SteeringAngle;
        wheelFrontPassenger.steerAngle = SteeringAngle;
    }

    private void Accelarate()
    {
        wheelFrontDriver.motorTorque = VerticalInput * motorForce;
        wheelFrontPassenger.motorTorque = VerticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(wheelFrontDriver, transformFrontDriver);
        UpdateWheelPose(wheelFrontPassenger, transformFrontPassenger);
        UpdateWheelPose(wheelRearDriver, transformRearDriver);
        UpdateWheelPose(wheelRearPassenger, transformRearPassenger);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        var pos = transform.position;
        var quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelarate();
        UpdateWheelPoses();
    }
}
