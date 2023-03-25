﻿using System.Collections.Generic;
using Exceptions;
using UnityEngine;

namespace Cards
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField]
        private List<Card> deckComposition;
        [SerializeField]
        private Deck workingDeck;
        public Transform deckLocation;
        public DiscardPile discardPile;

        public Card DrawFromDeck()
        {
            return workingDeck.DrawFromDeck();
        }

        public void ShuffleDeck()
        {
            workingDeck.ShuffleDeck();
        }

        public void InitializeDeck()
        {
            ResetDeck();
        }

        public void ResetDeck()
        {
            foreach (Card card in deckComposition)
            {
                Card createdCard = Instantiate(card, deckLocation.position, Quaternion.identity);
                createdCard.discardPile = discardPile;
                workingDeck.Cards.Push(createdCard);
            }
            
            ShuffleDeck();
        }
        
        public void AddToWorkingDeck(Card card)
        {
            workingDeck.AddCard(card);
            ShuffleDeck();
        }
        
        public void RemoveFromWorkingDeck(Card card)
        {
            workingDeck.RemoveCard(card);
            ShuffleDeck();
        }

        public void AddToDeckComposition(Card card)
        {
            deckComposition.Add(card);
        }
        
        public void RemoveFromDeckComposition(Card card)
        {
            deckComposition.Remove(card);
        }
    }
}