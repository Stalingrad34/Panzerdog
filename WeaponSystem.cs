using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons = null;
    private Weapon currentWeapon = null;
    private Coroutine coroutineShoot = null;

    private void Start()
    {
        currentWeapon = _weapons[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            coroutineShoot = StartCoroutine(currentWeapon.Shoot());
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(coroutineShoot);
        }

    }   

}
