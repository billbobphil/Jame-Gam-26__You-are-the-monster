using UnityEngine;

namespace GameLogic
{
    public class RoundManager : MonoBehaviour
    {
        public ReferencePig referencePig;
        public void RunRound()
        {
            //player and Ai both draw
                //possible condition for empty decks
            //Generate AI intention and show to player
            //Player chooses cards to play
                //limit one monster type card
                //unlimited non-monster type cards
            //Player 'submits' turn
                //Player cards must be removed from hand and added to discard
                //AI cards must be removed from hand and added to discard
            //Forces battle
                //Check power conditions
                    //if player loses, player takes damage equal to total power of AI
                    //if AI loses, AI takes a 'wound' of which they can take 3
        }

        private void PlayerDraw()
        {
            
        }

        private void AiDraw()
        {
            
        }
        
        private void GenerateAiIntention()
        {
            
        }

        public void OnPlayerSubmitTurn()
        {
            
        }

        private void RunBattle()
        {
            CalculateBattleResults();
        }
        
        private void CalculateBattleResults()
        {
            //maybe another method call here to apply outcomes of the round, or maybe that goes back to the match manager?   
        }
    }
}
