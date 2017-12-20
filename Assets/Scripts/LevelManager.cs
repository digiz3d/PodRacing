using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Multiple LevelManager scripts !!");
            DestroyImmediate(gameObject);
        }
    }

    public void LoadTheOnlyMap()
    {
        StartCoroutine(LoadScene("boonta training course"));
    }
    private IEnumerator LoadScene(string name)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        while (!async.isDone)
        {
            yield return null;
        }
    }
    public void GoBackToMainMenu()
    {
        StartCoroutine(LoadScene("main menu"));
    }
}
