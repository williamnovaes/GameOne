using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;


namespace GameOne {
    public class player : MonoBehaviour
    {
        [SerializeField] private LayerMask whatsIsGround;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject[] bulletSpawns;
        private Rigidbody2D playerRB;
        private BoxCollider2D wallCheck;
        private BoxCollider2D groundCheck;
        [SerializeField] private int life;
        [SerializeField] private float velocity = 1f;
        [SerializeField] private float jumpForce = 1f;
        private Vector2 movement;
        private Vector2 mousePosition;
        private bool jumpOffCoroutineRunning;

        private bool isAlive;
        private bool facingRight;
        [SerializeField] private bool touchingWall;
        [SerializeField] private bool grounded;

        [SerializeField] private bool isMelee;
        private float nextAttackTime = 0f;
        private float attackRate = 2f;
        private float cadence = 10f;
        [SerializeField] private Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            playerRB = GetComponent<Rigidbody2D>();
            groundCheck = GameObject.FindWithTag("GroundCheck").GetComponent<BoxCollider2D>();
            wallCheck = GameObject.FindWithTag("WallCheck").GetComponent<BoxCollider2D>();
            isAlive = true;
            life = 100;
            facingRight = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isAlive) { return; }

            movement.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            movement.y = CrossPlatformInputManager.GetAxisRaw("Vertical");

            AnimationsController();
            CheckGrounded();
            Jump();
            Flip();
            PassThrough();

            if (CrossPlatformInputManager.GetButton("Fire1"))
            {
                animator.SetBool("Shooting", true);
                Atack();
            }
            else {
                animator.SetBool("Shooting", false);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                ChangeAttackLayer();

            }
        }

        private void FixedUpdate()
        {
            if (!isAlive) { return; }

            if (!touchingWall)
            {
                Walk();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            //QUANDO COLIDE COM COLLIDER
            if (collision.gameObject.tag.Equals("Wall"))
            {
                if ((collision.transform.position.x < transform.position.x && movement.x < 0f)
                    || (collision.transform.position.x > transform.position.x && movement.x > 0f))
                {
                    Debug.Log("Colidiu com a parede");
                    touchingWall = true;
                } else
                {
                    touchingWall = false;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Wall"))
            {
                //AO SAIR DE COLISAO COM COLLIDER
                Debug.Log("Saiu da colisao com a parede");
                touchingWall = false;
            }
        }

        void Walk()
        {
            /*
             * script para movimentar em todas direcoes, nao funciona para plataforma
            Vector2 movement = new Vector2(horizontal, 0f);
            playerRB.MovePosition(playerRB.position + movement * velocity * Time.fixedDeltaTime);
            */
            if (touchingWall) { return; }

            Vector2 playerVelocity = new Vector2(movement.x * velocity, playerRB.velocity.y);
            playerRB.velocity = playerVelocity;
        }

        void Jump()
        {
            if (!grounded) { return; }

            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                playerRB.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }

        void Atack()
        {
            if (Time.time >= nextAttackTime)
            {
                if (isMelee)
                {
                    MeleeAtack();
                    nextAttackTime = Time.time + (1f / attackRate);
                }
                else
                {
                    RangedAttack();
                    nextAttackTime = Time.time + 1f / cadence;
                }
            }
        }

        void RangedAttack()
        {
            foreach (GameObject bulletSpawn in bulletSpawns)
            {
                if (bulletSpawn.activeSelf)
                {
                    bulletSpawn.GetComponent<BulletSpawn>().Fire();
                }
            }
        }

        void MeleeAtack()
        {

        }

        void ChangeAttackLayer()
        {
            isMelee = !isMelee;
            animator.SetLayerWeight(0, isMelee ? 1 : 0);
            animator.SetLayerWeight(1, isMelee ? 0 : 1);
        }

        public void TakeDamage(int damage)
        {
            if (isAlive) {
                life -= damage;
                
                if (life <= 0)
                {
                    isAlive = false;
                }
            }
        }

        private void PassThrough()
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                StartCoroutine(JumpOff());
            }
        }

        IEnumerator JumpOff()
        {
            jumpOffCoroutineRunning = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PassThrough"), true);
            yield return new WaitForSeconds(0.3f);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PassThrough"), false);
            jumpOffCoroutineRunning = false;
        }

        bool Walking()
        {
            return Mathf.Abs(movement.x) > Mathf.Epsilon;
            //return Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        }

        private void CheckGrounded()
        {
            grounded = groundCheck.IsTouchingLayers(whatsIsGround);
        }

        void Flip()
        {
            mousePosition = cam.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition);
            if (facingRight && transform.position.x > mousePosition.x || !facingRight && transform.position.x < mousePosition.x)
            {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
                /**
                Vector3 localScale = transform.localScale;
                localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
                transform.localScale = localScale;
            */
            }
            /**
            if (Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon)
            {
                transform.localScale = new Vector2(Mathf.Sign(playerRB.velocity.x), transform.localScale.y);
            }
            */
        }

        void AnimationsController()
        {
            bool lookingUp = false;
            bool lookingDown = false;
            if (movement.y > Mathf.Epsilon)
            {
                lookingUp = true;
                lookingDown = false;
            }
            else if (movement.y < 0.0f)
            {
                lookingUp = false;
                lookingDown = true;
            }
            animator.SetBool("Walking", Walking());
            animator.SetBool("LookingUp", lookingUp);
            animator.SetBool("LookingDown", lookingDown);
            animator.SetBool("Jumping", !grounded);
        }
    }
}