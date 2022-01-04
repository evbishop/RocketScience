using System;
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
        if (FindObjectsOfType<GameManager>().Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Rocket.OnLevelFinished += HandleLevelFinished;
        Rocket.OnDeath += HandleDeath;
    }

    void OnDestroy()
    {
        Rocket.OnLevelFinished -= HandleLevelFinished;
        Rocket.OnDeath -= HandleDeath;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
    }

    void HandleLevelFinished()
    {
        StartCoroutine(LoadNextLevel());
    }

    void HandleDeath()
    {
        StartCoroutine(ReloadLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        if (currentScene + 1 < SceneManager.sceneCountInBuildSettings)
            currentScene++;
        else currentScene = 0;
        SceneManager.LoadScene(currentScene);
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(restartLevelDelay);
        SceneManager.LoadScene(currentScene);
    }
}
