using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontlineCodeChallenge_DePaul
{
    class StringConverter
    {
        //The idea behind the first implementation is to take the original string and convert it to a character array.
        //Next, I cycle through the character array and add the appropriate number of '-' as well as ' ' in front of the first
        //letter of each word where necessary. This is triggered off of the '(' char and ',' char. 
        //Then I write the other letters of each word to the console.
        public void ConvertOriginal_FirstIdea()
        {
            string originalString = "(id,created,employee(id,firstname,employeeType(id), lastname),location)";
            char[] charArray = originalString.ToCharArray();
            int parenCount = 0;

            Console.WriteLine("Original String First Idea: " + originalString);
            Console.WriteLine();

            foreach (char elem in charArray)
            {
                if (elem != ' ')
                {
                    if (elem == '(')
                    {
                        parenCount++;
                        PrintDash(parenCount, elem);
                    }
                    else if (elem == ')')
                        parenCount--;
                    else if (elem == ',')
                    {
                        Console.WriteLine();
                        PrintDash(parenCount, elem);
                    }
                    else
                        Console.Write(elem);
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void PrintDash(int count, char ch)
        {
            if (count == 2 || count == 3)
            {
                if(ch == '(')
                    Console.WriteLine();

                for (int i = 2; i <= count; i++)
                    Console.Write('-');

                Console.Write(' ');
            }
        }


        //The idea behind the second implementation is to take the original string and convert it to a character array.
        //Then I convert the character array to a List<String> consisting of either "(", ")", or words I created from the 
        //individual letters in the character array.
        //Lastly, I cycle through the List<String> and determine if each word should have either "- " or "-- " appended to it.
        //Another List<String> is created while cycling through the List;
        //This logic is determined based upon the number of "(" cycled through.
        public void ConvertOriginal_SecondIdea()
        {
            string originalString = "(id,created,employee(id,firstname,employeeType(id), lastname),location)";
            char[] charArray = originalString.ToCharArray();
            List<String> convertedToList = ConvertCharArrayToList(charArray);

            Console.WriteLine("Original String Second Idea: " + originalString);
            CreateListWithDashes(convertedToList);
            Console.WriteLine();
            Console.WriteLine();
        }

        private List<String> ConvertCharArrayToList(char[] charArray)
        {
            List<String> lstStr = new List<string>();
            string temp = "";
            int count = 0;

            foreach (char elem in charArray)
            {
                if (char.IsLetter(elem) || elem == '(' || elem == ')' || elem == ',')
                {
                    if (elem == '(')
                    {
                        count++;
                        if (count > 1)
                        {
                            lstStr.Add(temp);
                            temp = "";
                        }

                        lstStr.Add(Char.ToString(elem));
                    }
                    else if (elem == ')')
                    {
                        lstStr.Add(temp);
                        temp = "";

                        lstStr.Add(Char.ToString(elem));
                        count--;
                    }
                    else if (elem == ',')
                    {
                        if (temp != "")
                        {
                            lstStr.Add(temp);
                            temp = "";
                        }
                    }
                    else
                    {
                        temp = temp + elem;
                    }
                }
            }
            return lstStr;
        }

        private void CreateListWithDashes(List<String> convertedToList)
        {
            List<String> list = new List<string>();
            int count = 0;
            string temp1 = "";
            string temp2 = "";

            foreach (String elem in convertedToList)
            {
                if (elem == "(")
                {
                    count++;
                    if (count >= 2)
                    {
                        temp1 = "";
                        for (int x = 2; x <= count; x++)
                            temp1 = temp1 + "-";

                        temp1 = temp1 + " ";
                    }
                }
                else if (elem == ")")
                {
                    count--;
                    if (count >= 2)
                    {
                        temp1 = "";
                        for (int x = 2; x <= count; x++)
                            temp1 = temp1 + "-";

                        temp1 = temp1 + " ";
                    }
                }
                else
                {
                    if (count >= 2)
                    {
                        temp2 = temp1 + elem;
                        list.Add(temp2);
                        temp2 = "";
                    }
                    else
                        list.Add(elem);
                }
            }

            foreach (String s in list)
                Console.WriteLine(s);
        }

        //The idea behind generating the Bonus output in alphabetical order is:
        //1. Convert original string to character array
        //2. Convert character array to a List<String> that consists of "(", ")", or the word in the original string, 
        //   all of which are in the order represented in the original string
        //3. Within AlphabeticalOrder(), create three distinct List<String> types to hold the words at their respective level.
        //   I define a level as being the number of "(" that preceed it without its corresponding ")" appearing.
        //4. Have two string variables that identify the word on each level that transitions to another level
        //5. Sort all three List<String> objects
        //6. Print the strings to the console.
        //   Use the level identifying variables to initiate the printing of each List<String> object for the appropriate level it represents 
        public void ConvertBonus()
        {
            string originalString = "(id,created,employee(id,firstname,employeeType(id), lastname),location)";
            char[] charArray = originalString.ToCharArray();
            List<String> convertedToList = ConvertCharArrayToList(charArray);

            Console.WriteLine("Original String Bonus: " + originalString);
            Console.WriteLine();
            AlphabeticalOrder(convertedToList);
            Console.WriteLine();
            Console.WriteLine();
        }

        private void AlphabeticalOrder(List<String> convertedToList)
        {
            int count = 0;
            string prevOne = "";
            string prevTwo = "";
            string strOne = "";
            string strTwo = "";
            List<string> one = new List<string>();
            List<string> two = new List<string>();
            List<string> three = new List<string>();

            foreach (String elem in convertedToList)
            {
                if (elem == "(")
                {
                    count++;

                    if (count == 2)
                    {
                        strOne = prevOne;
                    }
                    else if (count == 3)
                    {
                        strTwo = prevTwo;
                    }
                }
                else if (elem == ")")
                {
                    count--;
                }
                else
                {
                    switch (count)
                    {
                        case 1:
                            one.Add(elem);
                            if (strOne == "")
                                prevOne = elem;
                            break;
                        case 2:
                            two.Add(elem);
                            if (strTwo == "")
                                prevTwo = elem;
                            break;
                        case 3:
                            three.Add(elem);
                            break;
                    }
                }
            }

            one.Sort();
            two.Sort();
            three.Sort();

            foreach (String o in one)
            {
                Console.WriteLine(o);
                if (o == strOne)
                {
                    foreach (String tw in two)
                    {
                        Console.WriteLine("- " + tw);
                        if (tw == strTwo)
                        {
                            foreach (String th in three)
                            {
                                Console.WriteLine("-- " + th);
                            }
                        }

                    }
                }
            }
        }
    }
}