using UnityEngine;
using UnityEngine.Events;
using static NumbersTheCalculator.Enums;

namespace NumbersTheCalculator
{
    public class Keyswitch : MonoBehaviour
    {
        public UnityEvent onPress;
        public UnityEvent onRelease;

        private bool _isPressed;
        public float holdTime;       
        private KeyValue _keyValue;
        private Calculator _calculator;
        private MeshFilter _capMeshFilter;
        private MeshFilter _legendMeshFilter;
        private BoxCollider _boxCollider;
        // Start is called before the first frame update
        void Awake()
        {
            _calculator = FindObjectOfType<Calculator>();
            _capMeshFilter = GetComponent<MeshFilter>();
            _legendMeshFilter = transform.Find("Legend").GetComponent<MeshFilter>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void SetKeyValue(KeyValue keyValue)
        {
            _keyValue = keyValue;
            KeyStyle keyStyle = KeyStyle.Default;

            switch (keyValue)
            {
                case KeyValue.Zero:
                    keyStyle = KeyStyle.Long;                                        
                    _legendMeshFilter.mesh = _calculator.legendMeshes[0];
                    _legendMeshFilter.transform.position += new Vector3(1f, 0, 0);
                    break;
                case KeyValue.Equals:
                    keyStyle = KeyStyle.Tall;
                    _legendMeshFilter.mesh = _calculator.legendMeshes[40];
                    _legendMeshFilter.transform.position += new Vector3(0, 0, 1f);
                    break;
                case KeyValue.Add:
                    keyStyle = KeyStyle.Tall;
                    _legendMeshFilter.mesh = _calculator.legendMeshes[12];
                    _legendMeshFilter.transform.position += new Vector3(0, 0, 1f);
                    break;
                case KeyValue.Clear:
                    _legendMeshFilter.mesh = _calculator.legendMeshes[16];
                    break;
                case KeyValue.Divide:
                    _legendMeshFilter.mesh = _calculator.legendMeshes[5];
                    _legendMeshFilter.transform.localScale += new Vector3(1f, 0, 0);
                    _legendMeshFilter.transform.Rotate(0, 0, -65f);
                    break;
                case KeyValue.Multiply:
                    _legendMeshFilter.mesh = _calculator.legendMeshes[12];
                    _legendMeshFilter.transform.Rotate(0, 0, 45f);
                    break;
                case KeyValue.Subtract:
                    _legendMeshFilter.mesh = _calculator.legendMeshes[5];
                    break;
                default:
                    _legendMeshFilter.mesh = _calculator.legendMeshes[((int)keyValue)];
                    break;
            }
            switch (keyStyle)
            {
                case KeyStyle.Tall:
                    _capMeshFilter.mesh = _calculator.capMeshes[2];
                    _boxCollider.center = new Vector3(0, -.25f, 1f);
                    _boxCollider.size = new Vector3(1.33f, 1f, 3.5f);
                    break;
                case KeyStyle.Long:
                    _capMeshFilter.mesh = _calculator.capMeshes[1];
                    _boxCollider.center = new Vector3(1.1f, -.25f, 0);
                    _boxCollider.size = new Vector3(3.5f, 1f, 1.33f);
                    break;
                default:
                    _capMeshFilter.mesh = _calculator.capMeshes[0];
                    break;
            } 
        }
        public void KeyDown()
        {
            _calculator.EnterValue(_keyValue);
            Vector3 bottomOut = new Vector3(0f, -.66f, 0f);
            transform.position += bottomOut;
            _isPressed = true;
        }
        public void KeyUp()
        {            
            Debug.Log("RELEASE " + _keyValue);            
            Vector3 bottomOut = new Vector3(0f, -.66f, 0f);
            transform.position -= bottomOut;
            _isPressed = false;
        }
        public bool isPressed { get => _isPressed;}
    }
}
