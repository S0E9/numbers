using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersTheCalculator
{
    public class Enums
    {
        public enum CalculatorState
        {
            WaitingFirst, ValidFirst, WaitingSecond, ValidSecond
        }
        public enum DigitPlace // this might just be for the display class, maybe remove comma?
        {
            Ones, Tens, Hundreds,
            Thousands, TenThousands, HundredThousands,
            Millions, TenMillions, HundredMillions,
            Billions, Comma, Decimal
        };
        public enum KeyValue
        {
            Zero, DecimalPoint, One, Two, Three, Equals, Four, Five, Six,
            Seven, Eight, Nine, Add, Clear, Divide, Multiply, Subtract
        };
        public enum KeyStyle
        {
            Long, Tall, Default
        };
    }
}
