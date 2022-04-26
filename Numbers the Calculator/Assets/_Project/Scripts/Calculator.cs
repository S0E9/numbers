using NumbersTheCalculator.Assets._Project.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NumbersTheCalculator.Keyswitch;
using System.Linq;

namespace NumbersTheCalculator
{
    public class Calculator : MonoBehaviour
    {
        private enum DigitPlace // this might just be for the display class
        {
            Ones, Tens, Hundreds,
            Thousands, TenThousands, HundredThousands,
            Millions, TenMillions, HundredMillions,
            Billions, Comma, Decimal
        };
        [SerializeField]
        private Mesh[] _legendMeshes; // Sets calculator display & keyswitches add getter & setter
        [SerializeField]
        private Mesh[] _capMeshes;
        [SerializeField]
        private Keyswitch _keyswitch;
        [SerializeField]
        private GameObject _equalsMesh;
        [SerializeField]
        private Color _mainColor;
        [SerializeField]
        private Color _altColor;
        private Material _keyswitchMaterial;
        private Keyswitch[] _keyswitchArray;
        private Color _keyswitchColor;
        
        private ResultDisplay _resultDisplay;

        private void Awake()
        {
            _keyswitchArray = new Keyswitch[17];
            DrawKeyswitches();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void DrawKeyswitches()
        {
            KeyValue keyval = 0;
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
                newKeyswitch.SetKeyStyle(keyval);
                newKeyswitch.name = keyval.ToString() + " Key";
                // Adjust Long & Tall Keys
                if (keyval == 0)
                {
                    keySpawnPosition.x += hShift;
                }
                if (keyval is (KeyValue)5 or (KeyValue)12)
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
                keyval++;
            }
        }
        public Mesh[] legendMeshes
        {
            get { return _legendMeshes;}
        }
        public Mesh[] capMeshes
        {
            get { return _capMeshes; }
        }
    }
}
