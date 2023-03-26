using System;
using UnityEngine;

namespace Cards
{
    public abstract class Card : MonoBehaviour
    {
        public Guid CardId;
        public string cardName;
        public DiscardPile discardPile;
        public bool isDiscarded = false;

        public virtual void Play()
        {
            Discard();
        }

        private void Discard()
        {
            isDiscarded = true;
            discardPile.DiscardCard(this);
        }
    }
}
