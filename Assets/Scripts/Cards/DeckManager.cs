using System.Collections.Generic;
using Exceptions;
using UnityEngine;

namespace Cards
{
    public class DeckManager : MonoBehaviour
    {
        private List<Card> _deck = new();
        private Stack<Card> _workingDeck = new();

        public Card DrawFromDeck()
        {
            if (_workingDeck.Count == 0)
            {
                throw new EmptyDeckException();
            }
            
            return _workingDeck.Pop();
        }

        public void ShuffleDeck()
        {
            Stack<Card> tempDeck = new();
            List<Card> currentDeck = new(_workingDeck);
            
            while (currentDeck.Count > 0)
            {
                int randomIndex = Random.Range(0, currentDeck.Count);
                tempDeck.Push(currentDeck[randomIndex]);
                currentDeck.RemoveAt(randomIndex);
            }

            _workingDeck = tempDeck;
        }

        public void ResetDeck()
        {
            _workingDeck = new Stack<Card>(_deck);
            ShuffleDeck();
        }
        
        public void AddToWorkingDeck(Card card)
        {
            _workingDeck.Push(card);
            ShuffleDeck();
        }
        
        public void RemoveFromWorkingDeck(Card card)
        {
            List<Card> currentDeck = new(_workingDeck);
            currentDeck.Remove(card);
            _workingDeck = new Stack<Card>(currentDeck);
            ShuffleDeck();
        }

        public void AddToDeckPermanently(Card card)
        {
            _deck.Add(card);
        }
        
        public void RemoveFromDeckPermanently(Card card)
        {
            _deck.Remove(card);
        }
    }
}