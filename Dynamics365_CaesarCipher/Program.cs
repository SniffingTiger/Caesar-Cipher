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
            Console.WriteLine(CaesarCipher("-1:STRING"));
        }

        static string CaesarCipher(string unencryptedStringInput)
        {
            string encryptedString = "";
            int arrIndex = 0;

            char[] unencryptedInputCharArray = new char[unencryptedStringInput.Length];

            unencryptedInputCharArray = unencryptedStringInput.ToCharArray();

            // If the number is negative, start looking for digits after the negative symbol
            if (unencryptedInputCharArray[0] == '-')
            {
                arrIndex = 1;
            }

            if (!char.IsDigit(unencryptedInputCharArray[arrIndex]))
            {
                throw new FormatException("Your string must begin with a number that specifies how many places the string should be moved.");
            }

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
            arrIndex++;
            shiftNumber = Convert.ToInt32(shiftString);

            char[] encryptedCharArray = new char[unencryptedInputCharArray.Length - arrIndex];
            for (int i = 0; i < unencryptedInputCharArray.Length - arrIndex; i++)
            {
                encryptedCharArray[i] = unencryptedInputCharArray[i + arrIndex];
            }

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
            else if (unencryptedInputCharArray[0] != '-')
            {
                for (int i = 0; i < encryptedCharArray.Length; i++)
                {
                    int unicodeVal = (int)(encryptedCharArray[i]);
                    // If characters are uppercase
                    if (unicodeVal > 64 && unicodeVal < 91)
                    {
                        unicodeVal += shiftNumber;
                        while (unicodeVal < 65)
                        {
                            unicodeVal -= 26;
                        }
                    }

                    // If characters are lowercase
                    if (unicodeVal > 96 && unicodeVal < 123)
                    {
                        unicodeVal += shiftNumber;
                        while (unicodeVal < 97)
                        {
                            unicodeVal -= 26;
                        }
                    }

                    // If characters are numbers
                    if (unicodeVal > 47 && unicodeVal < 58)
                    {
                        unicodeVal += shiftNumber;
                        while (unicodeVal < 48)
                        {
                            unicodeVal -= 10;
                        }
                    }

                    encryptedCharArray[i] = Convert.ToChar(char.ConvertFromUtf32(unicodeVal));
                }
            }
            else
                throw new Exception("An unidentified error has occured.");

            
            encryptedString = new string(encryptedCharArray);

            return encryptedString;
        }
    }
}
