using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class DiscardManager : MonoBehaviour
    {
        public List<Card> discardPile = new();
        
        public void DiscardCard(Card card)
        {
            discardPile.Add(card);
        }
    }
}