using AI;
using UnityEngine;

namespace GameLogic
{
    public class MatchManager : MonoBehaviour
    {
        public delegate void EndMatch(bool didPlayerWin);
        public static event EndMatch OnMatchEnd;
        
        public ReferencePig referencePig;
        public int currentRound;
        private Opponent _aiOpponent;
        private Player _player;
        
        private void OnEnable()
        {
            RoundManager.OnRoundEnd += HandleRoundEnd;
        }
        
        private void OnDisable()
        {
            RoundManager.OnRoundEnd -= HandleRoundEnd;
        }
        
        public void RunMatch(Opponent opponent)
        {
            InitializeMatch(opponent);
            currentRound++;
            referencePig.roundManager.RunRound(_aiOpponent);
        }

        private void InitializeMatch(Opponent opponent)
        {
            currentRound = 0;
            
            //Get opponent & player
            _aiOpponent = opponent;
            _player = referencePig.player;

            //Build decks
            _player.deckManager.InitializeDeck();
            _aiOpponent.deckManager.InitializeDeck();
            
            //Shuffle decks
            _player.deckManager.ShuffleDeck();
            _aiOpponent.deckManager.ShuffleDeck();
            
            //Draw starting hands
            _player.handManager.DrawStartingHand();
            _aiOpponent.handManager.DrawStartingHand();
        }

        private void HandleRoundEnd()
        {
            if (DidOpponentWin() || DidPlayerWin())
            {
                OnMatchEnd?.Invoke(DidPlayerWin());
            }
            else
            {
                currentRound++;
                referencePig.roundManager.RunRound(_aiOpponent);
            }
        }

        private bool DidPlayerWin()
        {
            return _aiOpponent.IsDead();
        }
        
        private bool DidOpponentWin()
        {
            return _player.IsDead();
        }
    }
}
