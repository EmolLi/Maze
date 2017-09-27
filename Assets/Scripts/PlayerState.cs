using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public bool isDead;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "opponent")
        {
            isDead = true;
            Debug.Log("oppo");

        }

    }

}
