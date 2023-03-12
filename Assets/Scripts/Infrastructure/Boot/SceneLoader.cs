using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Infrastructure.Boot
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        private IEnumerator LoadScene(string target, Action onLoaded)
        {
            if(SceneManager.GetActiveScene().name == target)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitScene = SceneManager.LoadSceneAsync(target);

            while(waitScene.isDone == false)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
