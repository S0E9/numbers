using System;
using static NumbersTheCalculator.Enums;

namespace NumbersTheCalculator
{
    public class ALU
    {      
        private double? _input1;
        private double? _input2;
        private string _operator;
        private double? _result;
        private bool _isNegative;
        private bool _hasDecimal;

        public string CurrentOperator(KeyValue op)
        {
            string opString = op switch
            {
                KeyValue.Add => "+",
                KeyValue.Subtract => "-",
                KeyValue.Multiply => "*",
                KeyValue.Divide => "/",
                _ => ""
            };
            return opString;
        }
        public void Calculate(double? input1, double? input2, string op)
        {
            _result = op switch
            {
                "+" => _result = input1 + input2,
                "-" => _result = input1 - input2,
                "*" => _result = input1 * input2,
                "/" => _result = input1 / input2,
                _ => _result = null
            };
            _hasDecimal = _result % 1 != 0 ? true : false;
            _isNegative = _result < 0;
            if (!CalcError())
            {
                LimitOutput();
                _input1 = _result;
                _input2 = null;
                _operator = null;
            }
        }
        public bool CalcError()
        {
            const double maxValue = 9999999999;
            bool errorSent = _result switch
            {
                > maxValue => true,
                < -maxValue => true,
                _ => false
            };
            return errorSent;
        }
        private void LimitOutput()
        {
            int maxLength = 10;
            decimal result;
            decimal roundedResult;

            if (_result != null && _result % 1 != 0)
            {
                result = (decimal)_result;
                roundedResult = Math.Round(result, maxLength - Math.Round(result).ToString().Length);
                _result = (double?)roundedResult;
            }
        }
        public double? input1 { get => _input1; set => _input1 = value; }
        public double? input2 { get => _input2; set => _input2 = value; }
        public string currentOp { get => _operator; set => _operator = value; }
        public double? result { get => _result; set => _result = value; }
        public bool isNegative { get => _isNegative; set => _isNegative = value; }
        public bool hasDecimal { get => _hasDecimal; set => _hasDecimal = value; }
    }
}
