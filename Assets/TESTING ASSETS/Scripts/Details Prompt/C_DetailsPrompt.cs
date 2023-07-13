using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class C_DetailsPrompt : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public Image descriptionImage;
    public TextMeshProUGUI promptTitle;

    [SerializeField] private Button backButton;

    [SerializeField] private AudioSource buttonSFX;

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (!descriptionText)
        {
            Debug.LogError("Please fill text description in the inspector!"); ;
        }
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            buttonSFX.Play();
            StartCoroutine(Terminate());
        });
    }

    public IEnumerator Terminate()
    {
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(1);
    }
}
