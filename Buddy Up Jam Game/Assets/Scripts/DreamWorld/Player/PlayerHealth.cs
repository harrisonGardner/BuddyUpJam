using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;

    public Vector3 lastCheckpoint;

    public float invincibilityTime = 0.25f;
    private float invincibilityTimer = 0f;

    private void Start()
    {
        lastCheckpoint = transform.position;
    }

    public float Health 
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            HealthChangeChecks();
        }
    }

    public void Update()
    {
        if (invincibilityTimer > 0)
            invincibilityTimer -= Time.deltaTime;

        if (gameObject.transform.position.y < -50)
        {
            Die();
        }
    }

    public void Damage(float damageAmount)
    {
        invincibilityTimer = invincibilityTime;
        health -= damageAmount;
        HealthChangeChecks();
    }

    public bool IsInvincible()
    {
        return invincibilityTimer > 0;
    }

    //When the amount of health is changed check for things like death
    private void HealthChangeChecks()
    {
        if (health <= 0)
        {
            Die();
        }
        else if (health > 100f)
        {
            health = 100f;
        }
    }

    private void Die()
    {
        transform.position = lastCheckpoint;
        health = 100f;

        MusicSwap.Instance.Slow();
        SceneFade.Instance.DeathMessage("You died in a dream? I usually wake up when that happens");

        gameObject.SetActive(false);
    }
}
