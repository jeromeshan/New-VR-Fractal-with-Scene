using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ConeGlasses : MonoBehaviour
{
    List<XRNodeState> nodes;
    XRNodeState state;
    public GameObject pipePrefab;
    // Start is called before the first frame update
    void Start()
    {
        nodes = new List<XRNodeState>();
        XRNode eye = XRNode.LeftEye;
        float sep = Camera.main.stereoSeparation;
        state.nodeType = XRNode.LeftEye;
        nodes.Add(state);
        state.nodeType = XRNode.RightEye;
        nodes.Add(state);
        Vector3 leftPos, rightPos;
        Quaternion leftRot, rightRot;
        nodes[0].TryGetPosition(out leftPos);
        nodes[1].TryGetPosition(out rightPos);
        nodes[0].TryGetRotation(out leftRot);
        nodes[1].TryGetRotation(out rightRot);

        Debug.Log(leftPos);
        Instantiate(pipePrefab, leftPos, leftRot);
        Instantiate(pipePrefab, rightPos, rightRot);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
