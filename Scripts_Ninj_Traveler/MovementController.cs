using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public RuntimeAnimatorController controller1;

    public RuntimeAnimatorController controller2;

    public RuntimeAnimatorController controller3;

    public AudioSource audioSource;

    public AudioClip audioAttack1;

    public AudioClip audioAttack2;

    public AudioClip audioAttack3;

    public AudioClip audioAttack10;

    public Transform attackPoint;

    public float attackRange;

    public LayerMask enemyLayers;

    public float playerAttack;

    public float runSpeed;

    public CharacterController2D controller;

    float horizontalMove = 0f;

    private bool jump;

    public float hp;

    public Animator animator;

    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in colliders2D)
        {
            //Debug.Log("12" + enemy.name);
            enemy.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position - transform.position).normalized * 3f, ForceMode2D.Impulse);

            GroundedEnemyController enemyController;
            if (enemy.TryGetComponent(out enemyController))
            {
                enemyController.takeDamage(playerAttack);
                audioSource.PlayOneShot(audioAttack10);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.runtimeAnimatorController = controller1;
            playerAttack = 25f;
            runSpeed = 0.9f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.runtimeAnimatorController = controller2;
            playerAttack = 100f;
            runSpeed = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.runtimeAnimatorController = controller3;
            runSpeed = 0.8f;
            playerAttack = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetTrigger("shoot1");
            audioSource.PlayOneShot(audioAttack1);
            Attack();
        }

        if (Input.GetButtonDown("Jump")){
            jump = true;
        }

        if(controller.getGrounded()){
            animator.SetBool("jump1", false);
        }else if(!controller.getGrounded()){
            animator.SetBool("jump1", true);
        }

        if (hp <= 0)
        {
            animator.SetBool("end1", true);
            animator.SetBool("jump1", false);
            runSpeed = 0f;
            rb.velocity = Vector2.zero;
            //Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("ctrl1", true);
        }
        else
        {
            animator.SetBool("ctrl1", false);
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (animator != null)
        {
            animator.SetFloat("run1", Mathf.Abs(horizontalMove));
        }
    }

    public void OnJumpButtonDown()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (controller.getGrounded())
        {
            animator.SetBool("jump1", false);
        }
        else if (!controller.getGrounded())
        {
            animator.SetBool("jump1", true);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void takeDamage(float attack)
    {
        hp = hp - attack;
    }

    void FixedUpdate(){
        controller.Move(horizontalMove, false, jump);
        jump = false;
    }
}