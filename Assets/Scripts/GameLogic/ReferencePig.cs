using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class ReferencePig : MonoBehaviour
    {
        public Player player;
        public MatchManager matchManager;
        public RoundManager roundManager;
        public TextMeshProUGUI playerHealthText;
        public GameObject heartOne;
        public GameObject heartTwo;
        public GameObject heartThree;
        public TextMeshProUGUI cardsRemainingText;
    }
}
