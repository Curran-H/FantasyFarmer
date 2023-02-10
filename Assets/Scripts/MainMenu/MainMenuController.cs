using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private AlphaModifier blackBackground;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMPAlphaModifier playText;
    [SerializeField] private TMPAlphaModifier optionsText;
    [SerializeField] private TMPAlphaModifier quitText;

    private DissolveEffect dissolveEffect;
    private ShrinkAndReposition shrinkAndReposition;
    private bool introOver = false;

    private void Start()
    {
        dissolveEffect = title.GetComponent<DissolveEffect>();
        shrinkAndReposition = title.GetComponent<ShrinkAndReposition>();

        StartCoroutine(FadeRoutine());
        if (introOver == false)
        {
            FindObjectOfType<AudioManager>().Play("intro");
        }
    }

    private void Update()
    {
        if (dissolveEffect.isMaterialized == true)
        {
            shrinkAndReposition.shouldShrink = true;
            blackBackground.shouldChangeAlpha = true;

            if (shrinkAndReposition.isPositioned == true)
            {
                introOver = true;
                playButton.gameObject.SetActive(true);
                optionsButton.gameObject.SetActive(true);
                quitButton.gameObject.SetActive(true);

                playText.shouldChangeAlpha = true;
                optionsText.shouldChangeAlpha = true;
                quitText.shouldChangeAlpha = true;

                if (!FindObjectOfType<AudioManager>().IsPlaying("Title Screen Music"))
                {
                    FindObjectOfType<AudioManager>().Play("Title Screen Music");
                }
            }
        }
    }

    private IEnumerator FadeRoutine()
    {
        // Display title Game Object
        StartCoroutine(dissolveEffect.Materialize());
        yield return null;
    }
}