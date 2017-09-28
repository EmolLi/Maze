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

    public int startCellIndex;

    private int current;
    private bool reversePath;

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
    void Start() {
        maze = GameObject.Find("Maze Generator (1)").GetComponent<Maze>();

        curCell = startCellIndex;
        Debug.Log(curCell + "sdfds" + startCellIndex);

        visitCellInfo = new bool[maze.xSize * maze.ySize];
        cellPath = new List<int>();
        visitedCellsCnt = 0;
        backUp = new Stack<int>();

        searchPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null) return;
        if (!reversePath)
        {
            if (current >= path.Length)
            {
                reversePath = true;
                current--;
            }
            if (Mathf.Abs(transform.position.z - path[current].z) >= 0.2 || Mathf.Abs(transform.position.x - path[current].x) >= 0.2)
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, path[current], speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }
            else current++;
        }
        else
        {
            // back to original place, reverse path
            if (current < 0)
            {
                reversePath = false;
                current++;
            }
            if (Mathf.Abs(transform.position.z - path[current].z) >= 0.2 || Mathf.Abs(transform.position.x - path[current].x) >= 0.2)
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, path[current], speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }
            else current--;
        }
    }


    void searchPath()
    {
        visitCell(curCell); // starting point
        int infinite = 600;
        while (visitedCellsCnt < maze.cells.Length && infinite > 0) // FIXME: if time permits
        {
            int next = findNextCell(curCell);
            if (next == -1)
            {
                // back up
                if (backUp.Count <= 0)
                {
                    Debug.Log("back up = 0");
                    break;
                }
                else
                {
                    curCell = backUp.Pop();
                };
            }
            else curCell = next;
            visitCell(curCell);

            infinite--;
        }
        if (infinite <= 0) Debug.Log("inifinite");

        // build vector path
        path = new Vector3[cellPath.Count];
        for (int i = 0; i < cellPath.Count; i++)
        {
            path[i] = maze.cells[cellPath[i]].pos;
        }
    }

    // visit a cell
    void visitCell(int cellIndex)
    {

        cellPath.Add(cellIndex);
        if (!visitCellInfo[cellIndex])
        {
            visitedCellsCnt++;
            backUp.Push(cellIndex);
            visitCellInfo[cellIndex] = true;
        }
    }

    int findNextCell(int cellIndex)
    {
        for (int i = 0; i < maze.cells[cellIndex].neighbors.Count; i++)
        {
            int neighbor = maze.cells[cellIndex].neighbors[i];
            if (!visitCellInfo[neighbor]) return neighbor;
        }
        // no unvisited neighbor
        return -1;
    }


    
}
