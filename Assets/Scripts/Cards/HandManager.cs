using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class HandManager : MonoBehaviour
    {
        public DeckManager deckManager;
        public List<Card> hand = new();
        [Range(1,7)]
        public int maxHandSize = 4;
        
        public void DrawCard()
        {
            if (hand.Count < maxHandSize)
            {
                hand.Add(deckManager.DrawFromDeck());
            }
        }

        public void PlayCard(Card card)
        {
            hand.Remove(card);
            card.Play();
        }
        
    }
}