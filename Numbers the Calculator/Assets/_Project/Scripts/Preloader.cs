using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NumbersTheCalculator
{
    public class Preloader : MonoBehaviour
    {
        private void Awake()
        {
            LoadCalculator(1);
        }
        public void LoadCalculator(int index)
        {
            StartCoroutine(LoadAsync(index));
        }

        private IEnumerator LoadAsync(int index)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(index);

            while (!loading.isDone)
            {
                float progress = Mathf.Clamp01(loading.progress / .9f);
                Debug.Log(progress);
                yield return null;
            }
        }
    }
}
