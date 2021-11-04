using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] private GameObject breakVfx;
    [SerializeField] private float vfxLifeTime;

    private void Awake() {
        breakVfx.SetActive(false);
    }

    public void Destroy() {
        StartCoroutine(DestroyBlock());
    }

    private IEnumerator DestroyBlock() {
        GetComponent<MeshRenderer>().enabled = false;
        breakVfx.transform.parent = null;
        breakVfx.SetActive(true);
        yield return new WaitForSeconds(vfxLifeTime);
        Destroy(breakVfx);
        Destroy(gameObject);
    }

}
