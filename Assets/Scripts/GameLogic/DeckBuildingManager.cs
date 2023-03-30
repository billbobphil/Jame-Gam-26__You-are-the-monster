using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class DeckBuildingManager : MonoBehaviour
    {
        public List<ModifierCard> modifierCardPool;
        public List<MonsterCard> monsterCardPool;
        public Transform cardOneLocation;
        public Transform cardTwoLocation;
        public Transform cardThreeLocation;
        private int _numberOfCardsToChoose = 3;
        private (ModifierCard modifierCard, ModifierCard prefabModifier, MonsterCard monsterCard, MonsterCard prefabMonster) _cardOne;
        private (ModifierCard modifierCard, ModifierCard prefabModifier, MonsterCard monsterCard, MonsterCard prefabMonster) _cardTwo;
        private (ModifierCard modifierCard, ModifierCard prefabModifier, MonsterCard monsterCard, MonsterCard prefabMonster) _cardThree;
        
        public void Start()
        {
            List<int> usedIndexes = new();
            
            for(int i = 0; i < modifierCardPool.Count; i++)
            {
                //get random number between 0 and cardPool.Count`
                int randomIndex = UnityEngine.Random.Range(0, modifierCardPool.Count);

                while(usedIndexes.Contains(randomIndex))
                {
                    randomIndex = UnityEngine.Random.Range(0, modifierCardPool.Count);
                }

                ModifierCard selectedModifier = modifierCardPool[randomIndex];
                MonsterCard selectedMonster = monsterCardPool[randomIndex];
                usedIndexes.Add(randomIndex);

                switch (i)
                {
                    case 0:
                        PrepareCardForChoice(selectedModifier, selectedMonster, cardOneLocation, ref _cardOne);
                        break;
                    case 1:
                        PrepareCardForChoice(selectedModifier, selectedMonster, cardTwoLocation, ref _cardTwo);
                        break;
                    case 2:
                        PrepareCardForChoice(selectedModifier, selectedMonster, cardThreeLocation, ref _cardThree);
                        break;
                }
            }
        }

        private void PrepareCardForChoice(ModifierCard selectedModifier, MonsterCard selectedMonster, Transform cardLocation,
            ref (ModifierCard modifierCard, ModifierCard prefabModifier, MonsterCard monsterCard, MonsterCard prefabMonster) attachToCard)
        {
            ModifierCard instantiatedModifier = Instantiate(selectedModifier, new Vector3(cardLocation.position.x, cardLocation.position.y, 1), Quaternion.identity);
            MonsterCard instantiatedMonster = Instantiate(selectedMonster, new Vector3(cardLocation.position.x + 4, cardLocation.position.y + 6, 1), Quaternion.identity);
            instantiatedMonster.transform.localScale = new Vector3(.75f, .75f, .75f);
            instantiatedMonster.GetComponent<SpriteRenderer>().sortingOrder = -2;
            foreach (Transform child in instantiatedMonster.transform)
            {
                SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = -1;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
            instantiatedModifier.isDisabled = true;
            instantiatedMonster.isDisabled = true;
            attachToCard.modifierCard = instantiatedModifier;
            attachToCard.prefabModifier = selectedModifier;
            attachToCard.monsterCard = instantiatedMonster;
            attachToCard.prefabMonster = selectedMonster;
        }
        
        public void OnCardOneClick()
        {
            GameManager.PlayerDeckComposition.Add(_cardOne.prefabModifier);
            GameManager.PlayerDeckComposition.Add(_cardOne.prefabMonster);
            TransitionBackToGame();
        }
        
        public void OnCardTwoClick()
        {
            GameManager.PlayerDeckComposition.Add(_cardTwo.prefabModifier);
            GameManager.PlayerDeckComposition.Add(_cardTwo.prefabMonster);
            TransitionBackToGame();
        }
        
        public void OnCardThreeClick()
        {
            GameManager.PlayerDeckComposition.Add(_cardThree.prefabModifier);
            GameManager.PlayerDeckComposition.Add(_cardThree.prefabMonster);
            TransitionBackToGame();
        }

        private void TransitionBackToGame()
        {
            SceneManager.LoadScene(2);
        }
    }
}
