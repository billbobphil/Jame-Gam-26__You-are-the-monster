using System.Collections.Generic;
using Exceptions;
using UnityEngine;

namespace Cards
{
    public class Deck : MonoBehaviour
    {
        public Stack<Card> Cards = new();
        
        public Card DrawFromDeck()
        {
            if (Cards.Count == 0)
            {
                throw new EmptyDeckException();
            }
            
            return Cards.Pop();
        } 
        
        public void ShuffleDeck()
        {
            Stack<Card> tempDeck = new();
            List<Card> currentDeck = new(Cards);
            
            while (currentDeck.Count > 0)
            {
                int randomIndex = Random.Range(0, currentDeck.Count);
                tempDeck.Push(currentDeck[randomIndex]);
                currentDeck.RemoveAt(randomIndex);
            }

            Cards = tempDeck;
        }
        
        public void AddCard(Card card)
        {
            Cards.Push(card);
        }
        
        public void RemoveCard(Card card)
        {
            List<Card> currentDeck = new(Cards);
            currentDeck.Remove(card);
            Cards = new Stack<Card>(currentDeck);
        }
    }
}