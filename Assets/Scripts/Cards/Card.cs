using System;
using UnityEngine;

namespace Cards
{
    public abstract class Card : MonoBehaviour
    {
        public Guid CardId;
        public int baseCost;
        public string cardName;
        public DiscardPile discardPile;
        public bool isDiscarded = false;

        public virtual void Play()
        {
            //TODO: this should subtract base cost from health
            Discard();
        }

        private void Discard()
        {
            isDiscarded = true;
            discardPile.DiscardCard(this);
        }
    }
}
