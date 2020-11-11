using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float speed = 10f, rotationSpeed = 10f;
    Rigidbody rb;
    AudioSource audioSource;
    int currentScene;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }

    void Thrust()
    {
        float deltaY = Input.GetAxis("Jump") * Time.deltaTime * speed;
        audioSource.volume = Input.GetAxis("Jump");
        rb.AddRelativeForce(0, deltaY, 0);
    }

    void Rotate()
    {
        rb.freezeRotation = true;
        float deltaZ = -Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, 0, deltaZ));
        rb.freezeRotation = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 0.5f);
                break;
            default:
                state = State.Dying;
                Invoke("ReloadScene", 0.5f);
                break;
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }
}