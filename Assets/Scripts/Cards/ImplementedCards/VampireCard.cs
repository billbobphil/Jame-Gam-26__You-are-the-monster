using UnityEngine;

namespace Cards.ImplementedCards
{
    public class VampireCard : MonsterCard
    {
        public override void Play()
        {
            base.Play();
            Debug.Log("Playing Vampire Card");
        }
    }
}