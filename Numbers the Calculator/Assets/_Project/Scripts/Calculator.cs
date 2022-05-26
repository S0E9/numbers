using UnityEngine;
using System.Linq;
using TMPro;
using static NumbersTheCalculator.Enums;
using System;

namespace NumbersTheCalculator
{
    public class Calculator : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _inputField;
        [SerializeField]
        private Mesh[] _legendMeshes;
        [SerializeField]
        private Mesh[] _capMeshes;
        [SerializeField]
        private Keyswitch _keyswitch;
        [SerializeField]
        private Color _mainColor;
        [SerializeField]
        private Color _altColor;
        private Material _keyswitchMaterial;
        private Keyswitch[] _keyswitchArray;
        private ResultDisplay _resultDisplay;
        private Color _keyswitchColor;

        private CalculatorState _calcState;
        private double? _input1;
        private double? _input2;
        private string _operator;
        private double? _result;
        [SerializeField]
        private bool _hasDecimal;
        private bool _isNegative;
        private bool _inputEnabled;
        public bool calcError;
        public int? digitValue;
        public string fullNumber;
        public int decimalIndex;

        private void Awake()
        {
            _keyswitchArray = new Keyswitch[17];
            _inputField.text = "";
            _result = null;
            _inputEnabled = true;
            _resultDisplay = FindObjectOfType<ResultDisplay>();
            _calcState = CalculatorState.WaitingFirst;
            SetKeyswitches();
        }
        private void SetKeyswitches()
        {
            int[] linebreak = { 1, 5, 8, 12 };
            int[] altColor = { 0, 5, 12, 13, 14, 15, 16 };
            Vector3 keySpawnPosition = new Vector3(-3.5f, -2.5f, -7.8f);
            float hShift = 2.35f;
            float vShift = 2f;

            for (int i = 0; i < _keyswitchArray.Length; i++)
            {
                bool isLinebreak = linebreak.Contains(i);
                bool isAltColor = altColor.Contains(i);
                
                Keyswitch newKeyswitch = Instantiate(_keyswitch, keySpawnPosition, Quaternion.identity, transform);
                _keyswitchMaterial = newKeyswitch.GetComponent<MeshRenderer>().material;
                newKeyswitch.SetKeyValue((KeyValue)i);
                newKeyswitch.name = i.ToString() + " Key";
                // Adjust Long & Tall Keys
                if (i == 0)
                {
                    keySpawnPosition.x += hShift;
                }
                if (i is 5 or 12)
                {
                    newKeyswitch.transform.position -= new Vector3 (0f,0f, vShift);
                }
                keySpawnPosition.x += hShift;
                if (isLinebreak == true)
                {
                    keySpawnPosition.z += vShift;
                    keySpawnPosition.x = -3.5f;
                }
                if(isAltColor == true)
                {
                    _keyswitchColor = _altColor;
                }
                if(isAltColor == false)
                {
                    _keyswitchColor = _mainColor;
                }
                _keyswitchMaterial.color = _keyswitchColor;
            }
        }
        public void EnterValue(KeyValue keyvalue)
        {
            int digit;            
            bool inputIsValid = (_inputField.text != "." && _inputField.text != "" && _inputField.text != "-");
            bool hasResult = !_result.Equals(null);
            _isNegative = _inputField.text.Contains("-");
            _hasDecimal = _inputField.text.Contains(".");

            switch (keyvalue)
            {
                case KeyValue.Add:
                    if (inputIsValid)
                    {
                        if (_calcState == CalculatorState.WaitingSecond)
                        {
                            _input2 = double.Parse(_inputField.text);
                            Calculate(_input1, _input2, _operator);
                        }
                        _operator = TryOperator("+");
                    }                    
                    break;
                case KeyValue.Subtract:
                    switch (_calcState)
                    {
                        case CalculatorState.WaitingFirst:
                            if (!inputIsValid)
                            {
                                _inputField.text = _inputField.text == "-" ? "" : "-";
                                _resultDisplay.SetNegative(!_isNegative);
                            }
                            else
                            {
                                _operator = TryOperator("-");
                            }
                            break;
                        case CalculatorState.ValidFirst: // SOMETHING WRONG HERE
                            inputField.text = "";
                            _inputField.text = _inputField.text == "-" ? "" : "-";
                            _inputEnabled = true;
                            _resultDisplay.SetNegative(_inputField.text.Contains("-"));
                            _resultDisplay.hasDecimal = false;
                            _calcState = CalculatorState.WaitingSecond;
                            break;
                        case CalculatorState.WaitingSecond: // SOMETHING WRONG HERE
                            if (inputIsValid)
                            {
                                _input2 = double.Parse(_inputField.text);
                                Calculate(_input1, _input2, _operator);
                                _operator = TryOperator("-");
                            }
                            else
                            {
                                _inputField.text = _inputField.text == "-" ? "" : "-";
                                _resultDisplay.SetNegative(!_isNegative);                                 
                            }                            
                            break;
                        default:
                            calcError = true;
                            Debug.Log("WAT!?");
                            break;
                    }
                    break;
                case KeyValue.Multiply:
                    if(inputIsValid)
                    {
                        if (_calcState == CalculatorState.WaitingSecond)
                        {
                            _input2 = double.Parse(_inputField.text);
                            Calculate(_input1, _input2, _operator);
                        }
                        _operator = TryOperator("*");
                    }
                    break;
                case KeyValue.Clear:
                    Clear();
                    break;
                case KeyValue.Divide:
                    if (inputIsValid)
                    {
                        if (_calcState == CalculatorState.WaitingSecond)
                        {
                            _input2 = double.Parse(_inputField.text);
                            Calculate(_input1, _input2, _operator);
                        }
                        _operator = TryOperator("/");
                    }
                    break;
                case KeyValue.Equals:
                    if (_calcState == CalculatorState.WaitingSecond)
                    {
                        _input2 = double.Parse(_inputField.text);
                        Calculate(_input1, _input2, _operator);
                    }
                    break;
                case KeyValue.DecimalPoint:
                    if (!_hasDecimal)
                    {
                        if (_calcState == CalculatorState.ValidFirst)
                        {
                            _inputEnabled = true;
                            _inputField.text = "";
                            _resultDisplay.hasDecimal = true;
                            _inputField.text = ".";
                            _calcState = CalculatorState.WaitingSecond;
                        }
                        _resultDisplay.hasDecimal = true;
                        _inputField.text += ".";
                    }
                    if (_calcState == CalculatorState.WaitingFirst && hasResult)
                    {
                            _result = null;
                            _inputField.text = "";
                            _resultDisplay.hasDecimal = true;
                            _inputField.text = ".";
                    }
                    if (_hasDecimal && _calcState == CalculatorState.ValidFirst)
                    {
                        _inputEnabled = true;
                        _inputField.text = "";
                        _resultDisplay.hasDecimal = true;
                        _inputField.text = ".";
                        _calcState = CalculatorState.WaitingSecond;
                    }
                    break;
                default: // digit = -1 on first row, -2 on other two
                    if ((int)keyvalue < 5 && (int)keyvalue != 1)
                    {
                        digit = (int)keyvalue - 1;
                        if(keyvalue == 0)
                        {
                            digit = 0;
                        }
                    }
                    else
                    {
                        digit = (int)keyvalue - 2;
                    }
                    switch (_calcState)
                    {
                        case CalculatorState.WaitingFirst:
                            if (_inputField.text != "0" && _inputEnabled)
                            {
                                _inputField.text += $"{digit}";
                            }
                            if (hasResult)
                            {
                                _result = null;
                                _inputField.text = "";
                                _resultDisplay.SetNegative(false);
                                _resultDisplay.hasDecimal = false;
                                _inputField.text += $"{digit}";
                            }
                            break;
                        case CalculatorState.ValidFirst:
                            _inputField.text = "";
                            _resultDisplay.SetNegative(false);
                            _resultDisplay.hasDecimal = false;
                            _inputField.text += $"{digit}";
                            _inputEnabled = true;
                            _calcState = CalculatorState.WaitingSecond;
                            break;
                        case CalculatorState.WaitingSecond:
                            if (_inputField.text != "0" && _inputEnabled)
                            {
                                _inputField.text += $"{digit}";
                            }                            
                            break;
                        default:
                            calcError = true;
                            Debug.Log("WAT!?");
                            break;
                    }
                    break;
            }
            CheckInputLimit();
            fullNumber = _inputField.text.Replace(".", string.Empty);
            _resultDisplay.SetPlaceValue();
            switch (_calcState)
            {
                case CalculatorState.WaitingFirst:
                    _keyswitchMaterial.color = Color.red;
                    break;
                case CalculatorState.ValidFirst:
                    _keyswitchMaterial.color = Color.green;
                    Debug.Log(_input1);
                    break;
                case CalculatorState.WaitingSecond:
                    _keyswitchMaterial.color = Color.blue;
                    break;
                default:
                    Debug.Log("NM");
                    break;
            }
        }
        private string TryOperator(string operation)
        {
            if (_calcState == CalculatorState.WaitingFirst || _calcState == CalculatorState.ValidFirst)
            {
                switch (operation)
                {
                    case "+":
                        operation = "+";
                        break;
                    case "-":
                        operation = "-";
                        break;
                    case "*":
                        operation = "*";
                        break;
                    case "/":
                        operation = "/";
                        break;
                    default:
                        operation = "";
                        break;
                }
                _input1 = double.Parse(_inputField.text);
                _inputEnabled = true;
                _calcState = CalculatorState.ValidFirst;
                return operation;
            }
            else
            {
                return "";
            }
        }
        private void Clear()
        {
            if (_calcState == CalculatorState.WaitingSecond && _inputField.text != "" && !calcError)
            {
                _inputField.text = "";
                _resultDisplay.hasDecimal = _hasDecimal = _isNegative = false;
                _calcState = CalculatorState.ValidFirst;
            }
            else
            {
                _inputField.text = _operator = "";
                _result = _input1 = _input2 = null;
                _resultDisplay.hasDecimal = false;
                _isNegative = false;
                _calcState = CalculatorState.WaitingFirst;
            }
            calcError = false;
            _inputEnabled = true;
            _isNegative = _inputField.text.Contains("-");
            _resultDisplay.SetNegative(_isNegative);
        }
        private void Calculate(double? input1, double? input2, string op)
        {
            double maxValue = 9999999999;
            double minValue = -maxValue;
            switch (op)
            {
                case "+":
                    _result = input1 + input2;
                    break;
                case "-":
                    _result = input1 - input2;
                    break;
                case "*":
                    _result = input1 * input2;
                    break;
                case "/":
                    _result = input1 / input2;
                    break;
                default:
                    _result = null;
                    calcError = true;
                    Debug.Log("BORK");
                    break;
            }
           if (_result > maxValue)
           {
                _result = maxValue;
                calcError = true;
           }
           else if (_result < minValue)
           {
                _result = minValue;
                calcError = true;
           }
           else
           {
                CheckOutputLimit();
                _input1 = _result;
                _input2 = null;
                _operator = null;
                _inputEnabled = true;
                if (_hasDecimal)
                {
                    _resultDisplay.hasDecimal = true;
                }
                else
                {
                    _resultDisplay.hasDecimal = false;
                }
                _isNegative = _result < 0 ? true : false;
                _resultDisplay.SetNegative(_isNegative);
                _calcState = CalculatorState.WaitingFirst;
           }
           _inputField.text = _result.ToString();
            if (_result % 1 == 0)
            {
                _resultDisplay.hasDecimal = false;
            }
            else
            {
                _resultDisplay.hasDecimal = true;
            }
        }
        private void CheckInputLimit()
        {
            int maxLength;
            if (_hasDecimal && _isNegative)
            {
                maxLength = 12;
            }
            else if (_hasDecimal || _isNegative)
            {
                maxLength = 11;
            }
            else
            {
                maxLength = 10;
            }

            if (_inputField.text.Length >= maxLength)
            {
                _inputEnabled = false;
            }
        }
        private void CheckOutputLimit()
        {
            int maxLength = 10;
            decimal result;
            decimal wholeNumber;
            decimal roundedResult;

            if (_result % 1 != 0)
            {
                result = (decimal)_result;
                wholeNumber = Math.Round(result);
                roundedResult = Math.Round(result, maxLength - wholeNumber.ToString().Length);
                _result = (double?)roundedResult;
            }
        }
        public Mesh[] legendMeshes { get => _legendMeshes; }
        public Mesh[] capMeshes { get => _capMeshes; }
        public TMP_InputField inputField { get => _inputField; }
    }
}
