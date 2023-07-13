using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PrimeExpress
{
    public class B_InformationPrompt : B_BasicEvent
    {
        [SerializeField] private C_InformationPrompt canvasInformationPrompt;

        [SerializeField] private string promptTitle;

        [SerializeField] private string[] mainButtonText;
        [SerializeField] private string[] contentTitle;
        [SerializeField] private string[] contentText;
        [SerializeField] private Sprite[] contentImage;

        private void Awake()
        {
            if (mainButtonText.Length > 10)
            {
                string[] new_options = new string[10];
                mainButtonText.CopyTo(new_options, 0);
                mainButtonText = new_options;
            }
        }

        public override IEnumerator Flow_Initiate()
        {
            return base.Flow_Initiate();
        }

        public override IEnumerator Flow_EventSequence()
        {
            canvasInformationPrompt.Initiate(promptTitle, mainButtonText, contentTitle, contentImage, contentText);
            yield return new WaitUntil(() => canvasInformationPrompt.closing == true);
        }

        public override IEnumerator Flow_Terminate()
        {
            canvasInformationPrompt.Terminate();
            yield return new WaitForSeconds(3.5f);
            canvasInformationPrompt.gameObject.SetActive(false);
        }
    }
}