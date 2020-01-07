using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameOne {
    public class Enemy : MonoBehaviour
    {
        private Animator animator;
        [SerializeField] private Transform player;
        [SerializeField] private int followVelocity;
        public int currentHealth;
        [SerializeField] private bool pause;
        [SerializeField] private GameObject dieEffect;

        public float time;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            animator = GetComponent<Animator>();
            currentHealth = 60;
            time = 0f;
            followVelocity = 10;
        }

        void Update()
        {
            if (pause) { return; }
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * 3 / followVelocity);
            

            //time += Time.deltaTime;
            //if (time >= 2f)
            //{
            //    StartCoroutine(TakeDamage(20));
            //    time = 0f;
            //}
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth > 0)
            {
                animator.SetTrigger("Damage");
            } else
            {
                Die();
            }
        }

        private void Die()
        {
            //animator.SetTrigger("Die");
            //yield return new WaitForSeconds(.75f);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}