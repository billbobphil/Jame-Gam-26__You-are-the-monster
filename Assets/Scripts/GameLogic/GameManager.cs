using System.Collections.Generic;
using AI;
using Cards;
using UnityEngine;

namespace GameLogic
{
    public class GameManager : MonoBehaviour
    {
        public ReferencePig referencePig;
        public List<Card> startingPlayerCards = new();
        public List<Opponent> opponents = new();
        public int currentLevel = 0;
        
        private void Awake()
        {
            CreateStartingDeck();
        }

        private void OnEnable()
        {
            MatchManager.OnMatchEnd += HandleMatchEnd;
        }
        
        private void OnDisable()
        {
            MatchManager.OnMatchEnd -= HandleMatchEnd;
        }

        private void Start()
        {
            //TODO: should probably be moved to a call that happens on some sort of button click?
            Opponent opponent = Instantiate(opponents[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
            referencePig.matchManager.RunMatch(opponent);
        }

        private void CreateStartingDeck()
        {
            foreach (Card card in startingPlayerCards)
            {
                referencePig.player.deckManager.AddToDeckComposition(card);
            }
        }

        private void HandleMatchEnd(bool didPlayerWin)
        {
            //TODO: will need to adjust to accept a boolean stating who won the match
            
            Debug.Log("Match concluded.");
            Debug.Log($"Did player win? : {didPlayerWin}");
            //TODO: if player won then need to give health back
            //TODO: if ai won then need to end game
            //start logic to go to next level
        }
    }
}