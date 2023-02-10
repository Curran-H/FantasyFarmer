using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMPAlphaModifier : MonoBehaviour
{
    public TextMeshProUGUI button;
    public float startAlpha = 1.0f;
    public float targetAlpha = 0.0f;

    public float transitionTime = 1.0f;
    private float timeElapsed = 0.0f;

    public bool shouldChangeAlpha = false;

    private Color originalColor;

    private void Start()
    {
        button = GetComponent<TextMeshProUGUI>();
        originalColor = button.color;
        originalColor.a = startAlpha;
        button.color = originalColor;
    }

    private void Update()
    {
        if (shouldChangeAlpha)
        {
            if (timeElapsed < transitionTime)
            {
                timeElapsed += Time.deltaTime;

                float t = timeElapsed / transitionTime;
                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);
                button.color = newColor;
            }
            else
            {
                shouldChangeAlpha = false;
            }
        }
    }
}
