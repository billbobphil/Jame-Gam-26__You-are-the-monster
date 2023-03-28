using Cards;
using UnityEngine;

namespace GameLogic
{
    public class Player : CardUser
    {
        public int baseHealth = 30;
        public int currentHealth;
        public int usedHealthAsMana;
        private ReferencePig _referencePig;
        public AudioSource audioSource;

        private void Awake()
        {
            currentHealth = baseHealth;
        }

        private void Start()
        {
            _referencePig = GameObject.FindWithTag("Overseer").GetComponent<ReferencePig>();
            UpdateHealthUi();
        }
        
        public void ResetHealth()
        {
            currentHealth = baseHealth;
            usedHealthAsMana = 0;
            UpdateHealthUi();
        }
        
        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > baseHealth)
            {
                currentHealth = baseHealth;
            }
            UpdateHealthUi();
        }

        public void TakeDamage(int amount, bool isFromOpponent)
        {
            audioSource.Play();
            if (!isFromOpponent)
            {
                usedHealthAsMana += amount;
            }
            
            currentHealth -= amount;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            
            UpdateHealthUi();
        }

        private void UpdateHealthUi()
        {
            _referencePig.playerHealthText.text = currentHealth.ToString();
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}
