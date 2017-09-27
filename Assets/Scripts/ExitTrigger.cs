using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
          
                other.gameObject.layer = 9;
                Debug.Log(other.gameObject.layer);

        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


