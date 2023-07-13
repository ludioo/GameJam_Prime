using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrimeExpress
{
    public class B_CustomAudioPlayback2 : B_BasicEvent
    {
        public C_CustomAudioPlayback2 canvas_playback;

        public override IEnumerator Flow_Initiate()
        {
            // if canvas playback isn't assign, try to assign automatically
            // if none is found then log error
            if (!canvas_playback) canvas_playback = FindObjectOfType<C_CustomAudioPlayback2>();
            if (!canvas_playback) Debug.LogError("No QTA_Canvas_AudioPlayback exist on the scene. PLEASE ASSIGN!");
            yield return null;
        }

        public override IEnumerator Flow_EventSequence()
        {
            // find audio clip to be played
            if (B_CustomRecording2.recordContainer != null && B_CustomRecording2.recordContainer.Count > 0)
            {
                foreach (var clip in B_CustomRecording2.recordContainer)
                {
                    // playback the clip assign to be played
                    canvas_playback.Initiate(clip);

                    // wait until it is done (when initiate is turned off)
                    // note that canvas playback is self terminate
                    yield return new WaitUntil(() => !canvas_playback.is_initiate);

                    // wait 1 second to play the next one
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            {
                // either record container has not been initialized or count is zero. Thus no audio to be played.
                Debug.LogWarning("No Audio Clip Exist or Recorded! Skipping Playback.");
                // exit elegantly
                yield break;
            }
        }

        public override IEnumerator Flow_Terminate()
        {
            // wait a few seconds before returning
            yield return new WaitForSeconds(1f);
        }
    }
}
