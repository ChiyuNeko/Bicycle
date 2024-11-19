using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
//using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(InputData))]
public class HandsParameters : MonoBehaviour
{
    InputDevice LeftControllerDevice;
    InputDevice RightControllerDevice;
    public Vector3 LeftControllerVelocity;
    public Vector3 RightControllerVelocity;
    InputData inputData;

    void Start()
    {
        LeftControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        inputData = GetComponent<InputData>();
    }

    void Update()
    {
        inputData._leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity);
        inputData._rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);
    }


}
