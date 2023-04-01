using GameLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class NavigationHelper : MonoBehaviour
    {
        private void Start()
        {
            GameManager.CurrentLevel = -1;
            Player.CurrentHealth = 30;
            
        }
        public void GoToGame()
        {
            SceneManager.LoadScene(2);
        }

        public void GoToCredits()
        {
            SceneManager.LoadScene(3);
        }

        public void GoToTutorial()
        {
            SceneManager.LoadScene(4);
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
