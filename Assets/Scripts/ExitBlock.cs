using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBlock : MonoBehaviour {
    public BoxCollider block;
    public PlayerState playerState;

    private void Awake()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update () {
        if (playerState.keyCnt == 3)
        {
            block.enabled = false;
        }
    }
}
