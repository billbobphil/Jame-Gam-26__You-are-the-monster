using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class NavigationHelper : MonoBehaviour
    {
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
