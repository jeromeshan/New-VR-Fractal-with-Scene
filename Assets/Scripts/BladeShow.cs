using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BladeShow : MonoBehaviour
{
    
    public XRNode LeftInputSource;
    public float activateThreshold = 0.1f;
    public GameObject leftHand;
    public GameObject blade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

            blade.SetActive(true);
            leftHand.SetActive(false);
        }
        else
        {

            blade.SetActive(false);
            leftHand.SetActive(true);
        }
    }
}
