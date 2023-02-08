using System.Collections;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    public Transform transformToShrink;

    private Material material;
    private float fade = 0f;
    private const float WAIT_TIME = 3.0f;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found on object: " + gameObject.name);
        }

        fade = 0.0f;
        material.SetFloat("_Fade", fade);

        transformToShrink = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fade == 1.0f)
            {
                StartCoroutine(Dissolve());
            }
            else if (fade == 0.0f)
            {
                StartCoroutine(Materialize());
            }
        }
    }

    private IEnumerator Dissolve()
    {
        while (fade > 0.0f)
        {
            fade -= Time.deltaTime / 2;
            material.SetFloat("_Fade", fade);
            yield return null;
        }

        fade = 0.0f;
        material.SetFloat("_Fade", fade);
    }

    private IEnumerator Materialize()
    {
        while (fade < 1.0f)
        {
            fade += Time.deltaTime / 2;
            material.SetFloat("_Fade", fade);
            yield return null;
        }
        fade = 1.0f;
        material.SetFloat("_Fade", fade);

        // wait for the specified time
        yield return new WaitForSeconds(WAIT_TIME);

        // shrink the object
        ShrinkAndReposition shrink = transformToShrink.gameObject.GetComponent<ShrinkAndReposition>();
    }
}