using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;

    public XRNode inputSource;

    private float previousTime;
    private float previousActionTime;

    public static float fallTime = 0.8f;
    public static int height =20;
    public static int width = 10;

    public GameObject gameOverText;
    public static Transform[,] grid = new Transform[width, height];

    private Vector2 inputAxis;

    private float timeDelay = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        previousActionTime = Time.time;
    }




    // Update is called once per frame
    void Update()
    {

        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.secondary2DAxis, out inputAxis);

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        if (((Time.time - previousActionTime) > timeDelay) && (inputAxis.x< 0.5f) && (inputAxis.x < -1.0f * Mathf.Abs(inputAxis.y)) )
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!isValidMove())
                transform.position += new Vector3(1, 0, 0);

            previousActionTime = Time.time;

        }
       // else if (Input.GetKeyDown(KeyCode.RightArrow))
        else if (((Time.time - previousActionTime) > timeDelay) && (inputAxis.x > 0.5f) && (inputAxis.x > Mathf.Abs(inputAxis.y)))    
        {
            transform.position += new Vector3(1, 0, 0);
            if (!isValidMove())
                transform.position += new Vector3(-1, 0, 0);

            previousActionTime = Time.time;

        }
        //else if (Input.GetKeyDown(KeyCode.UpArrow))
        else if (((Time.time - previousActionTime) > timeDelay) && (inputAxis.y > 0.5f) && (inputAxis.y > Mathf.Abs(inputAxis.x)))
        {

            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90); 
            if (!isValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90); ;

            previousActionTime = Time.time;

        }


        //if (Time.time- previousTime > (Input.GetKey(KeyCode.DownArrow)? fallTime/10 : fallTime ))
        if (Time.time - previousTime > (((inputAxis.y < 0.5f) && (inputAxis.y < -1.0f * Mathf.Abs(inputAxis.x))) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!isValidMove())
            {
                transform.position += new Vector3(0, 1, 0);

                AddToGrid();
                CheckLines();
                this.enabled = false;
                if (!isGameOver())
                    FindObjectOfType<SpawnBlock>().NewTetraminoes();
                else
                {
                    //FindObjectOfType<UnityEngine.UI.Text>().text = "Game Over" ;


                }
            }
            previousTime = Time.time;
        }

        bool isGameOver()
        {
            foreach (Transform child in transform)
            {
                int roundedX = Mathf.RoundToInt(child.transform.position.x);
                int roundedY = Mathf.RoundToInt(child.transform.position.y);

                if (roundedY > 17)
                    return true;
            }
            return false;
        }
        void AddToGrid()
        {
            foreach (Transform child in transform)
            {
                int roundedX = Mathf.RoundToInt(child.transform.position.x);
                int roundedY = Mathf.RoundToInt(child.transform.position.y);

                grid[roundedX, roundedY] = child;
            }
        }

        bool isValidMove()
        {
            foreach(Transform child in transform)
            {
                int roundedX = Mathf.RoundToInt(child.transform.position.x);
                int roundedY = Mathf.RoundToInt(child.transform.position.y);
                if (roundedX < 0 || roundedX >= width || roundedY <0 || roundedY >= height)
                    return false;

                if (grid[roundedX, roundedY] != null)
                    return false;
            }
            return true;
        }
    }

    private void CheckLines()
    {
        for (int i = height-1; i >=0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    private void RowDown(int i)
    {
        for (int k = i; k < height; k++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j,k] != null)
                {
                    grid[j, k - 1] = grid[j, k];
                    grid[j, k] = null;
                    grid[j, k - 1].transform.position += new Vector3(0, -1, 0);
                }

            }
        }

    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[ j,i].gameObject);
            grid[j,i] = null;
                
        }
    }

    private bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[ j,i] == null)
                return false;
        }
        return true;
    }
}
