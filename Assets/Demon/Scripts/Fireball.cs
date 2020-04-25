using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    void Awake() {
        StartCoroutine(Create());
    }
    IEnumerator Create() {
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }
}
