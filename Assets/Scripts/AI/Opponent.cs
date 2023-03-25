using System.Collections.Generic;
using Cards;
using GameLogic;
using UnityEngine;

namespace AI
{
    public class Opponent : CardUser
    {
        public Transform intentionLocation;
        public int numberOfWounds = 0;
        [Range(1, 5)]
        public int maxNumberOfWounds = 3;
        public List<Card> cardsForDeckComposition;
        
        public void Awake()
        {
            foreach (Card card in cardsForDeckComposition)
            {
                deckManager.AddToDeckComposition(card);
            }
        }

        public void Wound()
        {
            numberOfWounds++;
        }
        
        public bool IsDead()
        {
            return numberOfWounds >= maxNumberOfWounds;
        }
    }
}