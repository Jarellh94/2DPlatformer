using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {

    public override void Damage(int value, Vector2 direction, float force)
    {
        health -= value;

        if (health <= 0)
            Die();
        else
            gameObject.GetComponent<Enemy>().Knockback(direction, force);
    }

    public override void Die()
    {
        /*
        gameObject.GetComponent<Enemy>().Respawn();

        health = maxHealth;*/

        Destroy(gameObject);
    }
}
