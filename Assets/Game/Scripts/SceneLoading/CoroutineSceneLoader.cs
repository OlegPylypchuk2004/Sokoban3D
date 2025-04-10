using System;
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

    public event Action LoadStarted;
    public event Action LoadCompleted;

    public bool IsLoading { get; private set; }

    public void Load(int index, float delay = 0f)
    {
        if (IsLoading)
        {
            return;
        }

        IsLoading = true;

        AsyncOperation loadAsyncOperation = SceneManager.LoadSceneAsync(index);
        loadAsyncOperation.allowSceneActivation = false;

        _monoBehaviour.StartCoroutine(LoadWithDelay(loadAsyncOperation, delay));
    }

    public void Load(string name, float delay = 0f)
    {
        if (IsLoading)
        {
            return;
        }

        IsLoading = true;

        AsyncOperation loadAsyncOperation = SceneManager.LoadSceneAsync(name);
        loadAsyncOperation.allowSceneActivation = false;

        _monoBehaviour.StartCoroutine(LoadWithDelay(loadAsyncOperation, delay));
    }

    public void RestartCurrent(float delay = 0f)
    {
        Load(SceneManager.GetActiveScene().buildIndex, delay);
    }

    private IEnumerator LoadWithDelay(AsyncOperation loadAsyncOperation, float delay)
    {
        LoadStarted?.Invoke();

        yield return new WaitForSeconds(delay);

        yield return new WaitUntil(() => loadAsyncOperation.progress < 1f);

        loadAsyncOperation.allowSceneActivation = true;

        IsLoading = false;

        LoadCompleted?.Invoke();
    }
}