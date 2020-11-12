using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float speed = 10f, rotationSpeed = 10f, nextLevelDelay = 1f, restartLevelDelay = 2.5f;
    [SerializeField] AudioClip mainEngineAudio, successAudio, deathAudio;
    [SerializeField] ParticleSystem engineVFX, successVFX, deathVFX;
    Rigidbody rb;
    AudioSource audioSource;
    int currentScene;

    bool isTransitioning = false;
    bool collisionsDisabled = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = mainEngineAudio;
        audioSource.Play();
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (!isTransitioning)
        {
            Thrust();
            Rotate();
        }
        if (Debug.isDebugBuild) RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) LoadNextLevel();
        else if (Input.GetKeyDown(KeyCode.C)) collisionsDisabled = !collisionsDisabled;
    }

    void Thrust()
    {
        float deltaY = Input.GetAxis("Jump") * Time.deltaTime * speed;
        audioSource.volume = Input.GetAxis("Jump");
        rb.AddRelativeForce(0, deltaY, 0);
        if (deltaY > Mathf.Epsilon) engineVFX.Play();
        else engineVFX.Stop();
    }

    void Rotate()
    {
        float deltaZ = -Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        if (deltaZ<Mathf.Epsilon || deltaZ>Mathf.Epsilon)
        {
            rb.freezeRotation = true;
            transform.Rotate(new Vector3(0, 0, deltaZ));
            rb.freezeRotation = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                Transcend();
                break;
            default:
                Die();
                break;
        }
    }

    private void Transcend()
    {
        isTransitioning = true;
        audioSource.volume = 0f;
        AudioSource.PlayClipAtPoint(successAudio, Camera.main.transform.position, 0.4f);
        successVFX.Play();
        Invoke("LoadNextScene", nextLevelDelay);
    }

    void Die()
    {
        isTransitioning = true;
        audioSource.volume = 0f;
        AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position, 0.4f);
        deathVFX.Play();
        Invoke("ReloadScene", restartLevelDelay);
    }

    void LoadNextLevel()
    {
        if (currentScene + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentScene + 1);
        else LoadLevelOne();
    }

    void LoadLevelOne()
    {
        SceneManager.LoadScene(0);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }
}