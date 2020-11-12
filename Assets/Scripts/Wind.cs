using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<Wind>().Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
}
