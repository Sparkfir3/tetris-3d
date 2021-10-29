using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    private const int GridWidth = 5, GridHeight = 7;

    [SerializeField] private float blockSize;
    [SerializeField] private Vector3 bottomLeft;
    [SerializeField] private Tetrimino currentTetrimino;

    private Row[] grid;

    // -------------------------------------------------------------------------------

    private void Awake() {
        grid = new Row[GridHeight];
        for(int i = 0; i < GridHeight; i++)
            grid[i] = new Row(GridWidth);
    }

    // -------------------------------------------------------------------------------

    /// <summary>
    /// Coverts the current tetrimino into grid blocks, then checks rows to clear & lose condition
    /// </summary>
    private void PlaceTetrimino() {
        throw new System.NotImplementedException();
    }

    // -------------------------------------------------------------------------------

#if UNITY_EDITOR

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position + bottomLeft + (new Vector3(GridWidth / 2f, GridHeight / 2f, 0f) * blockSize), 
            new Vector3(GridWidth * blockSize, GridHeight * blockSize, blockSize));
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

}