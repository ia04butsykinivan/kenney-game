using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material mat;
    float distance = 0.1f;
    
    [Range(0f, 0.5f)]
    public float speed = 0.2f;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        distance += speed * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
