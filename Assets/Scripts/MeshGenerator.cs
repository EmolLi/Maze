using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {
    Vector3[] mVerts;
    Vector2[] mUV;
    int[] mTris;    
	// Use this for initialization
	void Start () {
        mVerts = new Vector3[4];
        mUV = new Vector2[4];
        mTris = new int[2 * 3];

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mVerts[0] = new Vector3(-1.0f, 0.0f, 1.0f);
        mVerts[1] = new Vector3(1.0f, 0.0f, 1.0f);
        mVerts[2] = new Vector3(-1.0f, 0.0f, -1.0f);
        mVerts[3] = new Vector3(1.0f, 0.0f, -1.0f);

        mUV[0] = new Vector2(0.0f, 0.0f);
        mUV[1] = new Vector2(1.0f, 0.0f);
        mUV[2] = new Vector2(0.0f, 1.0f);
        mUV[3] = new Vector2(1.0f, 1.0f);

        mTris[0] = 0;
        mTris[1] = 1;
        mTris[2] = 3;

        mTris[3] = 0;
        mTris[4] = 3;
        mTris[5] = 2;

        mesh.vertices = mVerts;
        mesh.uv = mUV;
        mesh.triangles = mTris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

    }

}
