using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawn : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;

    public void Create() {
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
    }
}
