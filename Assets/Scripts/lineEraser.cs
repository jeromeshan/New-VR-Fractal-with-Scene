using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class lineEraser : MonoBehaviour
{

    public XRNode RightInputSource;
    public float activateThreshold = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        InputDevice right = InputDevices.GetDeviceAtXRNode(RightInputSource);
        float rightValue;

        right.TryGetFeatureValue(CommonUsages.trigger, out rightValue);

        
        if ((collision.collider.tag== "DrawedLine")&& (rightValue > activateThreshold))
        {
            Destroy(collision.collider);
        }
    }
}
