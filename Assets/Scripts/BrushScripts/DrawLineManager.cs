using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DrawLineManager : MonoBehaviour
{

    public Material lineMat;
    public XRNode LeftInputSource;
    //public XRNode RightInputSource;
    public GameObject leftHand;
    public GameObject Sphere;
    public GameObject Capsule;

    Vector3 previousPos = Vector3.zero;
    //public GameObject rightHand;

    public float activateThreshold = 0.1f;

    MeshLineRenderer leftCurrLine;//, rightCurrLine;

    bool leftState;//, rightState;

    int leftClicks=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputDevice left = InputDevices.GetDeviceAtXRNode(LeftInputSource);
        //InputDevice right = InputDevices.GetDeviceAtXRNode(RightInputSource);
        float leftValue;//, rightValue;

        left.TryGetFeatureValue(CommonUsages.trigger, out leftValue);
        //right.TryGetFeatureValue(CommonUsages.trigger, out rightValue);

        if (leftValue > activateThreshold)
        {
            //if (!leftState)
            //{
            //    GameObject go = new GameObject();
            //    go.AddComponent<MeshFilter>();
            //    go.AddComponent<MeshRenderer>();
            //    go.AddComponent<MeshCollider>();
            //    go.GetComponent<MeshCollider>().isTrigger = true;
            //    go.tag = "DrawedLine";
            //    leftCurrLine = go.AddComponent<MeshLineRenderer>();

            //    leftCurrLine.setWidth(.1f);
            //    leftCurrLine.lmat = lineMat;


            //    leftClicks = 0;
            //}

            //leftState = true;
            //Vector3 pos=leftHand.transform.position;
            ////left.TryGetFeatureValue(CommonUsages.devicePosition, out pos);


            ////leftCurrLine.positionCount = leftClicks + 1;
            ////leftCurrLine.SetPosition(leftClicks, pos);
            //leftCurrLine.AddPoint(pos);
            //leftClicks++;

           if (previousPos == Vector3.zero)
           {
               previousPos = leftHand.transform.position;
           }
           // leftState = true;
           Vector3 pos = leftHand.transform.position;
           GameObject go = GameObject.Instantiate(Capsule);
           go.GetComponent<MeshRenderer>().material = Sphere.GetComponent<MeshRenderer>().material;
           go.tag = "DrawedLine";
           go.transform.position = (pos + previousPos) / 2;
           go.transform.localScale = new Vector3(0.03f,  Vector3.Distance(previousPos, pos)*0.8f,0.03f);
           go.transform.LookAt(pos);
           go.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

           previousPos = pos;
            // GameObject go = new GameObject();
            // go.AddComponent<MeshFilter>();
            // go.AddComponent<MeshRenderer>();
            // go.tag = "DrawedLine";
            // MeshLineRenderer Line = go.AddComponent<MeshLineRenderer>();

            // Line.setWidth(.5f);
            // Line.lmat = Sphere.GetComponent<MeshRenderer>().material;
            // Line.AddPoint(previousPos);
            // Line.AddPoint(pos);
            // previousPos = pos;
        }
        else
        {
            leftState = false;
            previousPos = Vector3.zero;
        }




    }
}
