using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParallaxController : MonoBehaviour
{
    private Transform _cam;
    private Vector3 _camStartPos;
    private float _distance;
    
    private GameObject[] _backgrounds;
    private Material[] _mat;
    private float[] _backSpeed;
    
    private float _farthestBack;
    
    [Range(0.01f, 0.05f)]
    public float ParallaxSpeed;
    void Start()
    {
        _cam = Camera.main.transform;
        _camStartPos = _cam.position;
        
        int backCount = transform.childCount;
        _mat = new Material[backCount];
        _backSpeed = new float[backCount];
        _backgrounds = new GameObject[backCount];
        
        for (int i = 0; i < backCount; i++)
        {
            _backgrounds[i] = transform.GetChild(i).gameObject;
            _mat[i] = _backgrounds[i].GetComponent<Renderer>().material;
        }
        
        BackSpeedCalculate(backCount);
    }

    private void LateUpdate()
    {
        _distance = _cam.position.x - _camStartPos.x;
        transform.position = new Vector3(_cam.position.x, transform.position.y, 0);
        
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            float speed = _backSpeed[i] * ParallaxSpeed;
            _mat[i].SetTextureOffset("_MainTex", new Vector2(_distance, 0) * speed);
        }
    }
    
    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            float distanceToCam = _backgrounds[i].transform.position.z - _cam.position.z;
            if (distanceToCam > _farthestBack)
            {
                _farthestBack = distanceToCam;
            }

            _backSpeed[i] = 1 - distanceToCam / _farthestBack;
        }
    }
}