using Cards;
using UnityEngine;

namespace GameLogic
{
    public class GameInitializer : MonoBehaviour
    {
        public ReferencePig referencePig;

        private void Awake()
        {
            //TODO: fill these with non-abstract cards
            // referencePig.player.DeckManager.AddToDeckPermanently();
            // referencePig.player.DeckManager.AddToDeckPermanently();
            // referencePig.player.DeckManager.AddToDeckPermanently();
            // referencePig.player.DeckManager.AddToDeckPermanently();
            // referencePig.player.DeckManager.AddToDeckPermanently();
        }

        private void Start()
        {
            referencePig.matchManager.RunMatch();
        }
    }
}
