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
        private ALU _alu;
        private bool _inputEnabled;

        private void Awake()
        {
            _keyswitchArray = new Keyswitch[17];
            _inputField.text = "";
            _alu = new ALU();
            _alu.result = null;
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
                if (i == 0)
                    keySpawnPosition.x += hShift;
                if (i is 5 or 12)
                    newKeyswitch.transform.position -= new Vector3(0f, 0f, vShift);

                keySpawnPosition.x += hShift;
                if (isLinebreak == true)
                {
                    keySpawnPosition.z += vShift;
                    keySpawnPosition.x = -3.5f;
                }
                _keyswitchColor = isAltColor ? _altColor : _mainColor;
                _keyswitchMaterial.color = _keyswitchColor;
            }
        }
        public void EnterValue(KeyValue keyvalue)
        {
            keyvalue = keyvalue switch
            {
                KeyValue.Add => CheckOperation(keyvalue),
                KeyValue.Subtract => CheckMinus(),
                KeyValue.Multiply => CheckOperation(keyvalue),
                KeyValue.Clear => Clear(),
                KeyValue.Divide => CheckOperation(keyvalue),
                KeyValue.DecimalPoint => CheckDecimal(),
                KeyValue.Equals => CheckCalculate(),
                _ => EnterDigit(keyvalue)
            };
            ValidateInput();
        }
        private KeyValue CheckOperation(KeyValue op)
        {
            if (_inputField.text.Any(char.IsDigit))
            {
                if (_calcState == CalculatorState.WaitingSecond && _alu.currentOp != null)
                    SendCalc();

                _alu.currentOp = _alu.CurrentOperator(op);
                if (_calcState != CalculatorState.WaitingSecond)
                {
                    _alu.input1 = _alu.isNegative ? double.Parse(_inputField.text) * -1 : double.Parse(_inputField.text);
                    if (_inputField.text.Contains("-"))
                        _alu.input1 = _alu.input1 * -1;
                    _inputEnabled = true;
                    _alu.result = null;
                    _calcState = CalculatorState.ValidFirst;
                }
                else
                    _alu.currentOp = "";
            }
            if (_alu.input1 != null)
                _alu.currentOp = _alu.CurrentOperator(op);

            return op;
        }
        private KeyValue CheckMinus()
        {
            switch (_calcState)
            {
                case CalculatorState.WaitingFirst:
                case CalculatorState.WaitingSecond:
                    if (!_inputField.text.Any(char.IsDigit))
                        _alu.isNegative = !_alu.isNegative;
                    else
                        CheckOperation(KeyValue.Subtract);
                    break;
                case CalculatorState.ValidFirst:
                    _inputField.text = "";
                    _alu.isNegative = true;
                    _inputEnabled = true;
                    _calcState = CalculatorState.WaitingSecond;
                    break;
            }
            return KeyValue.Subtract;
        }
        private KeyValue Clear()
        {
            if (_calcState == CalculatorState.WaitingSecond && _inputField.text != "" && !_alu.CalcError())
            {
                _inputField.text = "";
                _alu.hasDecimal = _alu.isNegative = false;
                _calcState = CalculatorState.ValidFirst;
            }
            else
            {
                _inputField.text = _alu.currentOp = "";
                _alu.result = _alu.input1 = _alu.input2 = null;
                _alu.isNegative = _alu.hasDecimal = false;
                _calcState = CalculatorState.WaitingFirst;
            }
            _alu.CalcError();
            _inputEnabled = true;
            return KeyValue.Clear;
        }
        private KeyValue CheckCalculate()
        {
            if (_calcState == CalculatorState.WaitingSecond && _inputField.text.Any(char.IsDigit))
                SendCalc();
            return KeyValue.Equals;
        }
        private KeyValue EnterDigit(KeyValue keyvalue)
        {
            bool readyForNegative = _alu.isNegative && !_inputField.text.Any(char.IsDigit);
            int digit = (int)keyvalue switch
            {
                0 => 0,
                1 => 1,
                < 5 => (int)keyvalue - 1,
                _ => (int)keyvalue - 2
            };
            switch (_calcState)
            {
                case CalculatorState.WaitingFirst:
                    if (_inputField.text != "0" && _inputEnabled)
                    {
                        if (_alu.result != null)
                        {
                            _alu.result = null;
                            _alu.isNegative = false;
                            inputField.text = "";
                        }
                        _inputField.text += $"{digit}";
                    }
                    break;
                case CalculatorState.ValidFirst:
                    _alu.isNegative = readyForNegative;
                    _inputField.text = "";
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
            }
            return keyvalue;
        }
        private KeyValue CheckDecimal()
        {
            switch (_calcState)
            {
                case CalculatorState.WaitingFirst:
                case CalculatorState.WaitingSecond:
                    if (!_alu.hasDecimal)
                        _inputField.text += ".";
                    break;
                case CalculatorState.ValidFirst:
                    _inputField.text = "";
                    _inputField.text += ".";
                    _inputEnabled = true;
                    _calcState = CalculatorState.WaitingSecond;
                    break;
            }
            return KeyValue.DecimalPoint;
        }
        private void SendCalc()
        {
            _alu.input2 = _alu.isNegative ? double.Parse(_inputField.text) * -1 : double.Parse(_inputField.text);
            _alu.Calculate(_alu.input1, _alu.input2, _alu.currentOp);
            if (!_alu.CalcError())
            {
                _inputEnabled = true;
                _alu.isNegative = _alu.result < 0 ? true : false;
                _calcState = CalculatorState.WaitingFirst;
            }
            _inputField.text = _alu.result.ToString();
            _alu.input1 = _alu.result;
            _alu.input2 = null;
        }        
        private void ValidateInput()
        {
            _alu.hasDecimal = inputField.text.Contains(".");
            int maxLength = _alu.hasDecimal ? 11 : 10;
            if (_inputField.text.Length >= maxLength)
                _inputEnabled = false;

            _resultDisplay.SetNegative(_alu.isNegative);
            _resultDisplay.SetPlaceValue();
        }
        public Mesh[] legendMeshes { get => _legendMeshes; }
        public Mesh[] capMeshes { get => _capMeshes; }
        public TMP_InputField inputField { get => _inputField; }
        public ALU alu { get => _alu; }
    }
}