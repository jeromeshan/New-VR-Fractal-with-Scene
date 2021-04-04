using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Blade : MonoBehaviour
{

    public XRNode LeftInputSource;
    public float activateThreshold = 0.1f;

    bool isCutting = false;
    // Update is called once per frame
    void Update()
    {
        InputDevice left = InputDevices.GetDeviceAtXRNode(LeftInputSource);
        //InputDevice right = InputDevices.GetDeviceAtXRNode(RightInputSource);
        float leftValue;//, rightValue;

        left.TryGetFeatureValue(CommonUsages.trigger, out leftValue);
        //right.TryGetFeatureValue(CommonUsages.trigger, out rightValue);

        if (leftValue > activateThreshold)
        {

            StartCutting();
        }
        else
        {
            StopCutting();
        }
    }

     void StopCutting()
    {
        isCutting = false;
    }

    void StartCutting()
    {
        isCutting = false;
    }
}
