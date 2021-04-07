using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;



public class fractalFlicking : MonoBehaviour
{

    public GameObject plane;
    public GameObject pipe;
    public GameObject curtain;

    int mode = 0;
    MeshRenderer pipeMesh;
    MeshRenderer planeMesh;
    MeshRenderer curtainMesh;

    private float nextActionTime = 0.0f;

    public Material solidMat;
    public Material curtainMat;




    // Start is called before the first frame update
    void Start()
    {
        pipeMesh = pipe.GetComponentInChildren<MeshRenderer>();
        planeMesh = plane.GetComponentInChildren<MeshRenderer>();
        curtainMesh = curtain.GetComponentInChildren<MeshRenderer>();


   }

    // Update is called once per frame
    void Update()
    {

 
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + Weistrasse.get_dt;


            float coef = (float)Weistrasse.NextValue();
            solidMat.SetColor("_EmissionColor", new Color(coef*Weistrasse.Color.r, coef * Weistrasse.Color.g , coef * Weistrasse.Color.b , 1));
            int mode = Weistrasse.mode;
            switch (mode)
            {
                case 0:
                    pipeMesh.enabled = true;
                    planeMesh.enabled = false;
                    curtainMesh.enabled = false;
                    break;
                case 1:
                    pipeMesh.enabled = false;
                    planeMesh.enabled = true;
                    curtainMesh.enabled = false;
                    break;
                case 2:
                    pipeMesh.enabled = false;
                    planeMesh.enabled = false;
                    curtainMesh.enabled = true;
                    break;
                case 3:
                    pipeMesh.enabled = false;
                    planeMesh.enabled = false;
                    curtainMesh.enabled = false;
                    break;
            }
            

            var pos = pipe.GetComponent<Transform>().localPosition;

            if(pipe.GetComponent<Transform>().localPosition.z!= (float)Weistrasse.DiscDist)
                pipe.GetComponent<Transform>().localPosition = new Vector3(pos.x,pos.y,(float)Weistrasse.DiscDist);

            if (curtainMat.color.a != Weistrasse.Alpha)
            {
                curtainMat.color(new Color(0,0,0, Weistrasse.Alpha);
            }
        }
    }
}
