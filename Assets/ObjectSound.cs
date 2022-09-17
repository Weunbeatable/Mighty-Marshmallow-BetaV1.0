using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    [SerializeField] AudioSource objectsSound;
    // Start is called before the first frame update
    void Start()
    {
        objectsSound = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        objectsSound.Play();
    }
}
