using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartScreen : MonoBehaviour
    {
        public Action PlayButtonClicked;
        
        [SerializeField]
        private Button _playButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(() => PlayButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(() => PlayButtonClicked?.Invoke());
        }
    }
}