using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour {

    [SerializeField] private ParticleSystem breakParticles;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
                if(hit.collider.gameObject == gameObject) {
                    breakParticles.gameObject.transform.parent = transform.parent;
                    breakParticles.Play();
                    Destroy(gameObject);
                }
            }
        }
    }

}
