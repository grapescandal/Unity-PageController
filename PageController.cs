using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pages;

    public static PageController ins;

    private int currentPage = 0;

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
        }
    }

    public void ShowPage(int index)
    {
        CanvasGroup groupOld = pages[currentPage].GetComponent<CanvasGroup>();
        CanvasGroup groupNew = pages[index].GetComponent<CanvasGroup>();
        StartCoroutine(FadePage(index, groupOld, groupNew));
    }

    private IEnumerator FadePage(int index, CanvasGroup groupOld, CanvasGroup groupNew)
    {
        pages[index].SetActive(true);

        float progress = 0.0f;

        while (progress < 1)
        {
            progress += Time.deltaTime * 10;

            if (progress >= 1.0f)
            {
                progress = 1.0f;
                pages[currentPage].SetActive(false);
                currentPage = index;
            }
            groupOld.alpha = Mathf.Lerp(1f, 0f, progress);
            groupNew.alpha = Mathf.Lerp(0f, 1f, progress);
            yield return null;
        }
    }
}
