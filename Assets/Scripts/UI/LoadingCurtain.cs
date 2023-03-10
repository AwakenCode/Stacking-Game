using System.Collections;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        private CanvasGroup _curtain;

        private void Awake()
        {
            _curtain = GetComponent<CanvasGroup>();

            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1.0f;
        }

        public void Hide()
        {
            StartCoroutine(DoFadeIn());
        }

        private IEnumerator DoFadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}