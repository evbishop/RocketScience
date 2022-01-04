using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 1f, restartLevelDelay = 2.5f;
    int currentScene;

    void Awake()
    {
        if (FindObjectsOfType<Wind>().Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        if (currentScene + 1 < SceneManager.sceneCountInBuildSettings)
            currentScene++;
        else currentScene = 0;
        SceneManager.LoadScene(currentScene);
    }

    public IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(restartLevelDelay);
        SceneManager.LoadScene(currentScene);
    }
}
