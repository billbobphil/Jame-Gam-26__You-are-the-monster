using UnityEngine;

namespace Cards
{
    public abstract class CardUser : MonoBehaviour
    {
        public DeckManager deckManager;
        public DiscardPile discardPile;
        public HandManager handManager;
    }
}