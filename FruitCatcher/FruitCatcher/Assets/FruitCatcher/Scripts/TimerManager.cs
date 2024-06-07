using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FruitCatcher
{
    public class TimerManager : MonoBehaviour
    {
        [Header("Time seconds")] [SerializeField]
        private int seconds = 0;

        private Text _timerLabel = null;
        private Action _onFinishCounting = null;

        public void InitTimerParams(Text timerLabel, Action onFinishAction)
        {
            _timerLabel = timerLabel;
            _onFinishCounting = onFinishAction;
        }

        public void BeginTimerCounting() => StartCoroutine(AsyncTimerCounting());

        private IEnumerator AsyncTimerCounting()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                seconds -= 1;
                _timerLabel.text = $"{seconds}";
                if (seconds <= 0)
                {
                    _onFinishCounting?.Invoke();
                    yield break;
                }
            }
        }
    }
}