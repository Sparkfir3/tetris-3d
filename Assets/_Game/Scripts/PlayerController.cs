using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private TetrisGrid grid;
    [SerializeField] private int tetriminoQueueCount;
    [SerializeField] private List<GameObject> nextTetriminos;
    [SerializeField] private List<GameObject> allTetriminos;
    private List<GameObject> tetriminoStash = new List<GameObject>();

    private void Start() {
        FillNextTetriminos();
        grid.OnPlaceTetrimino.AddListener(() => { SpawnNextTetrimino(); });
        SpawnNextTetrimino();
    }

    private void Update() {
        if(Input.GetButtonDown("Left")) {
            grid.MoveLeft();
        } else if(Input.GetButtonDown("Right")) {
            grid.MoveRight();
        } else if(Input.GetButtonDown("Down")) {
            grid.MoveDown();
        } else if(Input.GetKeyDown(KeyCode.Space)) {
            grid.PlaceTetrimino();
        }
    }

    private void SpawnNextTetrimino() {
        grid.SetNewTetrimino(nextTetriminos[0]);
        nextTetriminos.RemoveAt(0);
        FillNextTetriminos();
    }

    private void FillNextTetriminos() {
        if(tetriminoStash.Count == 0) {
            tetriminoStash = new List<GameObject>(allTetriminos);
        }

        // Grab random from stash, spawn it and push to nextTetriminos, then delete from stash
        int index = Random.Range(0, tetriminoStash.Count - 1);
        GameObject newTetrimino = Instantiate(tetriminoStash[index], new Vector3(-5, -5), Quaternion.identity);
        nextTetriminos.Add(newTetrimino);
        tetriminoStash.RemoveAt(index);

        if(nextTetriminos.Count < tetriminoQueueCount)
            FillNextTetriminos();
    }

}
