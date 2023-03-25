using UnityEngine;

namespace GameLogic
{
    public class MatchManager : MonoBehaviour
    {
        public ReferencePig referencePig;
        public int numberOfRoundsTranspired;
        public void RunMatch()
        {
            InitializeMatch();
            
            bool playerWinConditionMet = false;
            bool opponentWinConditionMet = false;
            
            while (!playerWinConditionMet && !opponentWinConditionMet)
            {
                numberOfRoundsTranspired++;
                referencePig.roundManager.RunRound();
                
                playerWinConditionMet = DidPlayerWin();
                opponentWinConditionMet = DidOpponentWin();
            }
            
            EndMatch();
        }

        private void InitializeMatch()
        {
            numberOfRoundsTranspired = 0;
            //Get an AI opponent
            //Get a player
            //Shuffle both decks
            //Draw starting hands
        }

        private void EndMatch()
        {
            
        }

        private bool DidPlayerWin()
        {
            return false;
        }
        
        private bool DidOpponentWin()
        {
            return false;
        }
    }
}
