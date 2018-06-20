using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

    public int lives;

    PlayerUIManager playerUi;

    void Awake()
    {
        playerUi = GetComponent<PlayerUIManager>();
        playerUi.UpdateLives(lives);
    }

    public override void Damage(int value, Vector2 direction, float force)
    {
        if (invincibleCounter <= 0)
        {
            health -= value;
            invincibleCounter = invincibleTime;

            if (health <= 0)
                Die();
            else
                gameObject.GetComponent<PlayerMovement>().Knockback(direction, force);
        }
    }

    public override void Die()
    {
        gameObject.SetActive(false);

        health = maxHealth;

        lives--;

        playerUi.UpdateLives(lives);

        gameObject.GetComponent<PlayerMovement>().Invoke("Respawn", 2);

    }

    public void ArmorUp(int value)
    {
        health += value;
    }
}
