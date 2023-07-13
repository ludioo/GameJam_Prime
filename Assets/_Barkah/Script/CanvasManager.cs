using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] CanvasGroup mainCanvas;

    private void Awake()
    {
        mainCanvas = GetComponent<CanvasGroup>();
        mainCanvas.alpha = 0;
        LeanTween.alphaCanvas(mainCanvas, 1, 1);
    }


    public void MoveScene(string nameScene)
    {
        LeanTween.alphaCanvas(mainCanvas, 0, 1).setOnComplete(() =>
            SceneManager.LoadScene(nameScene));
    }
}
