using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    GameManager gameManager;
    PlayerHealth playerHealth;
    public GameObject collectEffect;
    public AudioClip collectSound;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.AddHealth();
            gameManager.currentHealth++;
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Destroy(gameObject);
        }
    }
}
