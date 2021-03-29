﻿using Assets;
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
    int mode = 0;
    MeshRenderer pipeMesh;
    MeshRenderer planeMesh;
    private float nextActionTime = 0.0f;

    public Material coneMat;


    public void ReceiveCallback(IAsyncResult AsyncCall)
    {


        Socket listener = (Socket)AsyncCall.AsyncState;
        Socket client = listener.EndAccept(AsyncCall);
        using (NetworkStream s = new NetworkStream(client))
        {
            BinaryReader reader = new BinaryReader(s);

            FunctionParams funcParams = new FunctionParams(new Color(reader.ReadInt32()/255f, reader.ReadInt32()/255f, reader.ReadInt32()/255f), reader.ReadInt32(),
                reader.ReadDouble(), reader.ReadDouble(), reader.ReadInt32(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());

            mode = reader.ReadInt32();

            Weistrasse.ParamsUpdate(funcParams);

            BinaryWriter writer = new BinaryWriter(s);
            writer.Write("Message accepted");
            writer.Flush();
            s.Close();
            client.Close();
            reader.Close();
            writer.Close();
            //if (mode == 0)
            //{
            //    pipeMesh.enabled = true;
            //    planeMesh.enabled = false;
            //}
            //if (mode == 1)
            //{
            //    pipeMesh.enabled = false;
            //    planeMesh.enabled = true;
            //}
            //if (mode == 3)
            //{
            //    pipeMesh.enabled = false;
            //    planeMesh.enabled = false;
            //}

            listener.BeginAccept(new AsyncCallback(ReceiveCallback), listener);
        }



        // После того как завершили соединение, говорим ОС что мы готовы принять новое

    }

    // Start is called before the first frame update
    void Start()
    {
        pipeMesh = pipe.GetComponentInChildren<MeshRenderer>();
        planeMesh = plane.GetComponentInChildren<MeshRenderer>();


        IPAddress localAddress = IPAddress.Parse("127.0.0.1");

        Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint ipEndpoint = new IPEndPoint(localAddress, 2200);

        listenSocket.Bind(ipEndpoint);

        listenSocket.Listen(1);
        listenSocket.BeginAccept(new AsyncCallback(ReceiveCallback), listenSocket);
   }

    // Update is called once per frame
    void Update()
    {

 
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + Weistrasse.get_dt;


            float coef = (float)Weistrasse.NextValue();
            coneMat.SetColor("_EmissionColor", new Color(coef*Weistrasse.Color.r, coef * Weistrasse.Color.g , coef * Weistrasse.Color.b , 1));

            if (mode == 0)
            {
                pipeMesh.enabled = true;
                planeMesh.enabled = false;
            }
            if (mode == 1)
            {
                pipeMesh.enabled = false;
                planeMesh.enabled = true;
            }
            if (mode == 2)
            {
                pipeMesh.enabled = false;
                planeMesh.enabled = false;
            }


        }
    }
}