using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolScript : MonoBehaviour
{
    [SerializeField] float fadeInTime = 1f;

    [SerializeField] Image logo;
    [SerializeField] Image fadeScreen;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeScreen.gameObject.SetActive(true);

        fadeScreen.color = Color.black;
        logo.color = Color.clear;
        text1.color = Color.clear;
        text2.color = Color.clear;

        StartCoroutine(beginingSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator beginingSequence()
    {
        text1.gameObject.SetActive(true);
        text1.DOColor(Color.white, fadeInTime).SetLoops(1).Play();

        yield return new WaitForSeconds(1);

        logo.gameObject.SetActive(true);
        logo.DOColor(Color.white, fadeInTime).SetLoops(1).Play();

        yield return new WaitForSeconds(2f);

        text1.DOColor(Color.clear, 1).SetLoops(1).Play();
        text2.gameObject.SetActive(true);
        text2.DOColor(Color.white, fadeInTime).SetLoops(1).Play();

        yield return new WaitForSeconds(3f);
        logo.DOColor(Color.clear, fadeInTime).SetLoops(1).Play();
        text2.DOColor(Color.clear, fadeInTime).SetLoops(1).Play();
        yield return new WaitForSeconds(2f);
        fadeScreen.DOFade(0, 1).SetLoops(1).Play();
    }
}
