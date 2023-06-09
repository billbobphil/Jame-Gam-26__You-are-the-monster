using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using Cards;
using Exceptions;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class RoundManager : MonoBehaviour
    {
        public delegate void EndRound();
        public static event EndRound OnRoundEnd;

        public ReferencePig referencePig;
        private Opponent _aiOpponent;
        private Player _player;
        public List<Card> playerSelectedCards;
        public List<Card> aiSelectedCards;
        public bool isPlayerSelectingCards;

        private void OnEnable()
        {
            PlayerCard.OnPlayerCardSubmitted += HandlePlayerCardSubmitted;
            PlayerCard.OnPlayerCardRetracted += HandlePlayerCardRetracted;
        }
        
        private void OnDisable()
        {
            PlayerCard.OnPlayerCardSubmitted -= HandlePlayerCardSubmitted;
            PlayerCard.OnPlayerCardRetracted -= HandlePlayerCardRetracted;
        }
        
        public void ResetRound()
        {
            _player = null;
            _aiOpponent = null;
        }
        public void RunRound(Opponent opponent)
        {
            _aiOpponent = opponent;
            _player = referencePig.player;
            isPlayerSelectingCards = true;
            Debug.Log("Running round");

            bool shouldRoundContinue = HandleDrawPhase();
            
            if (shouldRoundContinue)
            {
                GenerateAiIntention();
            }
        }

        private bool HandleDrawPhase()
        {
            bool playerDeckEmpty = false;
            bool aiDeckEmpty = false;
            try
            {
                PlayerDraw();
            }
            catch (EmptyDeckException)
            {
                playerDeckEmpty = true;
            }

            try
            {
                AiDraw();
            }
            catch (EmptyDeckException)
            {
                aiDeckEmpty = true;
            }

           

            if (playerDeckEmpty && aiDeckEmpty)
            {
                if (_player.handManager.hand.Count == 0 && _aiOpponent.handManager.hand.Count == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        _aiOpponent.Wound();
                    }
                    OnRoundEnd?.Invoke();
                    return false;
                }
                
                if (_player.handManager.hand.Count == 0)
                {
                    _player.TakeDamage(100, false);
                    OnRoundEnd?.Invoke();
                    return false;
                }
                
                if (_aiOpponent.handManager.hand.Count == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        _aiOpponent.Wound();
                    }
                    OnRoundEnd?.Invoke();
                    return false;
                }
            }
            else if (playerDeckEmpty && _player.handManager.hand.Count == 0)
            {
                _player.TakeDamage(100, false);
                OnRoundEnd?.Invoke();
                return false;
            }
            else if (aiDeckEmpty && _aiOpponent.handManager.hand.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    _aiOpponent.Wound();
                }
                OnRoundEnd?.Invoke();
                return false;
            }

            return true;
        }

        private void PlayerDraw()
        {
            _player.handManager.DrawCard();
        }

        private void AiDraw()
        {   
            _aiOpponent.handManager.DrawCard();
        }
        
        private void GenerateAiIntention()
        {
            //generate a number between 0 and the number of cards in the AI's hand
            int randomIndex = UnityEngine.Random.Range(0, _aiOpponent.handManager.hand.Count);
            
            //get the card at that index
            Card card = _aiOpponent.handManager.hand[randomIndex];
            
            //add the card to the list of cards the AI has selected
            aiSelectedCards.Add(card);
            card.transform.position = _aiOpponent.intentionLocation.position;
        }

        public void OnPlayerSubmitTurn()
        {
            isPlayerSelectingCards = false;
            Debug.Log("Player submitted turn");
            int numberOfMonsterCards = 0;

            if (playerSelectedCards.Count == 0)
            {
                StartCoroutine(ShowPlayerErrorMessage("You must select at least 1 card."));
                return;
            }

            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (PlayerCard card in playerSelectedCards)
            {
                if (card is MonsterCard)
                {
                    numberOfMonsterCards++;
                }
            }

            if (IsPlayerSubmissionValid())
            {
                int playerCost = CalculatePlayerCost();
                _player.TakeDamage(playerCost, false);
                
                int playerPower = CalculatePlayerPower();
                int aiPower = CalculateAiPower(GetPlayerMonsterCard());
                
                foreach(Card card in playerSelectedCards)
                {
                    _player.handManager.PlayCard(card);
                }
                
                foreach(Card card in aiSelectedCards)
                {
                    _aiOpponent.handManager.PlayCard(card);
                }
                
                Debug.Log($"Player power is: {playerPower}");
                Debug.Log($"AI power is: {aiPower}");

                playerSelectedCards = new List<Card>();
                aiSelectedCards = new List<Card>();
                
                RunBattle(playerPower, aiPower);
            }
            else
            {
                StartCoroutine(ShowPlayerErrorMessage("You can only choose 1 monster card."));
            }
        }

        private IEnumerator ShowPlayerErrorMessage(string message)
        {
            referencePig.errorText.GetComponent<TextMeshProUGUI>().text = message;
            referencePig.errorText.SetActive(true);
            yield return new WaitForSecondsRealtime(2);
            referencePig.errorText.SetActive(false);
        }

        private bool IsPlayerSubmissionValid()
        {
            int numberOfMonsterCards = 0;

            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (PlayerCard card in playerSelectedCards)
            {
                if (card is MonsterCard)
                {
                    numberOfMonsterCards++;
                }
            }
            
            return numberOfMonsterCards == 1;
        }

        private int CalculatePlayerCost()
        {
            int totalCost = 0;
            foreach(Card card in playerSelectedCards)
            {
                totalCost += ((PlayerCard)card).baseCost;
            }

            return totalCost;
        }
        
        private int CalculatePlayerPower()
        {
            MonsterCard monsterCard = GetPlayerMonsterCard();
            List<ModifierCard> modifierCards = GetPlayerModifierCards();

            int calculatedPower = monsterCard!.basePower;

            foreach (ModifierCard card in modifierCards)
            {
                if (card.modifierType == monsterCard.monsterType)
                {
                    calculatedPower += card.modifierAmount;
                }
            }

            return calculatedPower;
        }

        private int CalculateAiPower(MonsterCard playerMonsterCard)
        {
            HeroCard aiCard = aiSelectedCards[0] as HeroCard;
            int calculatedPower = aiCard!.basePower;

            if (aiCard != null && aiCard.strongAgainst == playerMonsterCard.monsterType)
            {
                calculatedPower *= aiCard.strongAgainstMultiplier;
            }

            return calculatedPower;
        }
        
        private MonsterCard GetPlayerMonsterCard()
        {
            MonsterCard monsterCard = null;
            foreach (Card card in playerSelectedCards)
            {
                if (card is MonsterCard)
                {
                    monsterCard = (MonsterCard)card;
                }
            }

            return monsterCard;
        }
        
        private List<ModifierCard> GetPlayerModifierCards()
        {
            List<ModifierCard> modifierCards = new();
            foreach (Card card in playerSelectedCards)
            {
                if (card is ModifierCard)
                {
                    modifierCards.Add((ModifierCard)card);
                }
            }

            return modifierCards;
        }
        
        private void HandlePlayerCardSubmitted(PlayerCard card)
        {
            playerSelectedCards.Add(card);
        }
        
        private void HandlePlayerCardRetracted(PlayerCard card)
        {
            playerSelectedCards.Remove(card);
        }

        private void RunBattle(int playerPower, int aiPower)
        {
            CalculateBattleResults(playerPower, aiPower);
            OnRoundEnd?.Invoke();
        }
        
        private void CalculateBattleResults(int playerPower, int aiPower)
        {
            if(playerPower > aiPower)
            {
                Debug.Log("Player wins round");
                _aiOpponent.Wound();
            }
            else
            {
                Debug.Log("AI wins round");
                _player.TakeDamage(aiPower, true);
            }
        }
    }
}
