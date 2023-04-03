using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NumbersTheCalculator
{
    public class Preloader : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadingIndicator;
        [SerializeField]
        private float _rotationSpeed;
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
            Quaternion curentRotation = _loadingIndicator.transform.rotation;

            while (!loading.isDone)
            {
                float progress = Mathf.Clamp01(loading.progress / .9f);
                curentRotation.y += _rotationSpeed;
                Debug.Log(progress);
                yield return null;
            }
        }
    }
}
