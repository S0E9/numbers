using System;
using System.Linq;
using UnityEngine;
using static NumbersTheCalculator.Enums;

namespace NumbersTheCalculator
{
    internal class ResultDisplay: MonoBehaviour
    {
        [SerializeField]
        private GameObject[] digitElements;
        [SerializeField]
        private GameObject[] decimalElements;
        [SerializeField]
        private GameObject[] commaElements;
        [SerializeField]
        private GameObject[] statusElements;
        private Calculator _calculator;
        private MeshFilter _meshFilter;
        private string _resultString;
        public bool hasDecimal;

        private void Awake()
        {
            _calculator = FindObjectOfType<Calculator>();
            SetNegative(false);
            SetPlaceValue();
        }
        public void SetPlaceValue()
        {
            _resultString = _calculator.inputField.text;
            SetDigits();
            
            if (hasDecimal)
            {
                SetDecimal();
            }
            else
            {
                foreach (GameObject go in decimalElements)
                {
                    SetActive(go, false);
                }
            }
            //SetComma();
            SetActive(statusElements[1], _calculator.calcError);
        }
        private void SetDigits()
        {
            string numberString = _calculator.fullNumber;
            int startIndex = numberString.Length;
            int digit;
            //int decimalPoint = 0;

            foreach (GameObject go in digitElements)
            {
                _meshFilter = go.GetComponent<MeshFilter>();
                if ((startIndex > 0 && !_resultString.Contains("-")) || (startIndex > 1 && _resultString.Contains("-")))
                {
                    startIndex--;
                    digit = int.Parse(numberString.Substring(startIndex, 1));
                    //Debug.Log(digit);
                    _meshFilter.mesh = _calculator.legendMeshes[CheckDigit(digit)];
                    SetActive(go, true);
                }
                else
                {
                    _meshFilter.mesh = _calculator.legendMeshes[10];
                    SetActive(go, false);
                }
            }
        }
        private void SetActive(GameObject go, bool isActive)
        {
            float activeHeight = -3.25f;
            float inactiveHeight = -3.65f;
            float digitHeight = isActive ? activeHeight : inactiveHeight;
            Vector3 digitPosition = new Vector3(go.transform.position.x, digitHeight, go.transform.position.z);
            go.transform.position = digitPosition;
        }
        private void SetDecimal()
        {
            _resultString = _calculator.inputField.text;
            string numberString = _calculator.fullNumber;
            string result = _resultString.Substring(_resultString.IndexOf('.'));
            int decimalIndex = result.Length - 1;
            if (_resultString == "..")
            {
                _resultString = ".";
            }
            foreach (GameObject go in decimalElements)
            {
                if (Array.IndexOf(decimalElements, go) == decimalIndex)
                {
                    SetActive(go, true);
                }
                else
                {
                    SetActive(go, false);
                }
            }
        }
        /*private void SetComma() TO BE IMPLIMENTED
        {
            int firstCommaIndex = -1;
            int SecondCommaIndex;
            int ThirdCommaIndex;
            int decimalLength = hasDecimal ? _resultString.Substring(_resultString.IndexOf('.')).Length - 1 : 0;
            if ( _resultString != null && float.Parse(_resultString) >= 1000)
            {
                firstCommaIndex = 0 + decimalLength;
                foreach (GameObject go in commaElements)
                {
                    if (Array.IndexOf(decimalElements, go) == firstCommaIndex)
                    {
                        SetActive(go, true);
                    }
                    else
                    {
                        SetActive(go, false);
                    }
                }
            }
        }*/
        public void SetNegative(bool isNegative)
        {
            SetActive(statusElements[0], isNegative);
        }
        private int CheckDigit(int digit)
        {
            int[] plusOne = { 1,2,3 };
            int[] plusTwo = { 4,5,6, 7, 8, 9 };
            if (plusOne.Contains(digit))
            {
                return digit + 1;
            }
            else if (plusTwo.Contains(digit))
            {
                return digit + 2;
            }
            else
            {
                return digit;
            }
        }
    }
}
