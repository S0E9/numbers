using UnityEngine;

namespace NumbersTheCalculator.Assets._Project.Scripts
{
    internal class ResultDisplay: MonoBehaviour
    {
        private float _result;
        private bool _isActive;
        private const float _ACTIVEHEIGHT = 1f;
        private const string _MESHNAMESTART = "SM_Icon_";
        private string _meshName;
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        [SerializeField]
        private enum digitPlace
        {
            Ones,      Tens,        Hundreds,
            Thousands, TenThousands,HundredThousands,
            Millions,  TenMillions, HundredMillions,
            Billions,  Comma,       Decimal
        };
        private enum numberValue
        {
            zero, one, two, three, four, five, six, seven, eight, nine, comma, decimalplace
        };
        private string _meshString()
        {
            string myValue = "";


            /*switch(numbers)
            {
                case numberValue.zero:
                    myValue = "test";
                    break;
                case numberValue.one:
                    myValue = "test";
                    break;
                case numberValue.two:
                    myValue = "test";
                    break;
                case numberValue.three:
                    myValue = "test";
                    break;
                case numberValue.four:
                    myValue = "test";
                    break;
                case numberValue.five:
                    myValue = "test";
                    break;
                case numberValue.six:
                    myValue = "test";
                    break;
                case numberValue.seven:
                    myValue = "test";
                    break;
                case numberValue.nine:
                    myValue = "test";
                    break;
                case numberValue.comma:
                    myValue = "test";
                    break;
                case numberValue.decimalplace:
                    myValue = "test";
                    break;
                default:
                    myValue= "test";
                    break;
            }*/
            return myValue;
        }
        [SerializeField]
        private digitPlace _myPlace;
        // change the mesh renderer to match whatever value is in each place
        private void Start()
        {
            meshFilter = GetComponentInChildren<MeshFilter>();
            //meshFilter.sharedMesh = Resources.Load<Mesh>("");
        }
        private void Update()
        {
            ShowPlace();
        }
        private void ShowPlace()
        {
            digitPlace digitPlace = _myPlace;
            Debug.Log(digitPlace.ToString());
            if(_isActive)
            {
                //transform.position.y = transform.position.y + _isActiveHeight;
            }
        }
        private void SetValue()
        {

        }
    }
}
