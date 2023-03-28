using System.Collections;
using Cards;
using UnityEngine;

namespace GameLogic
{
    public class Player : CardUser
    {
        public int baseHealth = 30;
        public static int CurrentHealth = 30;
        public int usedHealthAsMana;
        private ReferencePig _referencePig;
        public AudioSource audioSource;
        private Color _redColor = new(152f/255f, 26f/255f, 28f/255f);
        private Color _greenColor = new(66f/255f, 131f/255f, 40f/255f);

        private void Start()
        {
            _referencePig = GameObject.FindWithTag("Overseer").GetComponent<ReferencePig>();
            UpdateHealthUi();
        }
        
        public void ResetHealth()
        {
            CurrentHealth = baseHealth;
            usedHealthAsMana = 0;
            UpdateHealthUi();
        }
        
        public void Heal(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > baseHealth)
            {
                CurrentHealth = baseHealth;
            }
            UpdateHealthUi();
            StartCoroutine(ShowDamageHealingText(amount, false));
        }

        public void TakeDamage(int amount, bool isFromOpponent)
        {
            audioSource.Play();
            if (!isFromOpponent)
            {
                usedHealthAsMana += amount;
            }
            
            CurrentHealth -= amount;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
            
            UpdateHealthUi();
            UpdateBankedHealthUi();
            if (isFromOpponent)
            {
                StartCoroutine(ShowOpponentDamageText(amount));
            }
            else
            {
                StartCoroutine(ShowDamageHealingText(amount, true));
            }
        }

        private void UpdateHealthUi()
        {
            _referencePig.playerHealthText.text = CurrentHealth.ToString();
        }

        private void UpdateBankedHealthUi()
        {
            _referencePig.bankedHealthText.text = $"({usedHealthAsMana})";
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }

        public IEnumerator ShowDamageHealingText(int amount, bool isDamage)
        {
            string text = "Total Cost: ";
            text += isDamage ? "-" : "+";
            text += amount;
            _referencePig.damageTakenText.text = text;

            _referencePig.damageTakenText.color = isDamage ? _redColor : _greenColor;
            
            yield return new WaitForSeconds(2);
            _referencePig.damageTakenText.text = "";
        }
        
        public IEnumerator ShowOpponentDamageText(int amount)
        {
            string text = "Villager Damage: -";
            text += amount;
            _referencePig.opponentDamageText.text = text;
            _referencePig.opponentDamageText.color = _redColor;
            yield return new WaitForSeconds(2);
            _referencePig.opponentDamageText.text = "";
        } 
    }
}
