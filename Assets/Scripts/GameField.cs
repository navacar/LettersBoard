using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game 
{
    public class GameField : MonoBehaviour
    {
        [SerializeField]
        private Text _textSquare;
        [SerializeField]
        private Transform _parent;

        private List<List<Text>> vectorOfTextFields = new List<List<Text>>();
        private List<Vector2> listOfPositions = new List<Vector2>();
        private List<List<Vector2>> newPositionsContainer = new List<List<Vector2>>();

        private ToolBar toolBarController;

        private int fieldWidth = 0;
        private int fieldHeigth = 0;

        private Vector2 squareDefaultPosition;
        bool isAnimationStillOn = false;

        private static float rectWidth;
        private static float rectHeight;
        private static int lettersSize;


        void Start()
        {
            RectTransform fieldRect = _parent.GetComponent<RectTransform>();
            rectWidth = fieldRect.rect.width * fieldRect.lossyScale.x;
            rectHeight = fieldRect.rect.height * fieldRect.lossyScale.y;

            lettersSize = _textSquare.fontSize;
           
            toolBarController = GameObject.Find("ToolBar").GetComponent<ToolBar>();
            _textSquare.enabled = false;
            squareDefaultPosition = _textSquare.transform.position;
        }


        void Update()
        {
            if (toolBarController.GetHeight() != fieldHeigth)
            {
                fieldHeigth = toolBarController.GetHeight();
            }

            if (toolBarController.GetWidth() != fieldWidth)
            {
                fieldWidth = toolBarController.GetWidth();
            }

            if (toolBarController.GetIsGenButtonPressed())
            {
                toolBarController.SetIsMixButtonCanBePressed(false);
                GenerateField();
                isAnimationStillOn = true;
                toolBarController.SetIsGenButtonPressed(false);
            }
            
            if (toolBarController.GetIsMixButtonPressed())
            {
                toolBarController.SetIsMixButtonCanBePressed(false);
                MixField();
                isAnimationStillOn = true;
                toolBarController.SetIsMixButtonPressed(false);
            }


            
            if (isAnimationStillOn)
            {
                MovingLettersFunction();
            }
            else
            {
                toolBarController.SetIsMixButtonCanBePressed(true);
            }
        }


        private void MovingLettersFunction() // Анимация движения букв
        {
            int isAnimationStoped = 0;
            float movementSpeed = (fieldWidth > 11 || fieldHeigth > 11) ?  0.8f : 0.45f;
            
            for (int i = 0; i < vectorOfTextFields.Count; i++)
            {
                for (int j = 0; j < vectorOfTextFields[i].Count; j++)
                {
                    int index = Randomizer.RandomNewPosition(listOfPositions.Count);
                    vectorOfTextFields[i][j].transform.position = Vector2.MoveTowards(vectorOfTextFields[i][j].transform.position, newPositionsContainer[i][j], movementSpeed);
                    
                    if (Vector2.Distance(new Vector2(newPositionsContainer[i][j].x, newPositionsContainer[i][j].y), vectorOfTextFields[i][j].transform.position) < 0.001f) 
                    {
                        isAnimationStoped++;
                    }
                }                
            }

            if (isAnimationStoped == fieldWidth * fieldHeigth)
            {
                isAnimationStillOn = false;
            }
        }


        private void MixField()
        {
            FillPositionList(); // В этой функции программа запоминает существующие позиции
            SetNewPositions(); // В этой функции программа даёт буквам новоые позиции
        }


        private void SetNewPositions()
        {
            if (listOfPositions.Count != 0)
            {
                newPositionsContainer = new List<List<Vector2>>();
                for (int i = 0; i < vectorOfTextFields.Count; i++)
                {
                    List<Vector2> container = new List<Vector2>();
                    for (int j = 0; j < vectorOfTextFields[i].Count; j++)
                    {
                        int index = Randomizer.RandomNewPosition(listOfPositions.Count);
                        container.Add(listOfPositions[index]);
                        listOfPositions.RemoveAt(index);
                    }         

                    newPositionsContainer.Add(container);       
                }
            }
        }

        private void FillPositionList()
        {
            listOfPositions = new List<Vector2>();
            for (int i = 0; i < vectorOfTextFields.Count; i++)
            {
                for (int j = 0; j < vectorOfTextFields[i].Count; j++)
                {
                    listOfPositions.Add(vectorOfTextFields[i][j].transform.position);   
                }
            }
        }

        private void GenerateField()
        {
            FullFillField(); // В этой функции программа сначала удаляет старые буквы, затем создаёт новые
            ResizeField();  // Здесь программа устанавливает изначальные позиции букв
        }


        private void FullFillField()
        {
            if (vectorOfTextFields.Count != 0)
            {
                for (int i = 0; i < vectorOfTextFields.Count; i++)
                {
                    for (int j = 0; j < vectorOfTextFields[i].Count; j++)
                    {
                        Destroy(GameObject.Find(vectorOfTextFields[i][j].name));
                    }
                }
            }

            vectorOfTextFields = new List<List<Text>>();
            int counterForName = 0;

            for (int i = 0; i < fieldWidth; i++)
            {
                List<Text> container = new List<Text>();
                for (int j = 0; j < fieldHeigth; j++)
                {
                    Text newSquare = Instantiate(_textSquare, _parent);
                    newSquare.name = "Square" + counterForName;
                    counterForName++;
                    newSquare.enabled = true;
                    newSquare.text = Randomizer.RandomLetter();
                    container.Add(newSquare);
                }

                vectorOfTextFields.Add(container);
            }
        }


        private void ResizeField()
        {
            // Функция только запоминает новые позиции 
            // Само присвоение позиций проиходи в функции MovingLettersFunction()
            float scalerX = 0;
            const int squareDefaultSize = 70;

            if (vectorOfTextFields.Count != 1)
            {
                scalerX = (float)vectorOfTextFields.Count / 2 - 0.5f;
            }

            newPositionsContainer = new List<List<Vector2>>();
            for (int i = 0; i < vectorOfTextFields.Count; i++)
            {
                float scalerY = 0;
                if (vectorOfTextFields[i].Count != 1)
                {
                    scalerY = (float)vectorOfTextFields[i].Count / 2 - 0.5f;
                }

                List<Vector2> container = new List<Vector2>();
                for (int j = 0; j < vectorOfTextFields[i].Count; j++)
                {
                    container.Add(new Vector2(squareDefaultPosition.x + squareDefaultSize / 2 * scalerX, squareDefaultPosition.y + squareDefaultSize / 2 * scalerY));
                    scalerY --;
                }

                newPositionsContainer.Add(container); 
                scalerX --;                 
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


        public static int GetLettersSize()
        {
            return lettersSize;
        }
    }

}
