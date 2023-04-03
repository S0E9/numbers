using UnityEngine;
using UnityEngine.Events;
using static NumbersTheCalculator.Enums;

namespace NumbersTheCalculator
{
    public class Keyswitch : MonoBehaviour
    {
        public UnityEvent onPress;
        public UnityEvent onRelease;
        [SerializeField]
        private AudioClip[] _keyDown;
        [SerializeField]
        private AudioClip[] _keyUp;
        [SerializeField]
        private AudioSource _audioSource;

        private bool _isPressed;
        public float holdTime;       
        private KeyValue _keyValue;
        private Calculator _calculator;
        private MeshFilter _capMeshFilter;
        private MeshFilter _legendMeshFilter;
        private BoxCollider _boxCollider;
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
            KeyStyle keyStyle = keyValue switch
            {
                KeyValue.Zero => KeyStyle.Long,
                KeyValue.Equals => KeyStyle.Tall,
                KeyValue.Add => KeyStyle.Tall,
                _ => KeyStyle.Default
            };

            _legendMeshFilter.mesh = keyValue switch
            {
                KeyValue.Zero => _calculator.legendMeshes[0],
                KeyValue.Equals => _calculator.legendMeshes[40],
                KeyValue.Add => _calculator.legendMeshes[12],
                KeyValue.Clear => _calculator.legendMeshes[16],
                KeyValue.Divide => _calculator.legendMeshes[5],
                KeyValue.Multiply => _calculator.legendMeshes[12],
                KeyValue.Subtract => _calculator.legendMeshes[5],
                _ => _calculator.legendMeshes[(int)keyValue]
            };            
            if (keyValue == KeyValue.Zero)
                _legendMeshFilter.transform.position += new Vector3(1f, 0, 0);
            if (keyValue == KeyValue.Equals || keyValue == KeyValue.Add)
                _legendMeshFilter.transform.position += new Vector3(0, 0, 1f);
            if (keyValue == KeyValue.Multiply)
                _legendMeshFilter.transform.Rotate(0, 0, 45f);
            if (keyValue == KeyValue.Divide)
            {
                _legendMeshFilter.transform.localScale += new Vector3(1f, 0, 0);
                _legendMeshFilter.transform.Rotate(0, 0, -65f);
            }
            SetKeyStyle(keyStyle);
        }
        public void KeyDown()
        {
            AudioClip keyDown = _keyDown[Random.Range(0, _keyDown.Length)];
            _audioSource.PlayOneShot(keyDown);
            _calculator.EnterValue(_keyValue);
            Vector3 bottomOut = new Vector3(0f, -.66f, 0f);
            transform.position += bottomOut;
            _isPressed = true;
        }
        public void KeyUp()
        {
            AudioClip keyUp = _keyUp[Random.Range(0, _keyUp.Length)];
            _audioSource.PlayOneShot(keyUp);
            Vector3 bottomOut = new Vector3(0f, -.66f, 0f);
            transform.position -= bottomOut;
            _isPressed = false;
        }
        private void SetKeyStyle(KeyStyle keystyle)
        {
            _boxCollider.center = keystyle switch
            {
                KeyStyle.Tall => new Vector3(0, -.25f, 1f),
                KeyStyle.Long => new Vector3(1.1f, -.25f, 0),
                _ => new Vector3(0, 0, 0)
            };
            _boxCollider.size = keystyle switch
            {
                KeyStyle.Tall => new Vector3(1.33f, 1f, 3.5f),
                KeyStyle.Long => new Vector3(3.5f, 1f, 1.33f),
                _ => new Vector3(1f, 1f, 1f)
            };
            int value = keystyle switch
            {
                KeyStyle.Tall => 2,
                KeyStyle.Long => 1,
                _ => 0
            };
            _capMeshFilter.mesh = _calculator.capMeshes[value];
        }
        public bool isPressed { get => _isPressed;}
    }
}
