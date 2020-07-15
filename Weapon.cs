using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float _damage = 50.0f;
    public float Damage => _damage;
    private ParticleSystem _particleSystem = null;   
    private int layerMask = 0;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _particleSystem.Stop();
        }
    }

    public virtual IEnumerator Shoot()
    {
        _particleSystem.Play();      

        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            }

            yield return new WaitForSeconds(fireRate);
        }
        
        
    }

}
