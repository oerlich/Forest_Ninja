using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    [SerializeField] int damage = 25;
    [SerializeField] GameObject _hitPrefab;
    [SerializeField] Transform effectPoint;
    [SerializeField] AudioSource ImpactSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (other.gameObject.GetComponent<PlayerCombat>() != null)
            {
                other.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
                ImpactSound.Play();
                Instantiate(_hitPrefab, effectPoint.position, effectPoint.rotation);
            }
        }
    }
}
