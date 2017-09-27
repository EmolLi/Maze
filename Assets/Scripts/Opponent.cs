using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour {


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "bullet")
        {
            Debug.Log("hit");
            hitByBullet(collision.collider.gameObject);
        }

        if (collision.collider.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            hitByBullet(collision.collider.gameObject);
        }

    }

    void hitByBullet(GameObject bullet)
    {
        int random = Random.Range(0, 100);
        Debug.Log(random);
        if (random < 25)
        {
            // destoryed by the bullet
            Destroy(bullet);
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
