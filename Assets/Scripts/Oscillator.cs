using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;
    const float tau = Mathf.PI * 2;
    float cycles, movementFactor;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startPos + offset;
    }
}
