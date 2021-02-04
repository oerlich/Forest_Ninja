using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    [SerializeField] int damage = 50;
    [SerializeField] GameObject _hitPrefab;
    [SerializeField] Transform effectPoint;
    [SerializeField] AudioSource ImpactSound;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if(other.gameObject.GetComponent<Enemy>() != null)
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                ImpactSound.Play();
                Instantiate(_hitPrefab, effectPoint.position, effectPoint.rotation);
            }
        }
    }
}
