using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int scene;
    [SerializeField] GameObject fadeObject;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string hoverText;
    string originalText;

    // Start is called before the first frame update
    void Start()
    {
        originalText = text.text;
        Button myButton = GetComponent<Button>();

        myButton.onClick.AddListener(sendToGame);
    }

    void sendToGame()
    {
        StartCoroutine(transition());
    }

    IEnumerator transition()
    {
        fadeObject.SetActive(true);
        fadeObject.GetComponent<Image>().DOFade(1, 1).SetLoops(1).Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = hoverText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = originalText;
        Debug.Log("skibidid");
    }

}
