using System.Collections;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    //public Transform transformToShrink;

    private Material material;
    private float fade = 0f;
    private const float WAIT_TIME = .5f;
    public bool isMaterialized = false;

    private void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            material = renderer.material;
            if (material == null)
            {
                Debug.LogError("Material not found on object: " + gameObject.name);
                return;
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on object: " + gameObject.name);
            return;
        }

        fade = 0.0f;
        material.SetFloat("_Fade", fade);
    }

    public IEnumerator Dissolve()
    {
        if (material == null)
        {
            yield break;
        }

        while (fade > 0.0f)
        {
            fade -= Time.deltaTime / 2;
            material.SetFloat("_Fade", fade);
            yield return null;
        }

        fade = 0.0f;
        material.SetFloat("_Fade", fade);
    }

    public IEnumerator Materialize()
    {
        if (material == null)
        {
            yield break;
        }

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
        isMaterialized = true;
    }
}