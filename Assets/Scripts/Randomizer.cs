using System;
using UnityEngine;
using Random = System.Random;


namespace Game 
{
    public class Randomizer : MonoBehaviour
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


        public static string RandomLetter()
        {
            int randomedValue = new Random().Next(26);
            return chars[randomedValue].ToString();
        }


        public static int RandomNewPosition(int size)
        {
            return new Random().Next(size);
        }
    }       
}
