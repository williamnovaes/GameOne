using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOne {
    public class Parallax : MonoBehaviour
    {
        private Transform cam;
        [SerializeField] private float lenght, startPos;
        [SerializeField] private float parallaxSpeed;
        
        void Awake() {
            startPos = transform.position.x;
            cam = Camera.main.transform;
            //lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        }
        void FixedUpdate() {
            float temp = cam.position.x * (1 - parallaxSpeed);
            float dist = cam.position.x * parallaxSpeed;

            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            if (temp > startPos + lenght) {
                startPos += lenght;
            } else if (temp < startPos - lenght) {
                startPos -= lenght;
            }
        }
    }
}
