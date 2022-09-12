using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [SerializeField] float scrollRate = .5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ymove = scrollRate * Time.deltaTime;
        transform.Translate(Vector3.up * ymove);
    }
}
