using System;
using System.Collections;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    public class Gate : MonoBehaviour
    {
        [Header("Preferences")] [SerializeField]
        private float closedHeight = 3f;
        [SerializeField] private float secondsRequiredToCloseOrOpenGate = default;
        [Header("References")] [SerializeField]
        private Transform gatePivot = default;

        private GateState state = GateState.Closed;

        private float normalizedTime;

        public void OpenGate()
        {
            if (state == GateState.Transitioning || state == GateState.Open) return;

            normalizedTime = 0f;
            
            state = GateState.Transitioning;

            StartCoroutine(GateOpenCoroutine());
        }

        public void CloseGate()
        {
            if (state == GateState.Transitioning || state == GateState.Closed) return;

            normalizedTime = 0f;
            
            state = GateState.Transitioning;

            StartCoroutine(GateCloseCoroutine());
        }

        private IEnumerator GateOpenCoroutine()
        {
            var timerNotDone = normalizedTime <= 1f;
            
            while (timerNotDone)
            {
                var yScale = Mathf.Lerp(closedHeight, 0f, normalizedTime);
                var localScale = gatePivot.localScale;

                gatePivot.localScale = new Vector3(localScale.x, yScale, localScale.z);

                normalizedTime += Time.deltaTime / secondsRequiredToCloseOrOpenGate;
                timerNotDone = normalizedTime <= 1f;
                
                yield return null;
            }

            state = GateState.Open;
        }

        private IEnumerator GateCloseCoroutine()
        {
            var timerNotDone = normalizedTime <= 1f;
            
            while (timerNotDone)
            {
                var yScale = Mathf.Lerp(0f, closedHeight, normalizedTime);
                var localScale = gatePivot.localScale;

                gatePivot.localScale = new Vector3(localScale.x, yScale, localScale.z);

                normalizedTime += Time.deltaTime / secondsRequiredToCloseOrOpenGate;
                timerNotDone = normalizedTime <= 1f;
                
                yield return null;
            }

            state = GateState.Closed;
        }

#if UNITY_EDITOR
        [ContextMenu("Set ClosedHeight to pivot scale y")]
        public void ResetClosedHeightToScale() => closedHeight = gatePivot.localScale.y;

        [ContextMenu("Test Open")]
        public void TestOpen() => OpenGate();

        [ContextMenu("Text Close")]
        public void TestClose() => CloseGate();
#endif

        private enum GateState
        {
            Closed,
            Transitioning,
            Open
        }
    }
}