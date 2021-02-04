using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Combat code created following Brackeys Melee combat tutorial but adapted to 3D, some additional improvements were made for hit detection
    // https://www.youtube.com/watch?v=sPiVz1k-fEs

    [SerializeField] Animator animator;
    [SerializeField] float attackRate = 2;
    [SerializeField] int maxHealth = 200;
    [SerializeField] AudioSource SwingSound;
    [SerializeField] HealthBar healthBar;

    public Collider swordCollider;
    public LayerMask enemyLayers;
    public bool _isAttacking = false;
    public bool isDead = false;

    public GameOverScreen gms;

    private int currentHealth;

    private float nextAttackTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            _isAttacking = false;
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                _isAttacking = true;
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        SwingSound.Play();

        swordCollider.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        animator.SetTrigger("Damaged");

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        animator.SetBool("Die", true);
        isDead = true;

        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;

    }

}
