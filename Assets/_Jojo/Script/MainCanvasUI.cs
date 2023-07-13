using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasUI : MonoBehaviour
{
    [SerializeField] private GameObject hintPanelUI;
    [SerializeField] private GameObject notePanelUI;

    [SerializeField] private GameObject mainCanvas;

    public void OpenNote()
    {
        mainCanvas.SetActive(false);
    }
}