using System.Collections;
using UnityEngine;

public class Terrain : MonoBehaviour {
    public int mDivisions;  // FIXME: number of faces
    public float mSize;
    public float length;     // size of the terrain (e.g 10 x 10）
    public float mHeight;   // max height of the terrain

    Vector3[] mVerts;
    int mVertCount;     // vertice cnt

	// Use this for initialization
	void Start () {
        CreateTerrain();
	}
	
    void CreateTerrain()
    {
        mVertCount = (mDivisions + 1) * (mDivisions + 1);
        mVerts = new Vector3[mVertCount];
        Vector2[] uvs = new Vector2[mVertCount];
        int[] tris = new int[mDivisions * mDivisions * 2 * 3];  // # of triangles, 3-> triangle is defined by 3 numbers

        float halfSize = mSize * 0.5f;
        float divisionSize = mSize / mDivisions;

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int triOffset = 0;

        for(int i = 0; i <= mDivisions; i++)
        {
            for(int j = 0; j <= mDivisions; j++)
            {
                mVerts[i * (mDivisions + 1) + j] = new Vector3(-halfSize + j * divisionSize, 0.0f, halfSize - i * divisionSize); // we are using one dimensional array to represent two dimensional matrix
                uvs[i * (mDivisions + 1) + j] = new Vector2((float)i / mDivisions, (float)j / mDivisions);

                if (i < mDivisions && j < mDivisions)
                {
                    int topLeft = i * (mDivisions + 1) + j;
                    int bottomLeft = (i + 1) * (mDivisions + 1) + j;

                    tris[triOffset] = topLeft;
                    tris[triOffset + 1] = topLeft + 1;
                    tris[triOffset + 2] = bottomLeft + 1;

                    tris[triOffset + 3] = topLeft;
                    tris[triOffset + 4] = bottomLeft + 1;
                    tris[triOffset + 5] = bottomLeft;

                    triOffset += 6;
                }
            }
        }


        mVerts[0].y = Random.Range(-mHeight, mHeight);              // top left corner
        mVerts[mDivisions].y = Random.Range(-mHeight, mHeight);     // top right corner
        mVerts[mVerts.Length - 1].y = Random.Range(-mHeight, mHeight);                // bottom right corner
        mVerts[mVerts.Length - mDivisions - 1].y = Random.Range(-mHeight, mHeight);

        int iterations = (int)Mathf.Log(mDivisions, 2);
        int numSquares = 1;
        int squareSize = mDivisions;
        for (int i = 0; i < iterations; i++)
        {
            int row = 0;
            for (int j = 0; j < numSquares; j++)
            {
                int col = 0;
                for (int k = 0; k < numSquares; k++)
                {
                    DiamondSquare(row, col, squareSize, mHeight);
                    col += squareSize;
                }
                row += squareSize;
            }
            numSquares *= 2;
            squareSize /= 2;
            mHeight *= 0.5f;

        }



        mesh.vertices = mVerts;
        mesh.uv = uvs;
        mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

	// Update is called once per frame
	void Update () {
		
	}

    // run diamondSquare on each square
    void DiamondSquare(int row, int col, int size, float offset)
    {
        int halfSize = (int)(size * 0.5f);
        int topLeft = row * (mDivisions + 1) + col;
        int bottomLeft = (row + size) * (mDivisions + 1) + col;

        // middle point of the square
        // diamond part
        int mid = (int)(row + halfSize) * (mDivisions + 1) + (int)(col + halfSize);
        mVerts[mid].y = (mVerts[topLeft].y + mVerts[topLeft + size].y + mVerts[bottomLeft].y + mVerts[bottomLeft + size].y) * 0.25f + Random.Range(-offset, offset);

        // square part
        mVerts[topLeft + halfSize].y = (mVerts[topLeft].y + mVerts[topLeft + size].y + mVerts[mid].y) / 3 + Random.Range(-offset, offset);
        mVerts[mid - halfSize].y = (mVerts[topLeft].y + mVerts[bottomLeft].y + mVerts[mid].y) / 3 + Random.Range(-offset, offset);
        mVerts[mid + halfSize].y = (mVerts[topLeft + size].y + mVerts[bottomLeft + size].y + mVerts[mid].y) / 3 + Random.Range(-offset, offset);
        mVerts[bottomLeft + halfSize].y = (mVerts[bottomLeft].y + mVerts[bottomLeft + size].y + mVerts[mid].y) / 3 + Random.Range(-offset, offset);
    }
}
