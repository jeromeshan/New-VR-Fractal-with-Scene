using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Weistrasse ws = new Weistrasse();
        y = ws.CreateSignal(max_counter, 0.05);
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;

            if (counter == max_counter)
                counter = 0;

            float coef = (float)y[counter++];
            coneMat.SetColor("_EmissionColor", new Color(coef, coef, coef, 1));
        }
    }
}
