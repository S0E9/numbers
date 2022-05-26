using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumbersTheCalculator
{
    public class OpenCalculator : MonoBehaviour
    {
        [SerializeField]
        private float _coverSpeed;
        [SerializeField]
        private float _coverKill;
        [SerializeField]
        private GameObject _cover;
        private Vector3 _coverPosition;
        private Vector3 _endPosition;
        private InputManager _userInput;
        private void Awake()
        {
            _userInput = GetComponentInParent<InputManager>();
            _coverPosition = _cover.transform.position;
            _endPosition = new Vector3(0f, 0f, 30f);
            Debug.Log(_endPosition);
            
        }
        private void FixedUpdate()
        {
            if (_userInput.hasBeenCalled)
            {
                _cover.transform.position += Vector3.forward;                
            }
            if (_cover.transform.position.z >= _endPosition.z)
            {
                Destroy(_cover);
            } 
        }
    }
}
