using System.Collections;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    /// <summary>
    /// Gate behaviour implemented through coroutines
    /// </summary>
    public class Gate : MonoBehaviour
    {
        #region fields
        [Header("Preferences")] 
        [SerializeField] private float closedHeight = 3f;
        
        /// <summary>
        /// Set this parameter to 0 if you want the gate to "disappear" once it's fully open
        /// Anything else if you want it simply look as if it were "lowered"
        /// </summary>
        [SerializeField] private float openHeight = 0.1f;
        
        [SerializeField] private float secondsRequiredToCloseOrOpenGate = default;
        
        [Header("References")] 
        [SerializeField] private Transform gatePivot = default;
        #endregion
        
        private GateState state = GateState.Closed;

        /// <summary>
        /// NormalizedTime is kept between 0 and 1, this allows it to be used easily with
        /// Lerp methods
        /// </summary>
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
                var yScale = Mathf.Lerp(closedHeight, openHeight, normalizedTime);
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
                var yScale = Mathf.Lerp(openHeight, closedHeight, normalizedTime);
                var localScale = gatePivot.localScale;

                gatePivot.localScale = new Vector3(localScale.x, yScale, localScale.z);

                normalizedTime += Time.deltaTime / secondsRequiredToCloseOrOpenGate;
                timerNotDone = normalizedTime <= 1f;
                
                yield return null;
            }

            state = GateState.Closed;
        }

        #region editor methods
#if UNITY_EDITOR
        [ContextMenu("Set ClosedHeight to pivot scale y")]
        public void ResetClosedHeightToScale() => closedHeight = gatePivot.localScale.y;

        [ContextMenu("Test Open")]
        public void TestOpen() => OpenGate();

        [ContextMenu("Text Close")]
        public void TestClose() => CloseGate();
#endif
        #endregion

        private enum GateState
        {
            Closed,
            Transitioning,
            Open
        }
    }
}