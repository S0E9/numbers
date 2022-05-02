using UnityEngine;
using System.Linq;
using TMPro;
using static NumbersTheCalculator.Enums;

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
        private Color _keyswitchColor;

        private CalculatorState _calcState;
        private double _input1;
        private double _input2;
        private string _operator;
        private double _result;
        private ResultDisplay _resultDisplay;

        private void Awake()
        {
            _keyswitchArray = new Keyswitch[17];
            _inputField.text = "";
            _calcState = CalculatorState.WaitingFirst;
            SetKeyswitches();
        }
        private void SetKeyswitches()
        {
            int[] linebreak = { 1, 5, 8, 12 };
            int[] altColor = { 0, 5, 12, 13, 14, 15, 16 };
            Vector3 keySpawnPosition = new Vector3(-3.5f, 1f, -7.8f);
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
            bool hasDecimal = _inputField.text.Contains(".");
            bool isNegative = _inputField.text.Contains("-");
            bool inputIsValid = (_inputField.text != "." && _inputField.text != "" && _inputField.text != "-");

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
                    if (!inputIsValid)
                    {
                        if (!isNegative)
                        {
                            _inputField.text += "-";
                        }    
                        else
                        {
                            _inputField.text = "";
                        }
                    }
                    else
                    {
                        if (_calcState == CalculatorState.WaitingSecond)
                        {
                            _input2 = double.Parse(_inputField.text);
                            Calculate(_input1, _input2, _operator);
                        }
                        _operator = TryOperator("-");
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
                    if (!hasDecimal)
                    {
                        _inputField.text += ".";
                    }
                    break;
                case KeyValue.Zero:
                    if (_inputField.text == "0")
                    {
                        return;
                    }
                    else
                    {
                        _inputField.text += "0";
                    }
                    break;
                default: // digit = -1 on first row, -2 on other two
                    if ((int)keyvalue < 5 && (int)keyvalue > 1)
                    {
                        digit = (int)keyvalue - 1;
                    }
                    else
                    {
                        digit = (int)keyvalue - 2;
                    }
                    switch (_calcState)
                    {
                        case CalculatorState.WaitingFirst:
                            _inputField.text += $"{digit}";
                            break;
                        case CalculatorState.ValidFirst:
                            _inputField.text = "";
                            _inputField.text = digit.ToString();
                            _calcState = CalculatorState.WaitingSecond;
                            break;
                        case CalculatorState.WaitingSecond:
                            _inputField.text += $"{digit}";
                            break;
                        case CalculatorState.ValidSecond:
                            Clear();
                            break;
                        default:
                            _inputField.text = "E";
                            Debug.Log("WAT!?");
                            break;
                    }
                    break;
            }
            switch (_calcState)
            {
                case CalculatorState.WaitingFirst:
                    _keyswitchMaterial.color = Color.red;
                    break;
                case CalculatorState.ValidFirst:
                    _keyswitchMaterial.color = Color.blue;
                    break;
                case CalculatorState.WaitingSecond:
                    _keyswitchMaterial.color = Color.green;
                    break;
                case CalculatorState.ValidSecond:
                    _keyswitchMaterial.color = Color.yellow;
                    break;
                default:
                    _keyswitchMaterial.color = _keyswitchColor;
                    break;
            }
        }
        private string TryOperator(string operation)
        {
            if ((_calcState == CalculatorState.WaitingFirst || _calcState == CalculatorState.ValidFirst))
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
            if (_calcState == CalculatorState.WaitingSecond && _inputField.text != "")
            {
                _inputField.text = "";
                _calcState = CalculatorState.ValidFirst;
            }
            else
            {
                _inputField.text = _operator = null;
                _input1 = _input2 = _result = 0;
                _calcState = CalculatorState.WaitingFirst;
            }
        }
        private void Calculate(double input1, double input2, string op)
        {
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
                    _result = 0;
                    _inputField.text = "E";
                    Debug.Log("BORK");
                    break;
            }
            _inputField.text = _result.ToString();
            _input1 = _result;
            _input2 = 0;
            _operator = null;
            _calcState = CalculatorState.WaitingFirst;
        }
        private void DisplayResult() // to be implimented
        {

        }
        public Mesh[] legendMeshes { get => _legendMeshes; }
        public Mesh[] capMeshes { get => _capMeshes; }
    }
}
