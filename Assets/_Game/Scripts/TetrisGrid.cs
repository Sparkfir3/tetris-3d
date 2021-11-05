using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGrid : MonoBehaviour {

    private const int GridWidth = 4, GridHeight = 8;

    [SerializeField] private float blockSize;
    [SerializeField] private Vector3 bottomLeft;
    [SerializeField] private Tetrimino currentTetrimino;
    [SerializeField] private Vector2 tetriminoPosition;

    private Row[] grid;

    // -------------------------------------------------------------------------------

    public Tetrimino CurrentTetrimino {
        get { return currentTetrimino; }
        set { currentTetrimino = value; }
    }

    // -------------------------------------------------------------------------------

    #region Initialization

    private void Awake() {
        grid = new Row[GridHeight];
        for(int i = 0; i < GridHeight; i++)
            grid[i] = new Row(GridWidth);
    }

    #endregion

    // -------------------------------------------------------------------------------

    #region Tetrimino Placement

    /// <summary>
    /// Coverts the current tetrimino into grid blocks, then checks rows to clear & lose condition
    /// </summary>
    public void PlaceTetrimino() {
        // Place
        for(int i = 0; i < 4; i++) {
            PlaceBlock(currentTetrimino.XPos[i] + (int)tetriminoPosition.x, currentTetrimino.YPos[i] + (int)tetriminoPosition.y, currentTetrimino.Blocks[i]);
            currentTetrimino.Blocks[i].transform.parent = transform;
        }
        Destroy(currentTetrimino);
        // TODO - get next

        // Clear rows
        for(int i = 0; i < GridHeight; i++) {
            if(grid[i].IsFull()) {
                grid[i].Destroy();
            }
        }

        // TODO - check win/lose
    }

    private void PlaceBlock(int x, int y, GameObject block) {
        grid[y].row[x] = block;
        block.transform.position = new Vector3(x * blockSize + blockSize / 2, y * blockSize + blockSize / 2, 0f);
    }

    #endregion

    // -------------------------------------------------------------------------------

    #region Tetrimino Movement

    public void MoveLeft() {
        throw new System.NotImplementedException();
    }

    public void MoveRight() {
        throw new System.NotImplementedException();
    }

    public void MoveDown() {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Whether or not the current tetrimino can be moved to the given position
    /// </summary>
    /// <param name="offset">Offset of where the check should occur relative to the tetrimino's current position</param>
    private void RelativePositionOpen(Vector2 offset) {
        throw new System.NotImplementedException();
    }

    #endregion

    // -------------------------------------------------------------------------------

#if UNITY_EDITOR

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position + bottomLeft + (new Vector3(GridWidth / 2f, GridHeight / 2f, 0f) * blockSize), 
            new Vector3(GridWidth * blockSize, GridHeight * blockSize, blockSize));
    }

    public void PrintGrid() {
        string output = "";
        for(int i = GridHeight - 1; i >= 0; i--) {
            output += grid[i].ToString() + "\n";
        }
        Debug.Log(output);
    }

#endif

}

class Row {

    public GameObject[] row;

    public Row(int width) {
        row = new GameObject[width];
    }

    public bool IsFull() {
        foreach(GameObject obj in row) {
            if(!obj)
                return false;
        }
        return true;
    }

    public override string ToString() {
        string output = "";
        foreach(GameObject block in row) {
            output += block != null ? "X" : "O";
        }
        return output;
    }

    public void Destroy() {
        foreach(GameObject block in row) {
            block.GetComponent<Block>().Destroy();
        }
        row = new GameObject[row.Length];
    }

}