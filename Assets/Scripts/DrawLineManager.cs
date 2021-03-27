using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DrawLineManager : MonoBehaviour
{

    public Material lineMat;
    public XRNode LeftInputSource;
    public XRNode RightInputSource;
    public GameObject leftHand;
    public GameObject rightHand;

    public float activateThreshold = 0.1f;


    MeshLineRenderer leftCurrLine, rightCurrLine;

    bool leftState, rightState;

    int leftClicks=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice left = InputDevices.GetDeviceAtXRNode(LeftInputSource);
        InputDevice right = InputDevices.GetDeviceAtXRNode(RightInputSource);
        float leftValue, rightValue;

        left.TryGetFeatureValue(CommonUsages.trigger, out leftValue);
        right.TryGetFeatureValue(CommonUsages.trigger, out rightValue);

        if (leftValue > activateThreshold)
        {
            if (!leftState)
            {
                GameObject go = new GameObject();
                go.AddComponent<MeshFilter>();
                go.AddComponent<MeshRenderer>();
                leftCurrLine = go.AddComponent<MeshLineRenderer>();
                leftCurrLine.setWidth(.1f);
                leftCurrLine.lmat = lineMat;

                 
                leftClicks = 0;
            }

            leftState = true;
            Vector3 pos=leftHand.transform.position;
            //left.TryGetFeatureValue(CommonUsages.devicePosition, out pos);


            //leftCurrLine.positionCount = leftClicks + 1;
            //leftCurrLine.SetPosition(leftClicks, pos);
            leftCurrLine.AddPoint(pos);
            leftClicks++;




        }
        else
        {
            leftState = false;
        }

       
    }
}
