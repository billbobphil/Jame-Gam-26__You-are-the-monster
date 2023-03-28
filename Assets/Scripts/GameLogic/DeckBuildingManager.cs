using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class DeckBuildingManager : MonoBehaviour
    {
        public List<PlayerCard> cardPool;
        public Transform cardOneLocation;
        public Transform cardTwoLocation;
        public Transform cardThreeLocation;
        private int _numberOfCardsToChoose = 3;
        private (PlayerCard card, PlayerCard prefab) _cardOne;
        private (PlayerCard card, PlayerCard prefab) _cardTwo;
        private (PlayerCard card, PlayerCard prefab) _cardThree;
        
        public void Start()
        {
            List<int> usedIndexes = new();
            
            for(int i = 0; i < cardPool.Count; i++)
            {
                //get random number between 0 and cardPool.Count`
                int randomIndex = UnityEngine.Random.Range(0, cardPool.Count);

                while(usedIndexes.Contains(randomIndex))
                {
                    randomIndex = UnityEngine.Random.Range(0, cardPool.Count);
                }

                PlayerCard selectedCard = cardPool[randomIndex];
                usedIndexes.Add(randomIndex);

                switch (i)
                {
                    case 0:
                        PlayerCard cardOne = Instantiate(selectedCard, new Vector3(cardOneLocation.position.x, cardOneLocation.position.y, 1), Quaternion.identity);
                        cardOne.isDisabled = true;
                        _cardOne.card = cardOne;
                        _cardOne.prefab = selectedCard;
                        break;
                    case 1:
                        PlayerCard cardTwo = Instantiate(selectedCard, new Vector3(cardTwoLocation.position.x, cardTwoLocation.position.y, 1), Quaternion.identity);
                        cardTwo.isDisabled = true;
                        _cardTwo.card = cardTwo;
                        _cardTwo.prefab = selectedCard;
                        break;
                    case 2:
                        PlayerCard cardThree = Instantiate(selectedCard, new Vector3(cardThreeLocation.position.x, cardThreeLocation.position.y, 1), Quaternion.identity);
                        cardThree.isDisabled = true;
                        _cardThree.card = cardThree;
                        _cardThree.prefab = selectedCard;
                        break;
                }
            }
        }
        
        public void OnCardOneClick()
        {
            GameManager.PlayerDeckComposition.Add(_cardOne.prefab);
            TransitionBackToGame();
        }
        
        public void OnCardTwoClick()
        {
            GameManager.PlayerDeckComposition.Add(_cardTwo.prefab);
            TransitionBackToGame();
        }
        
        public void OnCardThreeClick()
        {
            GameManager.PlayerDeckComposition.Add(_cardThree.prefab);
            TransitionBackToGame();
        }

        private void TransitionBackToGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
