using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public void OpenPanel(GameObject panelToOpen)
    {
        panelToOpen.SetActive(true);
        CanvasGroup panelCG = panelToOpen.AddComponent(typeof(CanvasGroup)) as CanvasGroup;
        LeanTween.alphaCanvas(panelCG, 1, 0.5f);
    }

    public void ClosePanel(GameObject panelToClose)
    {
        CanvasGroup panelCG = panelToClose.AddComponent(typeof(CanvasGroup)) as CanvasGroup;
        LeanTween.alphaCanvas(panelCG, 0, 0.5f).setOnComplete(() =>
              panelToClose.SetActive(false));
    }
}
