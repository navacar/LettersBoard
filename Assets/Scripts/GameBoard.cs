using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game 
{
    public class GameBoard : MonoBehaviour
    {

        [SerializeField]
        private Transform _rectSize;

        private static float rectWidth;
        private static float rectHeight;

        private RectTransform mainRect;

        void Start()
        {
            mainRect = _rectSize.GetComponent<RectTransform>();
            rectWidth = mainRect.rect.width * mainRect.lossyScale.x;
            rectHeight = mainRect.rect.height * mainRect.lossyScale.y;
        }


        private void Update()
        {
            float newWidth = mainRect.rect.width * mainRect.lossyScale.x;
            float newHeight = mainRect.rect.height * mainRect.lossyScale.y;

            if (rectWidth == newWidth)
            {
                rectWidth = newWidth;
            }

            if (rectHeight == newHeight)
            {
                rectHeight = newHeight;
            }
        }


        public static float GetRectWidth()
        {
            return rectWidth;
        }


        public static float GetRectHeight()
        {
            return rectHeight;
        }
    }
}

