using System.Collections.Generic;
using Cards;
using GameLogic;
using UnityEngine;

namespace AI
{
    public class Opponent : CardUser
    {
        public Transform intentionLocation;
        public int numberOfWounds = 0;
        [Range(1, 5)]
        public int maxNumberOfWounds = 3;
        public List<Card> cardsForDeckComposition;
        private ReferencePig _referencePig;
        public AudioSource audioSource;

        public void Awake()
        {
            foreach (Card card in cardsForDeckComposition)
            {
                deckManager.AddToDeckComposition(card);
            }
        }

        public void Start()
        {
            _referencePig = GameObject.FindWithTag("Overseer").GetComponent<ReferencePig>();
        }

        public void Wound()
        {
            numberOfWounds++;
            audioSource.Play();

            switch (numberOfWounds)
            {
                case 1:
                    _referencePig.heartThree.SetActive(false);
                    break;
                case 2:
                    _referencePig.heartTwo.SetActive(false);
                    break;
                case 3:
                    _referencePig.heartOne.SetActive(false);
                    break;
            }
        }
        
        public bool IsDead()
        {
            return numberOfWounds >= maxNumberOfWounds;
        }
    }
}