using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TetrisGrid))]
public class GridEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        TetrisGrid grid = target as TetrisGrid;

        if(GUILayout.Button("Place Tetrimino")) {
            grid.PlaceTetrimino();
        }
        if(GUILayout.Button("Print Grid")) {
            grid.PrintGrid();
        }
    }

}
