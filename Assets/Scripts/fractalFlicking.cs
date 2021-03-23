using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class FunctionParams
{
    public FunctionParams(double d, int n, double sigma, double b, double s)
    {
        D = d;
        N = n;
        this.sigma = sigma;
        this.b = b;
        this.s = s;
    }

    public double D;
    public int N;
    public double sigma;
    public double b;
    public double s;

}

public class fractalFlicking : MonoBehaviour
{
    public double[] y;
    int counter = 0, max_counter;

    public int T = 20;

    private float nextActionTime = 0.0f;
    public float period = 0.05f;

    public Material coneMat;




    // Start is called before the first frame update
    void Start()
    {
        max_counter = (int)(T / period);

        //FunctionParams func_params = new FunctionParams(1.4, 10, 3.3, 2.5, 0.005);

        // string json_params = JsonUtility.ToJson(func_params);
        //using (var tw = new StreamWriter("func_params.json"))
        // {
        //      tw.WriteLine(json_params.ToString());
        //     tw.Close();
        //  }
        FunctionParams func_params;
        try
        {
            string json_text = File.ReadAllText("func_params.json");
            func_params = JsonUtility.FromJson<FunctionParams>(json_text);
        }
        catch (Exception err)
        {
            func_params = new FunctionParams(1.4, 10, 3.3, 2.5, 0.005);
            Debug.Log("Fail to read params, loading default");

        }

        Weistrasse ws = new Weistrasse();
        y = ws.CreateSignal(max_counter, 0.05,func_params.D, func_params.N, func_params.sigma, func_params.b, func_params.s);

// SceneManager.LoadScene("2D Tetris");

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;

            if (counter > y.Length)
                counter = 0;

            float coef = (float)y[counter++];
            coneMat.SetColor("_EmissionColor", new Color(coef, coef, coef, 1));
        }
    }
}
