using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class StoryTypeWriter : MonoBehaviour
{
    public float delay = 0.1f;
    public string[] paragraphs;
    private int currentIndex = 0;
    private bool isAnimating = false;
    public TMP_Text textComponent;
    public CanvasManager canvasManager;

    private void Start()
    {
        //textComponent = GetComponent<TMP_Text>();
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        while (currentIndex < paragraphs.Length)
        {
            isAnimating = true;
            string currentParagraph = paragraphs[currentIndex];
            int i = 0;
            while (i < currentParagraph.Length)
            {
                textComponent.text += currentParagraph[i];
                i++;
                yield return new WaitForSeconds(delay);
            }
            isAnimating = false;
            yield return new WaitForSeconds(1f); // Delay before clearing the text
            ClearText();
            currentIndex++;
        }
        canvasManager.MoveScene("Gameplay");
    }

    private void ClearText()
    {
        textComponent.text = "";
    }

    public bool IsAnimating()
    {
        return isAnimating;
    }
}
