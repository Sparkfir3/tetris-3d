using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private TetrisGrid grid;
    [SerializeField] private int tetriminoQueueCount;
    [SerializeField] private List<GameObject> nextTetriminos;
    [SerializeField] private List<GameObject> allTetriminos;
    private List<GameObject> tetriminoStash = new List<GameObject>();

    // ---

    [Header("Queue")]
    [SerializeField] private Vector3 queuePosition;
    [SerializeField] private Vector3 queueScale;
    [SerializeField] private float queueOffset;
    [SerializeField] private bool displayQueueGizmo;

    // -------------------------------------------------------------------------------

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
            if(Input.GetKey(KeyCode.LeftShift))
                grid.HardDrop();
            else
                grid.MoveDown();
        }

        else if(Input.GetKeyDown(KeyCode.E)) {
            grid.CurrentTetrimino.RotateClockwise();
            grid.OnRotateTetrimino.Invoke();
        } else if(Input.GetKeyDown(KeyCode.Q)) {
            grid.CurrentTetrimino.RotateCounterclockwise();
            grid.OnRotateTetrimino.Invoke();
        }
    }

    private void SpawnNextTetrimino() {
        nextTetriminos[0].transform.localScale = Vector3.one;
        grid.SetNewTetrimino(nextTetriminos[0]);
        nextTetriminos.RemoveAt(0);
        FillNextTetriminos();
        RenderQueue();
    }

    private void FillNextTetriminos() {
        if(tetriminoStash.Count == 0) {
            tetriminoStash = new List<GameObject>(allTetriminos);
        }

        // Grab random from stash, spawn it and push to nextTetriminos, then delete from stash
        int index = Random.Range(0, tetriminoStash.Count - 1);
        GameObject newTetrimino = Instantiate(tetriminoStash[index], new Vector3(-20, -20), Quaternion.identity);
        newTetrimino.transform.localScale = queueScale;
        nextTetriminos.Add(newTetrimino);
        tetriminoStash.RemoveAt(index);

        if(nextTetriminos.Count < tetriminoQueueCount)
            FillNextTetriminos();
    }

    private void RenderQueue() {
        Vector3 nextPosition = queuePosition;
        for(int i = 0; i < nextTetriminos.Count; i++) {
            nextTetriminos[i].transform.position = nextPosition;
            nextPosition += new Vector3(0f, -queueOffset, 0f);
        }
    }

    // -------------------------------------------------------------------------------

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if(displayQueueGizmo) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(queuePosition, Vector3.one);
        }
    }
#endif

}
