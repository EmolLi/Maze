using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public Vector3 pos;
        public GameObject north;    // 1
        public GameObject east;     // 2    
        public GameObject west;     // 3
        public GameObject south;    // 4
    }






    public Vector3 position;
    public int mazeEntry;


    public GameObject wall;
    public GameObject exit;
    public float wallLength = 1.0f;
    public int xSize = 5;   // 5 rows in x axis
    public int ySize = 5;   // 5 rows in y axis
    private Vector3 initialPos;
    private GameObject wallHolder;

    public Cell[] cells;
    public int currentCell = 0;
    private int totalCells;
    private int visitedCells;   // cnt of visitedCells
    private bool startedBuilding;
    private int currentNeighbor;
    private List<int> lastCells = new List<int>();
    private int backingUp = 0;
    private int wallToBreak;

    // Use this for initialization
    void Start () {
        CreateWalls();
	}
	
    void CreateWalls()
    {
        wallHolder = new GameObject();
        wallHolder.name = "Maze";

        initialPos = new Vector3((-xSize / 2) + wallLength / 2, 0.0f, (-ySize / 2
            ) + wallLength / 2) + position;
        Vector3 myPos = initialPos;
        GameObject tempWall;

        // For x Axis
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, 0.0f, initialPos.z + (i * wallLength) - wallLength / 2);
                tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }

        // For y Axis
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength), 0.0f, initialPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }

        createCells();
        CreateMaze();
    }

    void createCells()
    {
        int children = wallHolder.transform.childCount;
        GameObject[] allWalls = new GameObject[children];
        totalCells = xSize * ySize;
        cells = new Cell[totalCells];

        int eastWestProcess = 0;
        int childProcess = 0;
        int termCnt = 0;

        // Get all the children
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        // Assign walls to the cells 
        for (int cellProcess = 0; cellProcess < cells.Length; cellProcess++)
        {
            cells[cellProcess] = new Cell();
            cells[cellProcess].east = allWalls[eastWestProcess];
            cells[cellProcess].south = allWalls[childProcess + (xSize + 1) * ySize];
            if (termCnt == xSize)
            {
                eastWestProcess += 2;
                termCnt = 0;
            }
            else
            {
                eastWestProcess++;
            }
            termCnt++;
            childProcess++;
            cells[cellProcess].west = allWalls[eastWestProcess];
            cells[cellProcess].north = allWalls[childProcess + (xSize + 1) * ySize + xSize -1];

            cells[cellProcess].pos = cells[cellProcess].north.transform.position - new Vector3(0, 0, wallLength * 0.5f);
        }

    }


    void createFloor()
    {

    }

    void CreateMaze()
    {
        // create entry
        Destroy(cells[mazeEntry].east);

        if (visitedCells < totalCells)
        {
            if (startedBuilding)
            {
                SelectNeighbor();
                if (cells[currentNeighbor].visited == false && cells[currentCell].visited == true)
                {
                    BreakWall();
                    cells[currentNeighbor].visited = true;
                    visitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbor;
                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                currentCell = mazeEntry;
                //currentCell = Random.Range(0, totalCells);
                cells[currentCell].visited = true;
                visitedCells++;
                startedBuilding = true;
            }
            Invoke("CreateMaze", 0.0f);
        }

        else
        {
            // end of the maze
            Debug.Log(currentCell);
            GameObject exitObj = Instantiate(exit, cells[currentCell].pos, Quaternion.identity) as GameObject;
            //float scale = wallLength / 
            exitObj.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f); 
            //exitObj.transform.parent = wallHolder.transform;

        }
    }

    void BreakWall()
    {
        switch (wallToBreak)
        {
            case 1: Destroy(cells[currentCell].north); break;
            case 2: Destroy(cells[currentCell].east); break;
            case 3: Destroy(cells[currentCell].west); break;
            case 4: Destroy(cells[currentCell].south); break;
        }
    }

    void SelectNeighbor()
    {
        int length = 0;
        int[] neighbors = new int[4];
        int[] connectingWall = new int[4];
        int check = 0;  // check if we are cornering the cell
        check = (currentCell + 1) / xSize;
        check -= 1;
        check *= xSize;
        check += xSize;
        // west
        if (currentCell + 1 < totalCells && (currentCell + 1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighbors[length] = currentCell + 1;
                connectingWall[length] = 3;
                length++;
            }
        }

        // east
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbors[length] = currentCell - 1;
                connectingWall[length] = 2;
                length++;
            }
        }

        // north
        if (currentCell + xSize < totalCells)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighbors[length] = currentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }

        // south
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighbors[length] = currentCell - xSize;
                connectingWall[length] = 4;
                length++;
            }
        }

        if (length > 0)
        {
            int randomNeighbor = Random.Range(0, length);   //FIXME: length? or lenght - 1
            currentNeighbor = neighbors[randomNeighbor];
            wallToBreak = connectingWall[randomNeighbor];

        }
        else
        {
            if (backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--; 
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
