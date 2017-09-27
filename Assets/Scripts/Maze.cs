using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Maze : MonoBehaviour
{

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
    public class Room
    {
        public bool visited;
        public int[] cells;

        public Room(int bottomLeftCell, int xSize, int ySize)
        {
            cells = new int[16];
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    cells[row * 4 + col] = bottomLeftCell + row * xSize + col;
                }
            }
        }
    }






    public Vector3 position;
    public int mazeEntry;


    public GameObject wall;
    public GameObject exit;
    public float wallLength = 1.0f;
    public int xSize = 30;   // 30 rows in x axis
    public int ySize = 30;   // 30 rows in y axis
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

    public Room[] rooms;
    public Room curRoom;
    public int[] selectRoom;
    public bool ableToCreateRoom;


    //public 

    // Use this for initialization
    void Start()
    {
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

            if (termCnt == xSize)
            {
                eastWestProcess++;
                termCnt = 0;
            }
            cells[cellProcess] = new Cell();
            cells[cellProcess].east = allWalls[eastWestProcess];
            cells[cellProcess].south = allWalls[childProcess + (xSize + 1) * ySize];

            eastWestProcess++;

            termCnt++;
            childProcess++;
            cells[cellProcess].west = allWalls[eastWestProcess];
            cells[cellProcess].north = allWalls[childProcess + (xSize + 1) * ySize + xSize - 1];

            cells[cellProcess].pos = cells[cellProcess].north.transform.position - new Vector3(0, 0, wallLength * 0.5f);
        }

    }



    void CreateMaze()
    {
        // create entry
        Destroy(cells[mazeEntry].east);
        CreateRooms();
        /**
        while (visitedCells < totalCells)
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
        }

            // end of the maze
            //Debug.Log(currentCell);
            GameObject exitObj = Instantiate(exit, cells[currentCell].pos, Quaternion.identity) as GameObject;
            //float scale = wallLength / 
            exitObj.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f); 
            //exitObj.transform.parent = wallHolder.transform;**/

    }



    void CreateRooms()
    {
        rooms = new Room[3];
        /**
        int roomIndex = 0;
        foreach (int i in selectRoom)
        {
            if (checkIfAbleToCreateRoom(i))
            {
                rooms[roomIndex] = curRoom;
                roomIndex++;

                for (int j = 0; j < 16; j++)
                {
                    if (j % 4 != 3) Destroy(cells[curRoom.cells[j]].west);
                    if (j / 4 != 3) Destroy(cells[curRoom.cells[j]].north);
                }
            }


        } **/

        int bottomLeftCell;
        int j = 0;
        int maxCnt = 5;
        while (j < 3 && maxCnt > 0)
        {
            int row = UnityEngine.Random.Range(1, xSize - 4);
            int col = UnityEngine.Random.Range(1, ySize - 4);
            // check if we can build a 4 * 4 room 
            bottomLeftCell = row * xSize + col;    // exclude 0 -> 0 is the entrance
            ableToCreateRoom = checkIfAbleToCreateRoom(bottomLeftCell);
            //Debug.Log(bottomLeftCell);
            //Debug.Log(ableToCreateRoom);
            if (ableToCreateRoom)
            {
                rooms[j] = curRoom;
                for (int i = 0; i < 16; i++)
                {
                    if (i % 4 != 3) Destroy(cells[curRoom.cells[i]].west);
                    if (i / 4 != 3) Destroy(cells[curRoom.cells[i]].north);
                }
                j++;
            }
            maxCnt--;

        }



    }

    bool checkIfAbleToCreateRoom(int bottomLeftCell)
    {
        Debug.Log(bottomLeftCell + " run");
        if (xSize - bottomLeftCell % xSize >= 4
                    && ySize - bottomLeftCell / xSize >= 4)        // 4 is room size
            {
                Room r = new Room(bottomLeftCell, xSize, ySize);
                // check if overlap with other rooms 
                // if the room overlaps with another room, one of the vertices of the other room must be in this room
                for (int i = 0; i < 3; i++)
                {
                    Room room = rooms[i];
                    if (room != null)
                    {
                        if (Array.Exists(r.cells, c => (c == room.cells[0] || c == room.cells[4] || c == room.cells[12] || c == room.cells[15])))
                    {
                        Debug.Log(bottomLeftCell + "   false");
                        return false;
                    }

                    }
                    Debug.Log("here");
                }
            curRoom = r;
                Debug.Log(bottomLeftCell + "   true");
                return true;
            }
        Debug.Log(bottomLeftCell + "   false2" + (xSize - bottomLeftCell % xSize) + "   " + (ySize - bottomLeftCell / xSize) + "fsdf");
        return false;
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
            if (cells[currentCell + 1].visited == false && !(Array.Exists(rooms, r => Array.Exists(r.cells, c => c == currentCell + 1) && r.visited)))
            {
                neighbors[length] = currentCell + 1;
                connectingWall[length] = 3;
                length++;
            }
        }

        // east
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false && !(Array.Exists(rooms, r => Array.Exists(r.cells, c => c == currentCell - 1) && r.visited)))
            {
                neighbors[length] = currentCell - 1;
                connectingWall[length] = 2;
                length++;
            }
        }

        // north
        if (currentCell + xSize < totalCells)
        {
            if (cells[currentCell + xSize].visited == false && !(Array.Exists(rooms, r => Array.Exists(r.cells, c => c == currentCell + xSize) && r.visited)))
            {
                neighbors[length] = currentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }

        // south
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false && !(Array.Exists(rooms, r => Array.Exists(r.cells, c => c == currentCell - xSize) && r.visited)))
            {
                neighbors[length] = currentCell - xSize;
                connectingWall[length] = 4;
                length++;
            }
        }

        if (length > 0)
        {
            int randomNeighbor = UnityEngine.Random.Range(0, length);
            currentNeighbor = neighbors[randomNeighbor];
            wallToBreak = connectingWall[randomNeighbor];
            int room = Array.FindIndex(rooms, r => Array.Exists(r.cells, c => c == currentNeighbor));
            if (room >= 0) rooms[room].visited = true;

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
    void Update()
    {

    }
}
