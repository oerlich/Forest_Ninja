using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHP = 100;
    [SerializeField] Animator animator;
    [SerializeField] float disappearDelay = 1.5f;
    [SerializeField] GameObject _despawnPrefab;
    [SerializeField] AudioSource disappearSound;
    [SerializeField] Transform _effectPoint;
    [SerializeField] float attackRate = 1;
    [SerializeField] SkinnedMeshRenderer[] meshes;

    public Collider hurtBox;
    private int currentHP;

    private GameObject player;
    private NavMeshAgent nva;

    private float nextAttackTime;

    private float deathTime = 0;

    private GameObject deadEffect = null;


    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        nva = GetComponent<NavMeshAgent>();
        nva.stoppingDistance = 1.0f;

    }

    void Update()
    {
        if (deathTime != 0)
        {
            if (Time.time >= deathTime + disappearDelay)
            {
                if(deadEffect == null)
                {
                    deadEffect = Instantiate(_despawnPrefab, _effectPoint.position, _effectPoint.rotation);
                    disappearSound.Play();
                    foreach(SkinnedMeshRenderer mesh in meshes)
                    {
                        mesh.enabled = false;
                    }
                }

                Destroy(this.gameObject, disappearSound.clip.length);
            };
        }
        else
        {
            if (!player.GetComponent<PlayerCombat>().isDead)
            {
                nva.SetDestination(player.transform.position);
                if (nva.remainingDistance > nva.stoppingDistance)
                    animator.SetBool("Walking", true);
                else
                {
                    animator.SetBool("Walking", false);
                    if (Time.time >= nextAttackTime)
                    {
                        Attack();
                        nextAttackTime = Time.time + 1f / attackRate;
                    }
                }
            }
        }

        
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        animator.SetTrigger("Damaged");

        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        animator.SetBool("Die", true);

        deathTime = Time.time;

        this.GetComponent<Collider>().enabled = false;
    }
}
