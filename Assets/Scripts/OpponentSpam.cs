using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSpam : MonoBehaviour {
    public GameObject opponent;
    public GameObject generatedOpponent;
    public bool triggered;
    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.tag == "Player")
        {
            Debug.Log("spam");
            GameObject m = GameObject.Find("Maze Generator (1)");
            Debug.Log(m);
            Maze maze =  m.GetComponent<Maze>();
            GameObject generatedOpponent = Instantiate(opponent, maze.cells[0].pos + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
            
        }

    }
}
