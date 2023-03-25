using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class DiscardPile : MonoBehaviour
    {
        public List<Card> cards = new();
        public Transform discardPileLocation;
        
        public void DiscardCard(Card card)
        {
            cards.Add(card);
            card.transform.position = discardPileLocation.position;
        }
    }
}