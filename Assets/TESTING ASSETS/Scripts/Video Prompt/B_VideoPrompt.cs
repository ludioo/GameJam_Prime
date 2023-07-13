using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeExpress
{
    public class B_VideoPrompt : B_BasicEvent
    {
        [SerializeField] private C_VideoPrompt canvasVideoPrompt;

        [SerializeField] private string promptTitle;
        [SerializeField] private Texture[] textures;
        [SerializeField] private RenderTexture[] renderTextures;

        public override IEnumerator Flow_Initiate()
        {
            return base.Flow_Initiate();
        }

        public override IEnumerator Flow_EventSequence()
        {
            canvasVideoPrompt.InitiateWithoutDetailsPrompt(promptTitle, textures, renderTextures);
            yield return new WaitUntil(() => canvasVideoPrompt.closing);
        }

        public override IEnumerator Flow_Terminate()
        {
            canvasVideoPrompt.Terminate();
            yield return new WaitForSeconds(1);
            canvasVideoPrompt.gameObject.SetActive(false);
        }
    }
}