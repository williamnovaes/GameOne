using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehaviour : MonoBehaviour {
    
    [SerializeField] private GameObject fireballSpawn;
    void Update() {
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        if (Input.GetButtonDown("Fire1")) {
            GetComponent<Animator> ().SetTrigger("Attack");
            yield return new WaitForSeconds(.78f);
            fireballSpawn.GetComponent<FireballSpawn> ().Create();
        }
    }
}
