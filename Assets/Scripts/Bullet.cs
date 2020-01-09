using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOne
{
    public class Bullet : MonoBehaviour
    {

        [SerializeField] private float velocity;
        [SerializeField] private int damage;
        [SerializeField] private float lifeTime;
        private float currentTime;
        [SerializeField] private GameObject hitEffect;

        [SerializeField] private AudioClip bulletDestroy;

        void Start()
        {
            lifeTime = 4f;
            currentTime = 0f;
        }

        void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= lifeTime)
            {
                //Destroy();
            }
        }

        private void OnBecameInvisible()
        {
            Destroy();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                //StartCoroutine(collision.GetComponent<Enemy>().TakeDamage(damage));
                collision.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (!collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, .375f);
                AudioSource.PlayClipAtPoint(bulletDestroy, Camera.main.transform.position);
                Destroy();
            }

        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}