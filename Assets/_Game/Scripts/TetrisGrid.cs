using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TetrisGrid : MonoBehaviour {

    private const int GridWidth = 8, GridHeight = 10;

    [SerializeField] private float blockSize;
    [SerializeField] private Vector3 bottomLeft;
    [SerializeField] private Tetrimino currentTetrimino;
    [SerializeField] private Vector2 tetriminoPosition;

    public UnityEvent OnPlaceTetrimino;

    private Row[] rows;

    // -------------------------------------------------------------------------------

    public Tetrimino CurrentTetrimino {
        get { return currentTetrimino; }
        set { currentTetrimino = value; }
    }

    // -------------------------------------------------------------------------------

    #region Initialization

    private void Awake() {
        rows = new Row[GridHeight];
        for(int i = 0; i < GridHeight; i++)
            rows[i] = new Row(GridWidth);
    }

    #endregion

    // -------------------------------------------------------------------------------

    #region Tetrimino Placement

    public void SetNewTetrimino(GameObject tetrimino) {
        CurrentTetrimino = tetrimino.GetComponent<Tetrimino>();
        tetriminoPosition = new Vector2(GridWidth / 2 - 1, GridHeight - 4);
        currentTetrimino.transform.parent = transform;
        PositionCurrentTetrimino();
    }

    /// <summary>
    /// Coverts the current tetrimino into grid blocks, then checks rows to clear & lose condition
    /// </summary>
    public void PlaceTetrimino() {
        // Place
        for(int i = 0; i < 4; i++) {
            currentTetrimino.Blocks[i].transform.parent = transform;
            PlaceBlock(currentTetrimino.XPos[i] + (int)tetriminoPosition.x, currentTetrimino.YPos[i] + (int)tetriminoPosition.y, currentTetrimino.Blocks[i]);
        }
        Destroy(currentTetrimino.gameObject);
        // TODO - get next

        // Clear rows
        for(int i = 0; i < GridHeight; i++) {
            if(rows[i].IsFull()) {
                rows[i].Destroy();
            }
        }

        // TODO - check win/lose

        OnPlaceTetrimino.Invoke();
    }

    private void PlaceBlock(int x, int y, GameObject block) {
        rows[y].row[x] = block;
        block.transform.localPosition = new Vector3(x * blockSize + blockSize / 2, y * blockSize + blockSize / 2, 0f);
    }

    #endregion

    // -------------------------------------------------------------------------------

    #region Tetrimino Movement

    public void MoveLeft() {
        if(RelativePositionOpen(new Vector2(-1, 0))) {
            tetriminoPosition += new Vector2(-1, 0);
            PositionCurrentTetrimino();
        }
    }

    public void MoveRight() {
        if(RelativePositionOpen(new Vector2(1, 0))) {
            tetriminoPosition += new Vector2(1, 0);
            PositionCurrentTetrimino();
        }
    }

    public void MoveDown() {
        if(RelativePositionOpen(new Vector2(0, -1))) {
            tetriminoPosition += new Vector2(0, -1);
            PositionCurrentTetrimino();
        } else {
            PlaceTetrimino();
        }
    }

    private void PositionCurrentTetrimino() {
        currentTetrimino.transform.localPosition = new Vector3(tetriminoPosition.x * blockSize + blockSize / 2, tetriminoPosition.y * blockSize + blockSize / 2, 0);
    }

    /// <summary>
    /// Whether or not the current tetrimino can be moved to the given position
    /// </summary>
    /// <param name="offset">Offset of where the check should occur relative to the tetrimino's current position</param>
    private bool RelativePositionOpen(Vector2 offset) {
        int i = 0;
        foreach(GameObject block in currentTetrimino.Blocks) {
            if(!IsPositionOpen(currentTetrimino.XPos[i] + (int)(tetriminoPosition.x + offset.x), currentTetrimino.YPos[i] + (int)(tetriminoPosition.y + offset.y)))
                return false;
            i++;
        }
        return true;
    }

    private bool IsPositionOpen(int x, int y) {
        // Out of bounds
        if(x < 0 || x > GridWidth - 1 || y < 0 || y > GridHeight - 1)
            return false;

        // Block occupied
        return rows[y].row[x] == null;
    }

    #endregion

    // -------------------------------------------------------------------------------

    #region Debug
#if UNITY_EDITOR

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position + bottomLeft + (new Vector3(GridWidth / 2f, GridHeight / 2f, 0f) * blockSize), 
            new Vector3(GridWidth * blockSize, GridHeight * blockSize, blockSize));
    }

    public void PrintGrid() {
        string output = "";
        for(int i = GridHeight - 1; i >= 0; i--) {
            output += rows[i].ToString() + "\n";
        }
        Debug.Log(output);
    }

#endif
    #endregion

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