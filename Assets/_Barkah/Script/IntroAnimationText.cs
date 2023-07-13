using UnityEngine;
using TMPro;
using System.Collections;

public class IntroAnimationText : MonoBehaviour
{
    public float letterDelay = 0.2f;  // Delay between animating each letter
    public string finalText = "am I ?";  // The final text to be displayed
    private TMP_Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        string originalText = "am I.";
        textComponent.text = "";  // Clear the initial text

        // Animate the letters of the original text
        for (int i = 0; i < originalText.Length; i++)
        {
            textComponent.text += originalText[i];
            yield return new WaitForSeconds(letterDelay);
        }

        // Delay before deleting the last character
        yield return new WaitForSeconds(1f);

        // Delete the last character (".")
        textComponent.text = textComponent.text.Remove(textComponent.text.Length - 1);

        // Add a space before the final text
        textComponent.text += " ";

        // Delay before changing to the final text
        yield return new WaitForSeconds(1f);

        // Animate the letters of the final text
        for (int i = originalText.Length; i < finalText.Length; i++)
        {
            textComponent.text += finalText[i];
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
