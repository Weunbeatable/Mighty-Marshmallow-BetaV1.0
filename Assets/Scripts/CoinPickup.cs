using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip[] coinPickupSFX = new AudioClip[2];
    AudioClip randomCoinSound;
    [SerializeField] int pointsForCoinPickup = 100;

    bool wasCollected = false; // so you can't pickup the object twice 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        randomCoinSound = coinPickupSFX[UnityEngine.Random.Range(0, coinPickupSFX.Length)];
        if(collision.tag == "Hero" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToSCore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(randomCoinSound, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
