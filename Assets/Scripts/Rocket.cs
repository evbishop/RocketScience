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
        float deltaZ = -Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, 0, deltaZ));
    }
}