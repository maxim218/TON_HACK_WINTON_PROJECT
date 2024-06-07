using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FruitCatcher
{
    public class BeforeGameStartManager : MonoBehaviour
    {
        private const float DelayTime = 0.7f;

        [Header("Label for Ready Steady Go")] [SerializeField]
        private Text label = null;

        [Header("Block with label")] [SerializeField]
        private GameObject block = null;

        [Header("On finish animation")] [SerializeField]
        private UnityEvent OnFinishAnimation = null;

        private void Start()
        {
            StartCoroutine(AsyncReadySteadyGoRun());
        }

        private IEnumerator AsyncReadySteadyGoRun()
        {
            label.text = "Ready";
            yield return new WaitForSeconds(DelayTime);
            label.text = "Steady";
            yield return new WaitForSeconds(DelayTime);
            label.text = "Go Go Go";
            yield return new WaitForSeconds(DelayTime);
            block.SetActive(false);
            OnFinishAnimation?.Invoke();
        }
    }
}