using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace PrimeExpress
{
    public class B_CustomGesture : B_BasicEvent
    {
        /* Conversion to Unity XR Notes:
         * Position debugging (SetPositionDebug()) now can be removed on runtime via external buttons 
         * since we can use Oculus Headset for Dev to move our hands to position. 
         * It is still accessible through Context Menu.
         * - Yoshua 2021-11-16
         */

        [Header("Targets")]
        public float threshold = 0.1f;
        public HandTarget left_target;
        public HandTarget right_target;

        [Header("QTA Stuff")]
        public string prompt = "Ikuti contoh Gesture";
        public float time_success = 2f;
        public float timeout = 30f;

        [Header("Canvas")]
        public C_CustomGesture canvas_gesture;
        List<HandTarget> hand_targets;
        float internal_countdown = 0f;
        float internal_successtime = 0f;

        [Header("Animation Settings")]
        public Transform target_parent;
        private Animator target_animator;
        public string bool_name = "B_Animate";


        [System.Serializable]
        public class HandTarget
        {
            public Transform vr_hand;
            public Transform target;
            public float magnitude = 0f;
            public bool in_range = false;
        }

        public override IEnumerator Flow_Initiate()
        {
            if (onInitiate != null)
                onInitiate.Invoke();
            // initiate the canvas
            canvas_gesture.Initiate(prompt, timeout);

            // get animator
            target_animator = target_parent.GetComponent<Animator>();

            // initiate the hand targets
            hand_targets = new List<HandTarget>();
            if (left_target.target != null) hand_targets.Add(left_target);
            if (right_target.target != null) hand_targets.Add(right_target);
            return base.Flow_Initiate();
        }

        public override IEnumerator Flow_EventSequence()
        {
            if (onEventSequence != null)
                onEventSequence.Invoke();
            // if there is nothing to track then return
            if (hand_targets.Count == 0)
                yield break;

            // enable target parent and all related target objects
            target_parent.gameObject.SetActive(true);
            foreach (var ht in hand_targets) ht.target.gameObject.SetActive(true);

            // update per frame regarding canvas
            internal_countdown = timeout;
            internal_successtime = 0f;
            yield return new WaitUntil(() => GetUpdateGesture());

            // disable the targets
            target_parent.gameObject.SetActive(false);
            foreach (var ht in hand_targets) ht.target.gameObject.SetActive(false);

            // if there is any time left then player is correct, set branch to 1 on correct, 0 otherwise
            bool is_correct = internal_countdown > 0f;
            branch = is_correct ? 1 : 0;

            // set canvas display accordingly
            if (is_correct)
                canvas_gesture.SetCanvasCorrect();
            else
                canvas_gesture.SetCanvasWrong();

            // wait 2 seconds before terminating
            yield return new WaitForSeconds(2f);
            canvas_gesture.Terminate();
        }

        public override IEnumerator Flow_Terminate()
        {
            if (onTerminate != null)
                onTerminate.Invoke();
            return base.Flow_Terminate();
        }

        bool all_in_range = false;
        public bool GetIsAllInRange { get => all_in_range; }
        public bool GetUpdateGesture()
        {
            all_in_range = true;

            // update average magnitude
            foreach (var ht in hand_targets)
            {
                ht.magnitude = (ht.vr_hand.position - ht.target.position).magnitude;
                //Debug.Log(ht.magnitude);
                ht.in_range = ht.magnitude < threshold;
                all_in_range = !ht.in_range ? false : all_in_range;
            }

            // update internal countdown
            internal_countdown -= Time.deltaTime;

            // if all hand is in range then count up, else reset
            if (all_in_range)
            {
                internal_successtime += Time.deltaTime;
                if (target_animator)
                    target_animator.SetBool(bool_name, true);
            }
            else
            {
                internal_successtime = 0f;
            }

            // update target animator if there is any
            if (target_animator)
                target_animator.SetBool(bool_name, all_in_range);

            // update the canvas accordingly
            UpdateCanvasTime();

            // return true if success time exceeds set time or countdown finish
            return (internal_successtime > time_success) || (internal_countdown <= 0f);
        }

        void UpdateCanvasTime()
        {
            canvas_gesture.UpdateGestureBar(internal_successtime / time_success);
            canvas_gesture.UpdateTimeBar(internal_countdown, internal_countdown / timeout);
        }

        [ContextMenu("SetPositionDebug")]
        void SetPositionDebug()
        {
            if (hand_targets != null)
            {
                foreach (var ht in hand_targets)
                {
                    ht.vr_hand.parent.position = ht.target.position;
                }
            }
        }
    }
}
