using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float patrolSpeed = 2f;
    public float distanceToPlayer;
    public Transform playerTransform;
    private Rigidbody2D rb;
    public float knockbackForce = 10f; // Сила отталкивания
    public AudioSource audioSource;
    public AudioClip audioAttack11;

    public Transform groundCheck;
    private bool isGrounded;
    public LayerMask groundLayer;

    public float chaseSpeed = 3f;

    public float attackDistance = 10f;

    public Transform attackPoint; 
    
    public float attackRange = 2f; 
    public LayerMask  playerLayer; 
    private Vector2 originalScele;
    public float AttackEnemy;
    private bool hasPlayed = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        originalScele = playerTransform.localScale;
    }
    
    // Update is called once per frame  
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        distanceToPlayer = Vector2.Distance( transform.position, playerTransform.position);

        if(distanceToPlayer <= attackDistance){
            //transform.Rotate( 0,180f,0);
            ChasePlayer();
        }else if(distanceToPlayer > attackDistance){
            Patrol();
        }
        
        /*if(distanceToPlayer <= 2f){
            AttackPlayer();
            patrolSpeed = 0f;
        }
        if(distanceToPlayer > 2f)
        {
            patrolSpeed = 2f;
        }*/
    }

    public void Update() 
    {
        if (distanceToPlayer <= 2f)
        {
            AttackPlayer();
        }
        if (distanceToPlayer > 2f)
        {

        }
        if (health <= 0)
        {
            Destroy(gameObject);
            //Debug.Log("-hp");
        }
    }

    public void takeDamage(float damage){
        health = health - damage;
        //Debug.Log("-hp enemy");
    }

    void Patrol(){
        rb.velocity = new Vector2(Vector2.right.x * patrolSpeed, rb.velocity.y);
        rb.velocity = new Vector2(-Vector2.right.x * patrolSpeed, rb.velocity.y);
    }

    void ChasePlayer(){
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        if(direction.x > 0){
            transform.localScale  = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }else if(direction.x < 0){
            transform.localScale  = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    void AttackPlayer()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in colliders2D)
        {
            //Debug.Log("12" + enemy.name);
            player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * 3f, ForceMode2D.Impulse);

            GroundedEnemyController enemyController;
            if (player.TryGetComponent(out enemyController))
            {
                enemyController.takeDamage(AttackEnemy);
                AudioSource.PlayClipAtPoint(audioAttack11, transform.position);
                hasPlayed = true;
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
