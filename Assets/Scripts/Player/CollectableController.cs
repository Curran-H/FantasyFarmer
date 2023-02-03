using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.transform.parent.name == "Player") {
            Destroy(gameObject);
        }
    }
}
