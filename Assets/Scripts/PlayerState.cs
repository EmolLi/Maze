using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public bool isDead;
    public int keyCnt = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "opponent")
        {
            isDead = true;
            Debug.Log("oppo");

        }

        if (other.tag == "key")
        {
            keyCnt++;
            Destroy(other.gameObject);
        }

        if (other.name == "exit")
        {
            //if 
        }

    }

}
