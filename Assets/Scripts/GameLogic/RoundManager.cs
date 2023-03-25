using System;
using System.Collections.Generic;
using AI;
using Cards;
using UnityEngine;

namespace GameLogic
{
    public class RoundManager : MonoBehaviour
    {
        public delegate void EndRound();
        public static event EndRound OnRoundEnd;

        public ReferencePig referencePig;
        private Opponent aiOpponent;
        private Player player;
        public List<Card> playerSelectedCards;
        public List<Card> aiSelectedCards;

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
            player = null;
            aiOpponent = null;
        }
        public void RunRound(Opponent opponent)
        {
            aiOpponent = opponent;
            player = referencePig.player;
            Debug.Log("Running round");
            
            //DONE: player and Ai both draw
            PlayerDraw();
            AiDraw();
                //possible condition for empty decks
                
            //DONE: Generate AI intention and show to player
            GenerateAiIntention();

            //DONE: Player chooses cards to play - this will need to run in an update loop
                //DONE: limit one monster type card
                //DONE: unlimited non-monster type cards
                
            //DONE: (need button still)Player 'submits' turn - will be triggered by button or something?
                //DONE: Player cards must be removed from hand and added to discard
                //DONE: AI cards must be removed from hand and added to discard
                //Forces battle
                        //1/2 DONE: (need to calculate AI) Check power conditions
                            //if player loses, player takes damage equal to total power of AI
                            //if AI loses, AI takes a 'wound' of which they can take 3
        }

        private void PlayerDraw()
        {
            player.handManager.DrawCard();
        }

        private void AiDraw()
        {   
            aiOpponent.handManager.DrawCard();
        }
        
        private void GenerateAiIntention()
        {
            //generate a number between 0 and the number of cards in the AI's hand
            int randomIndex = UnityEngine.Random.Range(0, aiOpponent.handManager.hand.Count);
            
            //get the card at that index
            Card card = aiOpponent.handManager.hand[randomIndex];
            
            //add the card to the list of cards the AI has selected
            aiSelectedCards.Add(card);
            card.transform.position = aiOpponent.intentionLocation.position;
        }

        public void OnPlayerSubmitTurn()
        {
            Debug.Log("Player submitted turn");
            //TODO: Something to prevent player from select cards during this time
            int numberOfMonsterCards = 0;

            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (PlayerCard card in playerSelectedCards)
            {
                if (card is MonsterCard)
                {
                    numberOfMonsterCards++;
                }
            }

            if (numberOfMonsterCards > 1)
            {
                //TODO: prevent player from submitting turn and force them to retract cards
                Debug.Log("Cannot choose more than 1 monster card");
            }
            else
            {
                //Calculate player power
                MonsterCard monsterCard = null;
                List<ModifierCard> modifierCards = new();

                foreach (Card card in playerSelectedCards)
                {
                    if (card is MonsterCard)
                    {
                        monsterCard = (MonsterCard)card;
                    }
                    else
                    {
                        modifierCards.Add((ModifierCard)card);
                    }
                }

                int calculatedPower = monsterCard!.basePower;

                foreach (ModifierCard card in modifierCards)
                {
                    if (card.modifierType == monsterCard.monsterType)
                    {
                        calculatedPower += card.modifierAmount;
                    }
                }
                
                foreach(Card card in playerSelectedCards)
                {
                    player.handManager.PlayCard(card);
                }
                
                foreach(Card card in aiSelectedCards)
                {
                    aiOpponent.handManager.PlayCard(card);
                }
                
                Debug.Log($"Player power is: {calculatedPower}");
                Debug.Log($"AI power is: N/A");

                playerSelectedCards = new List<Card>();
                aiSelectedCards = new List<Card>();
                
                RunBattle(calculatedPower, 0);
            }
        }

        public void HandlePlayerCardSubmitted(PlayerCard card)
        {
            Debug.Log("Player submitted card");
            playerSelectedCards.Add(card);
        }
        
        public void HandlePlayerCardRetracted(PlayerCard card)
        {
            Debug.Log("Player retracted card");
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
                aiOpponent.Wound();
            }
            else
            {
                Debug.Log("AI wins round");
                player.TakeDamage(aiPower);
            }
        }
    }
}
