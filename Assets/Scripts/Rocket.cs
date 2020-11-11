using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float speed = 10f, rotationSpeed = 10f;
    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Thrust();
        Rotate();
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
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
}