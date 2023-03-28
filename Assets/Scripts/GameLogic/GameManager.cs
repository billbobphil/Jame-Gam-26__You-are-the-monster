using System.Collections.Generic;
using AI;
using Cards;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class GameManager : MonoBehaviour
    {
        public ReferencePig referencePig;
        public static List<Card> PlayerDeckComposition = new();
        public List<Card> startingDeck;
        public List<Opponent> opponents = new();
        public static int CurrentLevel = -1;
        
        private void Awake()
        {
            CurrentLevel++;
            referencePig.levelText.text = $"{CurrentLevel + 1}";
            
            if (CurrentLevel == 0)
            {
                foreach (Card card in startingDeck)
                {
                    PlayerDeckComposition.Add(card);
                }
            }
            
            CreateDeck();
            referencePig.damageTakenText.text = "";
            referencePig.opponentDamageText.text = "";
            referencePig.victoryPanel.SetActive(false);
            referencePig.defeatPanel.SetActive(false);
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
            Opponent opponent = Instantiate(opponents[CurrentLevel], new Vector3(0, 0, 0), Quaternion.identity);
            referencePig.matchManager.RunMatch(opponent);
        }

        private void CreateDeck()
        {
            foreach (Card card in PlayerDeckComposition)
            {
                referencePig.player.deckManager.AddToDeckComposition(card);
            }
        }

        private void HandleMatchEnd(bool didPlayerWin)
        {
            Debug.Log("Match concluded.");
            Debug.Log($"Did player win? : {didPlayerWin}");
            
            if (didPlayerWin)
            {
                Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
                player.Heal(player.usedHealthAsMana);
                
                if (CurrentLevel == 4)
                {
                    referencePig.gameWinPanel.SetActive(true);
                }
                else
                {
                    referencePig.victoryPanel.SetActive(true);    
                }
            }
            else
            {
                referencePig.defeatPanel.SetActive(true);
            }
        }

        public void OnClickVictory()
        {
            SceneManager.LoadScene(1);
        }

        public void OnClickMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void OnClickCredits()
        {
            SceneManager.LoadScene(3);
        }

        public void OnClickTutorial()
        {
            SceneManager.LoadScene(4);
        }
    }
}