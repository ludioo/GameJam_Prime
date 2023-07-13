using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public void OpenPanel(GameObject panelToOpen)
    {
        panelToOpen.SetActive(true);
        CanvasGroup panelCG = panelToOpen.GetComponent<CanvasGroup>();

        if (panelCG == null)
        {
            panelCG = panelToOpen.AddComponent<CanvasGroup>();
            panelCG.alpha = 0;
        }
        else
        {
            panelCG.alpha = 0;
        }

        LeanTween.alphaCanvas(panelCG, 1, 0.5f);
    }

    public void ClosePanel(GameObject panelToClose)
    {
        CanvasGroup panelCG = panelToClose.GetComponent<CanvasGroup>();

        if (panelCG == null)
        {
            panelCG = panelToClose.AddComponent<CanvasGroup>();
            panelCG.alpha = 1;
        }
        else
        {
            panelCG.alpha = 1;
        }

        LeanTween.alphaCanvas(panelCG, 0, 0.5f).setOnComplete(() =>
            panelToClose.SetActive(false));
    }
}
