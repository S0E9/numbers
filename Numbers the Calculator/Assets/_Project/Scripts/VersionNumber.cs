using TMPro;
using UnityEngine;

namespace NumbersTheCalculator
{
    [RequireComponent(typeof(TMP_Text))]
    public class VersionNumber : MonoBehaviour
    {
        private TMP_Text _versionText;

        void Awake()
        {
            _versionText = GetComponent<TMP_Text>();
            _versionText.text = "v" + Application.version;
        }
    }
}
