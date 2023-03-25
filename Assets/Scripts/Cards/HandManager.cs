using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class HandManager : MonoBehaviour
    {
        
        public DeckManager deckManager;
        public List<Card> hand = new();
        public Transform handLocation;
        [Range(1,7)]
        public int maxHandSize = 5;
        [Range(0, 5)]
        public int startingHandSizeReductionFromMax = 3;
        private int StartingHandSize => maxHandSize - startingHandSizeReductionFromMax;

        public void DrawStartingHand()
        {
            for(int i = 0; i < StartingHandSize; i++)
            {
                DrawCard();
            }
        }
        
        public void DrawCard()
        {
            if (hand.Count < maxHandSize)
            {
                Card drawnCard = deckManager.DrawFromDeck();

                if (drawnCard is PlayerCard)
                {
                    ((PlayerCard)drawnCard).hand = this;
                }
                
                hand.Add(drawnCard);
                
                Vector3 handPosition = handLocation.position;
                
                for (int i = 0; i < hand.Count; i++)
                {
                    hand[i].transform.position = handPosition + new Vector3(-(i * 11), 0, 0);
                }
            }
        }

        public void PlayCard(Card card)
        {
            hand.Remove(card);
            card.Play();
        }
    }
}