using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365_CaesarCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CaesarCipher(""));
            Console.ReadLine();
        }

        static string CaesarCipher(string unencryptedStringInput)
        {
            // arrIndex tracks our position in the character array
            int arrIndex = 0;

            // Create new character array to store all the characters in the input string
            char[] unencryptedInputCharArray = new char[unencryptedStringInput.Length];

            // Store all the characters in the input string in the new character array
            unencryptedInputCharArray = unencryptedStringInput.ToCharArray();

            // If the number is negative, start looking for digits after the negative symbol
            if (unencryptedInputCharArray[0] == '-')
            {
                arrIndex = 1;
            }

            // If the first character (after the negative symbol, if there is one) is not a digit, throw a Format Exception
            if (!char.IsDigit(unencryptedInputCharArray[arrIndex]))
            {
                throw new FormatException("Your string must begin with a number that specifies how many places the string should be moved.");
            }

            // Get the shift numbers from the string
            string shiftString = "";
            bool isDigit = true;
            int shiftNumber;
            while (isDigit)
            {
                if (char.IsDigit(unencryptedInputCharArray[arrIndex]) == true)
                {
                    shiftString += (unencryptedInputCharArray[arrIndex]);
                    arrIndex++;
                }
                else
                    isDigit = false;
            }

            // If the first character after the digits is not a semicolon, throw a FormatException
            if (unencryptedInputCharArray[arrIndex] != ':')
            {
                throw new FormatException("There must be a semicolon following the number");
            }
            shiftNumber = Convert.ToInt32(shiftString);

            // Set arrIndex to the location of the first letter
            arrIndex++;

            // If character length is over 200, throw ArgumentOutOfRangeException
            if (unencryptedInputCharArray.Length - arrIndex > 200)
            {
                throw new ArgumentOutOfRangeException("Character length can only be up to 200 characters.");
            }

            // If shift number is not between -1Bil and 1Bil, throw ArgumentOutOfRangeException
            if (shiftNumber < -1000000000 || shiftNumber > 1000000000)
            {
                throw new ArgumentOutOfRangeException("Shift number must be between -1000000000 and 1000000000 (between -1Bil and 1Bil).");
            }

            // Store the letters after the semicolon into encryptedCharArray
            char[] encryptedCharArray = new char[unencryptedInputCharArray.Length - arrIndex];
            for (int i = 0; i < unencryptedInputCharArray.Length - arrIndex; i++)
            {
                encryptedCharArray[i] = unencryptedInputCharArray[i + arrIndex];
            }

            // Encrypt encryptedCharArray with a negative shift number
            if (unencryptedInputCharArray[0] == '-')
            {
                for (int i = 0; i < encryptedCharArray.Length; i++)
                {
                    int unicodeVal = (int)(encryptedCharArray[i]);
                    // If characters are uppercase
                    if (unicodeVal > 64 && unicodeVal < 91)
                    {
                        unicodeVal -= shiftNumber;
                        while (unicodeVal < 65)
                        {
                            unicodeVal += 26;
                        }
                    }

                    // If characters are lowercase
                    if (unicodeVal > 96 && unicodeVal < 123)
                    {
                        unicodeVal -= shiftNumber;
                        while (unicodeVal < 97)
                        {
                            unicodeVal += 26;
                        }
                    }

                    // If characters are numbers
                    if (unicodeVal > 47 && unicodeVal < 58)
                    {
                        unicodeVal -= shiftNumber;
                        while (unicodeVal < 48)
                        {
                            unicodeVal += 10;
                        }
                    }

                    encryptedCharArray[i] = Convert.ToChar(char.ConvertFromUtf32(unicodeVal));
                }
            }
            // Encrypt encryptedCharArray with a positive shift number
            else if (unencryptedInputCharArray[0] != '-')
            {
                for (int i = 0; i < encryptedCharArray.Length; i++)
                {
                    int unicodeVal = (int)(encryptedCharArray[i]);
                    // If characters are uppercase
                    if (unicodeVal > 64 && unicodeVal < 91)
                    {
                        unicodeVal += shiftNumber;
                        while (unicodeVal > 90)
                        {
                            unicodeVal -= 26;
                        }
                    }

                    // If characters are lowercase
                    if (unicodeVal > 96 && unicodeVal < 123)
                    {
                        unicodeVal += shiftNumber;
                        while (unicodeVal > 122)
                        {
                            unicodeVal -= 26;
                        }
                    }

                    // If characters are numbers
                    if (unicodeVal > 47 && unicodeVal < 58)
                    {
                        unicodeVal += shiftNumber;
                        while (unicodeVal > 57)
                        {
                            unicodeVal -= 10;
                        }
                    }

                    encryptedCharArray[i] = Convert.ToChar(char.ConvertFromUtf32(unicodeVal));
                }
            }
            // General exception handling
            else
                throw new Exception("An unidentified error has occured.");

            // Store encryptedCharArray in a new string
            string encryptedString = new string(encryptedCharArray);

            // Return encrypted string
            return encryptedString;
        }
    }
}
