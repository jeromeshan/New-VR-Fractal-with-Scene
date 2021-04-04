using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    public float upForse =15f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up*upForse,ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Blade")
        {
            Vector3 direction = (collider.transform.position - transform.position).normalized;
            Quaternion rotation= Quaternion.LookRotation(direction);
            GameObject slicedFruit = Instantiate(slicedFruitPrefab,transform.position,rotation);
            Destroy(slicedFruit, 5f);
            Destroy(gameObject);
        }
    }
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Blade")
    //    {
    //        Vector3 direction = (collision.collider.transform.position - transform.position).normalized;
    //        Quaternion rotation = Quaternion.LookRotation(direction);
    //        GameObject slicedFruit = Instantiate(slicedFruitPrefab, transform.position, rotation);
    //        Destroy(slicedFruit, 5f);
    //        Destroy(gameObject);
    //    }
    //}
}
