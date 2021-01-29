using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{

    public GameObject[] Tetrominoes;
    // Start is called before the first frame update
    void Start()
    {
        NewTetraminoes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewTetraminoes()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }
}
