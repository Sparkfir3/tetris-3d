using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour {

    [SerializeField] private GameObject[] blocks = new GameObject[4];
    [SerializeField] private int[] blockXPos = new int[4];
    [SerializeField] private int[] blockYPos = new int[4];

    private int width, height;

    // -------------------------------------------------------------------------------

    public GameObject[] Blocks => blocks;
    public int[] XPos => blockXPos;
    public int[] YPos => blockYPos;
    public int Width => width;
    public int Height => height;

    // -------------------------------------------------------------------------------

    #region Initialization

    private void Awake() {
        for(int i = 0; i < 4; i++) {
            width = Mathf.Max(width, blockXPos[i]);
            height = Mathf.Max(height, blockYPos[i]);
        }
        /*width++; // Offset for indexes at 0
        height++;*/
    }

    #endregion

    // -------------------------------------------------------------------------------

    public void RotateClockwise() {
        int oldX, oldY;
        for(int i = 0; i < 4; i++) {
            oldX = blockXPos[i];
            oldY = blockYPos[i];

            blockXPos[i] = oldY;
            blockYPos[i] = width - oldX;
        }

        int oldWidth = width;
        width = height;
        height = oldWidth;
        RepositionBlocks();
    }

    public void RotateCounterclockwise() {
        int oldX, oldY;
        for(int i = 0; i < 4; i++) {
            oldX = blockXPos[i];
            oldY = blockYPos[i];

            blockXPos[i] = height - oldY;
            blockYPos[i] = oldX;
        }

        int oldWidth = width;
        width = height;
        height = oldWidth;
        RepositionBlocks();
    }

    private void RepositionBlocks() {
        for(int i = 0; i < 4; i++) {
            blocks[i].transform.localPosition = new Vector3(blockXPos[i], blockYPos[i], 0f);
        }
    }

}
