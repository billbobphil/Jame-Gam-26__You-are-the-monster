using System.Collections.Generic;
using Exceptions;
using GameLogic;
using UnityEngine;

namespace Cards
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField]
        private List<Card> deckComposition;
        [SerializeField]
        public Deck workingDeck;
        public Transform deckLocation;
        public DiscardPile discardPile;
        private ReferencePig _referencePig;
        
        private void Awake()
        {
            _referencePig = GameObject.FindWithTag("Overseer").GetComponent<ReferencePig>();
        }

        public Card DrawFromDeck()
        {
            Player player = GetComponentInParent<Player>();

            Card drawnCard = workingDeck.DrawFromDeck();
            
            if (player != null)
            {
                _referencePig.cardsRemainingText.text = workingDeck.Cards.Count.ToString();
            }

            return drawnCard;
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
            Player player = GetComponentInParent<Player>();
            if (player != null)
            {
                _referencePig.cardsRemainingText.text = workingDeck.Cards.Count.ToString();
            }
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