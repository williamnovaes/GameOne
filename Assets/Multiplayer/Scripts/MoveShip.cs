using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class MoveShip : NetworkBehaviour {
    
    [SerializeField] private float speed;
    private Rigidbody2D playerRb;

    private void Start() {
        playerRb = GetComponent<Rigidbody2D> ();
    }
    
    private void FixedUpdate() {
        if (this.isLocalPlayer) {
            float movement = Input.GetAxis("Horizontal");
            playerRb.velocity= new Vector2(movement * speed, 0f);
        }
    }
}
