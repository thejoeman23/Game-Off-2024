using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BeginningOfScene : MonoBehaviour
{
    [SerializeField] Image fadeScreen;
    [SerializeField] AudioSource music;
    private float desiredMusicVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        desiredMusicVolume = music.volume;
        music.volume = 0;
        StartCoroutine(beginingSequence());
    }

    IEnumerator beginingSequence()
    {
        fadeScreen.GetComponent<Image>().color = Color.black;
        fadeScreen.transform.localScale = new Vector3(200, 200, 200);
        fadeScreen.GetComponent<Image>().DOFade(0, 6f).Play();
        music.DOFade(desiredMusicVolume, 20).Play();

        yield return new WaitForSeconds(10f);

        fadeScreen.GetComponent<Image>().color = Color.white;
        fadeScreen.transform.localScale = Vector3.zero;
    }
}
