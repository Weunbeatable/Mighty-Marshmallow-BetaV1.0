using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip[] coinPickupSFX = new AudioClip[2];
    AudioClip randomCoinSound;
    int index;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        randomCoinSound = coinPickupSFX[Random.Range(0, coinPickupSFX.Length)];
        if(collision.tag == "Hero")
        {
            AudioSource.PlayClipAtPoint(randomCoinSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
