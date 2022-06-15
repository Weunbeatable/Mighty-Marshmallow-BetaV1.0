
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;


    Vector3 initialPosition;
    GameObject playerPosition;
    public Animator FadeOut;
    void Start()
    {
        initialPosition = transform.position;
        playerPosition = GameObject.FindWithTag("Hero");
    }

    // Update is called once per frame

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            transform.position = playerPosition.transform.position + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            FadeOut.Play("Fade_Out");
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
       
        transform.position = initialPosition;
       
    }
    /*IEnumerator FadingOut(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = fadescreen.GetComponent<RawImage>().color;
        float fadeAmount;

        if (fadeToBlack)
            while (fadescreen.GetComponent<RawImage>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadescreen.GetComponent<RawImage>().color = objectColor;
                yield return null;
            }

        yield return new WaitForEndOfFrame();
    }*/
}
