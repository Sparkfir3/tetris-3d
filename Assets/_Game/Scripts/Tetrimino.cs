using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour {

    [SerializeField] private GameObject[] blocks = new GameObject[4];
    [SerializeField] private int[] blockXPos = new int[4];
    [SerializeField] private int[] blockYPos = new int[4];

    public GameObject[] Blocks => blocks;
    public int[] XPos => blockXPos;
    public int[] YPos => blockYPos;

    public void RotateClockwise() {
        throw new System.NotImplementedException();
    }

    public void RotateCounterclockwise() {
        throw new System.NotImplementedException();
    }

}
