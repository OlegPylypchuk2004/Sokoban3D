using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoroutineSceneLoader : ISceneLoader
{
    private MonoBehaviour _monoBehaviour;

    public CoroutineSceneLoader(MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
    }

    public void Load(int index, float delay = 0f)
    {
        AsyncOperation loadAsyncOperation = SceneManager.LoadSceneAsync(index);
        loadAsyncOperation.allowSceneActivation = false;

        _monoBehaviour.StartCoroutine(CountDelay(loadAsyncOperation, delay));
    }

    public void Load(string name, float delay = 0f)
    {
        AsyncOperation loadAsyncOperation = SceneManager.LoadSceneAsync(name);
        loadAsyncOperation.allowSceneActivation = false;

        _monoBehaviour.StartCoroutine(CountDelay(loadAsyncOperation, delay));
    }

    public void RestartCurrent(float delay = 0f)
    {
        Load(SceneManager.GetActiveScene().buildIndex, delay);
    }

    private IEnumerator CountDelay(AsyncOperation loadAsyncOperation, float delay)
    {
        yield return new WaitForSeconds(delay);

        yield return new WaitUntil(() => loadAsyncOperation.progress < 1f);

        loadAsyncOperation.allowSceneActivation = true;
    }
}