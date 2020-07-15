using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI = null;
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float armor = 0.0f;

    private void Start()
    {
        _gameUI.ChangeHPText(health);
    }

    public void TakeDamage(float damage)
    {
        float changeHP = damage - armor;
        if (changeHP > 0)
        {
            health -= changeHP;

            if (health <= 0)
            {
                health = 0;
                Dead();
            }

            _gameUI.ChangeHPText(health);
        }
    }

    private void Dead()
    {
        Messenger.Broadcast(GameEvent.PLAYER_DEAD);
    }
}
