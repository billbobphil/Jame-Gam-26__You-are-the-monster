using Cards;
using UnityEngine;

namespace GameLogic
{
    public class Player : CardUser
    {
        public int baseHealth = 30;
        public int currentHealth;

        private void Awake()
        {
            currentHealth = baseHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}
