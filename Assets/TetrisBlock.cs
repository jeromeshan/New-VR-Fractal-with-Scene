﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public static float fallTime = 4.8f;
    public static int height =20;
    public static int width = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!isValidMove())
                transform.position += new Vector3(1, 0, 0);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!isValidMove())
                transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90); ;
            if (!isValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90); ;
        }


        if (Time.time- previousTime > (Input.GetKey(KeyCode.DownArrow)? fallTime/10 : fallTime ))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!isValidMove())
                transform.position += new Vector3(0, 1, 0);
            previousTime = Time.time;
        }

        bool isValidMove()
        {
            foreach(Transform child in transform)
            {
                int roundedX = Mathf.RoundToInt(child.transform.position.x);
                int roundedY = Mathf.RoundToInt(child.transform.position.y);

                if (roundedX <= 0 || roundedX > width || roundedY <=0 || roundedY > height)
                    return false;
            }
            return true;
        }
    }
}
