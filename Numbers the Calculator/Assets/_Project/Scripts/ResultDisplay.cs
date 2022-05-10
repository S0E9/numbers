using System;
using System.Linq;
using UnityEngine;
using static NumbersTheCalculator.Enums;

namespace NumbersTheCalculator
{
    internal class ResultDisplay: MonoBehaviour
    {
        [SerializeField]
        public GameObject[] displayElements;
        private Calculator _calculator;
        private bool _isActive;
        private MeshFilter _meshFilter;
        public bool readyForDigit;

        private void Awake()
        {
            _calculator = FindObjectOfType<Calculator>();
            SetPlaceValue();
        }
        public void SetPlaceValue()
        {
            string resultString = _calculator.inputField.text;
            int startIndex = resultString.Length;
            foreach (GameObject go in displayElements)
            {
                startIndex--;
                _meshFilter = go.GetComponent<MeshFilter>();
                if (startIndex > 0)
                {
                    _meshFilter.mesh = _calculator.legendMeshes[int.Parse(resultString.Substring(startIndex, 1))];
                }
                else
                {
                    _meshFilter.mesh = _calculator.legendMeshes[10];
                }
            }
        }
    }
}
