using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Opponent : MonoBehaviour {
    public Maze maze;
    public bool[] visitCellInfo;
    public int visitedCellsCnt;
    public Stack<int> backUp;    // path made of cell index

    public int curCell;
    public List<int> cellPath;  // path made of cell index
    public Vector3[] path;
    public float speed;
    public Vector3 rotation;

    private int current;

    /**
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
    }**/
    // Use this for initialization
    void Start () {
        visitCellInfo = new bool[maze.xSize * maze.ySize];
        cellPath = new List<int>();
        visitedCellsCnt = 0;
        backUp = new Stack<int>();
    }
    /**
	// Update is called once per frame
	void Update () {
        if (current >= path.Length) return;
        if (transform.position.z != path[current].z || transform.position.x != path[current].x)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, path[current], speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else current = current + 1;
	}**/


    void searchPath()
    {
        visitCell(curCell); // starting point

        while (visitedCellsCnt < maze.cells.Length)
        {
            
        }
    }

    // visit a unvisited cell
    void visitCell(int cellIndex)
    {
        cellPath.Add(cellIndex);  // add starting place
        visitedCellsCnt++;
        backUp.Push(cellIndex);
        visitCellInfo[cellIndex] = true;
    }
}
