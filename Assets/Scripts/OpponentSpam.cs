using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSpam : MonoBehaviour {
    public GameObject opponent;
    public GameObject generatedOpponent;
    public bool triggered;
    public Room room;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.tag == "Player")
        {
            Debug.Log("spam");
            GameObject m = GameObject.Find("Maze Generator (1)");
            Debug.Log(m);
            Maze maze =  m.GetComponent<Maze>();
            GameObject generatedOpponent = Instantiate(opponent, room.midPos, Quaternion.identity) as GameObject;
            generatedOpponent.GetComponent<Opponent>().startCellIndex = room.cells[5];

            // only spam once, so once triggered, destroy the object
            Destroy(gameObject);
            
        }
    }
}
