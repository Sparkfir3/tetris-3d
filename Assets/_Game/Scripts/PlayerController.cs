using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private TetrisGrid grid;
    [SerializeField] private int tetriminoQueueCount;
    [SerializeField] private List<GameObject> nextTetriminos;
    [SerializeField] private List<GameObject> allTetriminos;
    private List<GameObject> tetriminoStash = new List<GameObject>();

    private void Awake() {
        FillNextTetriminos();
    }

    private void Update() {
        if(Input.GetButtonDown("Left")) {
            grid.MoveLeft();
        } else if(Input.GetButtonDown("Right")) {
            grid.MoveRight();
        } else if(Input.GetButtonDown("Down")) {
            grid.MoveDown();
        }
    }

    private void SpawnNextTetrimino() {
        grid.CurrentTetrimino = nextTetriminos[0].GetComponent<Tetrimino>();
    }

    private void FillNextTetriminos() {
        if(tetriminoStash.Count == 0) {
            tetriminoStash = new List<GameObject>(allTetriminos);
        }

        // TODO - grab random from stash, spawn it and push to nextTetriminos, then delete from stash
        throw new System.NotImplementedException();

        if(nextTetriminos.Count < tetriminoQueueCount)
            FillNextTetriminos();
    }

}
