using System;
using UnityEngine;

namespace Cards
{
    public abstract class Card : MonoBehaviour
    {
        public Guid CardId;
        public int baseCost;
        public string cardName;
        public DiscardManager discardManager;

        public void Play()
        {
            //TODO: this should subtract base cost from health
            Discard();
        }

        private void Discard()
        {
            discardManager.DiscardCard(this);
        }
    }
}
