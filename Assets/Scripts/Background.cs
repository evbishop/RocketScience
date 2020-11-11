using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.2f;
    Material material;
    Vector2 offSet;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        offSet = new Vector2(backgroundScrollSpeed, 0f);
    }

    void Update()
    {
        material.mainTextureOffset += offSet * Time.deltaTime;
    }
}
