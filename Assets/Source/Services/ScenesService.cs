using Source.Constants;
using UnityEngine.SceneManagement;

namespace Source.Services
{
    public class ScenesService : IScenesService
    {
        public void LoadMenu()
        {
            SceneManager.LoadScene(ScenesConstants.MenuScene);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(ScenesConstants.GameScene);
        }

        public bool IsMenu()
        {
            return SceneManager.GetActiveScene().buildIndex == ScenesConstants.MenuScene;
        }

        public bool IsGame()
        {
            return SceneManager.GetActiveScene().buildIndex == ScenesConstants.GameScene;
        }
    }
}
