using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOne {
    public class Spitter : MonoBehaviour
    {

        private Vector2 startPoint;
        public Vector2 endPoint;
        [SerializeField] private GameObject dieEffect;
        private Rigidbody2D spitterRB;
        private Animator spitterAnim;

        [SerializeField] private float velocity;
        [SerializeField] private int health;
        private bool comingUp;
        private bool comingOut;
        private float timer;
        private bool pause;
        // Start is called before the first frame update

        private void Awake() {
            Vector2 start = transform.position;
            startPoint = start;
        }
        void Start()
        {
            spitterRB = GetComponent<Rigidbody2D>();
            spitterAnim = GetComponent<Animator>();
            comingUp = true;
            velocity *= -1;
            timer = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (pause) { return; }

            timer += Time.deltaTime;

            if (timer >= 20f) 
            {
                    timer = 0f;
                    comingUp = false;
                    comingOut = true;
            }
            isReachedEndStartPoint();
            Walk();
        }

        void Walk() 
        {
            if (comingUp || comingOut) {
                Vector2 vel = new Vector2(1 * velocity, spitterRB.velocity.y);
                spitterRB.velocity = vel;
            }
            spitterAnim.SetBool("Walking", comingUp || comingOut);
        }

        bool isReachedEndStartPoint() 
        {
            float xDestinyPoint = comingUp ? endPoint.x : startPoint.x;
            bool reached = false;
            if (transform.rotation.y == 0) 
            {
                reached = transform.position.x <= xDestinyPoint;
            }
            else 
            {
                reached = transform.position.x >= xDestinyPoint;
            }
            if (reached) 
            {
                comingUp = !comingUp;
                comingOut = !comingOut;
                velocity *= -1;
            }
            return reached;
        }

        public void TakeDamage(int damage) 
        {   
            health -= damage;
            if (health <= 0) 
            {
                Die();                
            }
        }

        void Die() 
        {            
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
