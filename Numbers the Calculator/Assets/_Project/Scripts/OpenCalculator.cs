using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace NumbersTheCalculator
{
    public class OpenCalculator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _cover;
        private Vector3 _endPosition;
        private InputManager _userInput;
        [SerializeField]
        private TMP_Text _versionText;
        private void Awake()
        {
            _userInput = GetComponentInParent<InputManager>();
            _endPosition = new Vector3(0f, 0f, 30f);
        }
        private void FixedUpdate()
        {
            if(!_cover.IsDestroyed())
            {
                if (_userInput.hasBeenCalled)
                {
                    _cover.transform.position += Vector3.forward;                
                }
                if (_cover.transform.position.z >= _endPosition.z)
                {
                    Destroy(_cover);
                    _versionText.enabled = false;
                }
            }
        }
    }
}
