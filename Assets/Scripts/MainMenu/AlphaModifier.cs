using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaModifier : MonoBehaviour
{
    public Image image;
    public float startAlpha = 1.0f;
    public float targetAlpha = 0.0f;

    public float transitionTime = 1.0f;
    private float timeElapsed = 0.0f;

    public bool shouldChangeAlpha = false;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, startAlpha);
    }

    private void Update()
    {
        if (shouldChangeAlpha)
        {
            if (timeElapsed < transitionTime)
            {
                timeElapsed += Time.deltaTime;

                float t = timeElapsed / transitionTime;
                image.color = Color.Lerp(new Color(0, 0, 0, startAlpha), new Color(0, 0, 0, targetAlpha), t);
            }
            else
            {
                shouldChangeAlpha = false;
            }
        }
    }
}
