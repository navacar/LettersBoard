using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{    
    public class ToolBar : MonoBehaviour
    {
        [SerializeField]
        private InputField _widthField;
        [SerializeField]
        private InputField _heightField;
        [SerializeField]
        private Text _warningsLabel;


        private int width = 0;
        private int height = 0;
        
        private int lettersSize; // default 34
        private int maxLettersWidth;
        private int maxLettersHeight;

        private bool isGenButtonPressed = false;
        private bool isMixButtonPressed = false;

        private bool isMixButtonCanBePressed = true;


        void Start()
        {
            _warningsLabel.enabled = false;
            _widthField.onEndEdit.AddListener(GetWidthFromField);
            _heightField.onEndEdit.AddListener(GetHeigthFromField);
            lettersSize = GameField.GetLettersSize();

            maxLettersWidth = (int)GameField.GetRectWidth() / lettersSize;
            maxLettersHeight = (int)GameField.GetRectHeight() / lettersSize;
        }


        public void GetWidthFromField(string newWidth)
        {
            if (!string.IsNullOrEmpty(newWidth))
            {
                width = int.Parse(newWidth);
            }
        }


        public void GetHeigthFromField(string newHeight)
        {
            if (!string.IsNullOrEmpty(newHeight))
            {
                height = int.Parse(newHeight);
            }
        }


        public void OnGenerateButtonPress()
        {
            if (width <= 0 || height <= 0)
            {
                _warningsLabel.enabled = true;
                _warningsLabel.text = "Ширина и высота должны быть больше 0";
            }
            else if (width > maxLettersWidth)
            {
                _warningsLabel.enabled = true;
                _warningsLabel.text = "Максимальная ширина " + maxLettersWidth;
            }
            else if (height > maxLettersHeight)
            {
                _warningsLabel.enabled = true;
                _warningsLabel.text = "Максимальная высота " + maxLettersHeight;
            }
            else
            {
                isGenButtonPressed = true;
                _warningsLabel.enabled = false;
            }
        }


        public void OnMixButtonPress()
        {
            if (isMixButtonCanBePressed)
            {
                isMixButtonPressed = true;
            }
        }


        public bool GetIsGenButtonPressed()
        {
            return isGenButtonPressed;
        }


        public void SetIsGenButtonPressed(bool newState)
        {
            isGenButtonPressed = newState;
        }


        public bool GetIsMixButtonPressed()
        {
            return isMixButtonPressed;
        }


        public void SetIsMixButtonPressed(bool newState)
        {
            isMixButtonPressed = newState;
        }


        public void SetIsMixButtonCanBePressed(bool newState)
        {
            isMixButtonCanBePressed = newState;
        }


        public int GetWidth()
        {
            return width;
        }


        public int GetHeight()
        {
            return height;
        }
    }
}
