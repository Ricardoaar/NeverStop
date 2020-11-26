using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeJeffreyctm : MonoBehaviour
{
    public TextMeshProUGUI textFade;
    private bool _goOn;
    private Coroutine _cFade;

    public Image back;

    private void Awake()
    {
        InvokeRepeating("Fade", 0.05f, 0.05f);
        _goOn = false;
    }

    private void Update()
    {
        if (Input.anyKey && _cFade == null)
        {
            _cFade = StartCoroutine(fade());
        }
    }


    private void Fade()
    {
        if (textFade.color.a <= 0)
        {
            _goOn = true;
        }
        else if (textFade.color.a >= 1)
        {
            _goOn = false;
        }

        if (_goOn)
        {
            textFade.color = new Color(textFade.color.r, textFade.color.g, textFade.color.b, textFade.color.a + 0.1f);
        }
        else
        {
            textFade.color = new Color(textFade.color.r, textFade.color.g, textFade.color.b, textFade.color.a - 0.1f);
        }
    }


    IEnumerator fade()
    {
        CancelInvoke();
        for (float i = 0; i < 1; i += 0.05f)
        {
            back.color = new Color(255, 255, 255, back.color.a - i);
            textFade.color = new Color(255, 255, 255, back.color.a - i);
            yield return new WaitForSecondsRealtime(0.05f);
        }

        gameObject.SetActive(false);
    }
}