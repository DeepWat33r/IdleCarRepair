using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        public int gameSceneIndex = 1;
        
        public void LoadSceneAsync()
        {
            StartCoroutine(LoadSceneCoroutine());
        }

        private IEnumerator LoadSceneCoroutine()
        {
            //Debug.Log("Loading started...");

            AsyncOperation operation = SceneManager.LoadSceneAsync(gameSceneIndex);
            
            while (!operation.isDone)
            {
                //Debug.Log($"Loading progress: {operation.progress}");

                yield return null;
            }
            //Debug.Log("Scene Loaded!");
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
