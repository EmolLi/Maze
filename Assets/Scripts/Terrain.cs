using System.Collections;
using UnityEngine;

public class Terrain : MonoBehaviour {
    public int mDivisions;  // FIXME: number of faces
    public float mSize;     // size of the terrain (e.g 10 x 10）
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


        mesh.vertices = mVerts;
        mesh.uv = uvs;
        mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
