using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAndReposition : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0, 2, 0);
    public float startScale = 2;

    public Vector3 targetPosition = new Vector3(0, 1.75f, 0);
    public float targetScale = 0.75f;

    public float transitionTime = 1.0f;
    private float timeElapsed = 0.0f;

    public bool shouldShrink = false;
    public bool isPositioned = false;

    private void Start()
    {
        transform.position = startPosition;
        transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    private void Update()
    {
        if (shouldShrink)
        {
            if (timeElapsed < transitionTime)
            {
                timeElapsed += Time.deltaTime;

                float t = timeElapsed / transitionTime;
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                transform.localScale = Vector3.Lerp(new Vector3(startScale, startScale, startScale), new Vector3(targetScale, targetScale, targetScale), t);
            }
            else
            {
                isPositioned = true;
                shouldShrink = false;
            }
        }
    }
}