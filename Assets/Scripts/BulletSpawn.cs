using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace GameOne {
    public class BulletSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Camera cam;
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private float bulletForce;
        [SerializeField] private AudioClip shootSound;
        Vector2 mousePosition;

        private void Update()
        {
            mousePosition = cam.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition);
        }

        private void FixedUpdate()
        {
            Vector2 lookDir = mousePosition - playerRb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void Fire()
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
            AudioSource.PlayClipAtPoint(shootSound, cam.transform.position);
        }
    }
}