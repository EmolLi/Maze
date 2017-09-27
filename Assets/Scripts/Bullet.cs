using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "wall")
        {
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            Debug.Log("trigger");
            Destroy(gameObject);

        }

    }

}
