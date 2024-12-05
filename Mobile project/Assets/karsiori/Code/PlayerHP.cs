using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int health = 1;
    private CheckpointManager checkpointManager;

    private void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (checkpointManager != null)
        {
            checkpointManager.RespawnPlayer();
            health = 1; // Reset health upon respawn
        }
    }
}