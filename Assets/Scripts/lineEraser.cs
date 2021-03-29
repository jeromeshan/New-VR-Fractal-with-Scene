using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class lineEraser : MonoBehaviour
{

    public XRNode RightInputSource;
    public float activateThreshold = 0.1f;
    public GameObject rightHand;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice right = InputDevices.GetDeviceAtXRNode(RightInputSource);
        float rightValue;

        right.TryGetFeatureValue(CommonUsages.trigger, out rightValue);


        if ( (rightValue > activateThreshold))
        {
            DestroyObjectAtLocation(rightHand.transform.position, "DrawedLine");
        }
    }

    void DestroyObjectAtLocation(Vector3 pos,string tag)
    {

        Vector3 tmpLocation = pos;
        Transform[] tiles = GameObject.FindObjectsOfType<Transform>();
        for (int i = 0; i < tiles.Length; i++)
        {
            if ((Vector3.Distance(tiles[i].position, tmpLocation) <= 0.1f)&&(tiles[i].tag==tag))
            {
                Destroy(tiles[i].gameObject);
            }
        }
    }


 


}
