using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEntrance : MonoBehaviour {
    int enterCnt;
    private bool ableToIncrement;
    public Material[] materials;
    private Renderer rend;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && ableToIncrement)
        {   
            this.enterCnt++;
            this.ableToIncrement = false;
            if (enterCnt < 2)
            {
                rend.sharedMaterial = materials[enterCnt];
            }
            if (enterCnt == 2)
            {
                other.gameObject.layer = 8;
                Destroy(gameObject);
                Debug.Log(other.gameObject.layer);
            }

        } 

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !ableToIncrement)
        {
            ableToIncrement = true;
        }
    }
    // Use this for initialization
    void Start () {
        enterCnt = -1; 
        ableToIncrement = true;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
