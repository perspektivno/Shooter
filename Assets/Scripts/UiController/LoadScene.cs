using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UiController
{
    public class LoadScene : MonoBehaviour
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void SceneLoad(string name)
        {
            SceneManager.LoadScene(name);
        }



    }
}