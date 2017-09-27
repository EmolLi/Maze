using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    void OnCollisionEnter(Collision collision)
    {
        // maze wall
        if (collision.collider.gameObject.tag == "wall")
        {
            Debug.Log("hit");
            Destroy(gameObject);
        }
        // opponent
        if (collision.collider.gameObject.tag == "opponent")
        {
            Debug.Log("oppo hit");

            int random = Random.Range(0, 100);
            Debug.Log(random);
            if (random < 25)
            {
                // destoryed by the bullet
                Destroy(collision.collider.gameObject);
                Destroy(gameObject);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        // maze exit & entrance
        if (other.tag == "wall")
        {
            Debug.Log("trigger");
            Destroy(gameObject);

        }

    }

}
