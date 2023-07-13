using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeExpress
{
    public class B_ImagePrompt : B_BasicEvent
    {
        [SerializeField] private C_ImagePrompt canvasImagePrompt;

        [SerializeField] private string promptTitle;
        [SerializeField] private Sprite[] images;
        [SerializeField] private Button[] descriptionButtons;

        [Header("Please fill if you use Details Prompt!")]
        [SerializeField] private C_DetailsPrompt detailsPrompt;
        [SerializeField] private string[] descriptionText;

        [SerializeField] private bool useDetailsPrompt = true;


        public override IEnumerator Flow_Initiate()
        {
            return base.Flow_Initiate();
        }

        public override IEnumerator Flow_EventSequence()
        {
            if (useDetailsPrompt)
            {
                canvasImagePrompt.InitiateWithDetailsPrompt(promptTitle, images, descriptionText);                
            }
            else
            {
                canvasImagePrompt.InitiateWithoutDetailsPrompt(promptTitle, images);
            }

            yield return new WaitUntil(() => canvasImagePrompt.closing);
        }

        public override IEnumerator Flow_Terminate()
        {
            canvasImagePrompt.Terminate();
            yield return new WaitForSeconds(1);
            canvasImagePrompt.gameObject.SetActive(false);
        }
    }
}