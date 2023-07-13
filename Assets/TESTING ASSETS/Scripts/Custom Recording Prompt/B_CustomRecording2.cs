using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using PrimeTools;

namespace PrimeExpress
{
    public class B_CustomRecording2 : B_BasicEvent
    {
        [Header("Initiate Options")]
        public string title;
        public string subTitle;
        public float timeLimit;

        [Header("External Reference")]
        public C_CustomRecording2 _vgrScript;

        [Header("Optional if Want Gesture Record")]
        //public Player_Root playerRoot;
        [SerializeField] private Prime_IK_RecorderFull ik_recorder;

        public static List<AudioClip> recordContainer = new List<AudioClip>();

        // List Of Device
        public List<InputDevice> devices = new List<InputDevice>();

        public static void ClearRecordContainer()
        {
            recordContainer.Clear();
        }

        public override IEnumerator Flow_Initiate()
        {
            if (onInitiate != null)
            {
                onInitiate.Invoke();
            }

            InputDevices.GetDevices(devices);
            return base.Flow_Initiate();
        }

        public static void AddBlankRecording(AudioClip placeholderClip)
        {
            recordContainer.Add(placeholderClip);
            Debug.Log("Recording Dummy = " + placeholderClip.name);
        }

        public override IEnumerator Flow_EventSequence()
        {
            // IN this Project we dont use script Prime_IK_RecorderFull and Pvr_UnitySDKAPI
            //FindKeyComponents();

            // initiating canvas values
            _vgrScript.titleText = title;
            _vgrScript.subtitleText = subTitle;
            _vgrScript._recordTimer = timeLimit;

            // IN this Project we dont use script Prime_IK_RecorderFull and Pvr_UnitySDKAPI
            //initiate recording and record time
            /*if (ik_recorder) 
                Pvr_UnitySDKAPI.Sensor.UPvr_OptionalResetSensor(0, 0, 1); // reset player position first*/

            if (ik_recorder)
            {
                ResetPosition();
            }


            _vgrScript.gameObject.SetActive(true);
            float timeStart = Time.time;

            yield return new WaitUntil(() => _vgrScript._startRecordTimer == true);

            // IN this Project we dont use script Prime_IK_RecorderFull and Pvr_UnitySDKAPI
            if (!_vgrScript._recordStopImm)
            {
                if (ik_recorder) ik_recorder.StartRecord();
            }

            yield return new WaitUntil(() => _vgrScript._startRecordTimer == false);
            _vgrScript.stopRecord();

            // IN this Project we dont use script Prime_IK_RecorderFull and Pvr_UnitySDKAPI
            if (ik_recorder) ik_recorder.StopRecord();

            recordContainer.Add(_vgrScript.micInput.ReturnCopyClipRecord((Time.time - timeStart - _vgrScript.countdownTimer) + 0.5f));
            Debug.Log("Total recording now: " + recordContainer.Count);
            yield return new WaitForSeconds(2f);
        }

        public override IEnumerator Flow_Terminate()
        {
            if (onTerminate != null)
            {
                onTerminate.Invoke();
            }

            // EDIT JONATHAN WILLIAM
            _vgrScript.animator.SetTrigger("Hide");
            yield return new WaitForSeconds(1f);

            _vgrScript.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
        }

        private void FindKeyComponents()
        {
            // IN this Project we dont use script Prime_IK_RecorderFull and Pvr_UnitySDKAPI
            /*if (playerRoot)
            {          
                //  ik_recorder = playerRoot.GetItem<Prime_IK_RecorderFull>();
                //cs_mouthController = playerRoot.GetItem<cs_MouthController_fifi>();
            }*/
        }

        public void RemoveLastRecording()
        {
            if (recordContainer.Count > 0)
                recordContainer.RemoveAt(recordContainer.Count - 1);

            Debug.Log(string.Format("Last Recording Removed, leftover recording: {0}", recordContainer.Count));
        }

        private void ResetPosition()
        {
            devices[0].subsystem.TryRecenter();
        }
    }
}
