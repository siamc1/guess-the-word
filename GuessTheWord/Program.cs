using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheWord
{
    class Program
    {
        //Reates a new random object,used to determine the word for each round
        static Random r = new Random();
        static void Main(string[] args)
        {
            //creates a new array called strWordList and reads all the words into it from a file called Words.txt
            string[] strWordList = File.ReadAllLines("Words.txt");
            //creates a new boolean variable to hold whether the game is still being played
            Boolean isPlaying = true;

            //wil loop as long as the game is being played
            while (isPlaying)
            {
                //picks a random word from the list and sets it as the word for that round
                string strWord = strWordList[r.Next(0, strWordList.Length)];
                //creates a new string to hold each guess
                string strGuess = "";
                //creates a counter to count how many guesses are used
                int intCount = 0;
                //creates another variable to hold the nomber of correct characters are in the guess in the wrong spots
                int intCorrectChars;
                //creates a new boolean variable to represent whether the player is in a game right now and sets it to true
                Boolean inGame = true;
                //prints out the length of the mystery word
                Console.WriteLine("The word is " + strWord.Length + " characters long\n\n");

                //will run as long as the player is in a game
                while (inGame)
                {
                    //increments the move counter
                    intCount++;
                    //asks for the player to guess a word and takes input from the console, saving it to the strGuess variable
                    Console.Write("Enter Guess: ");
                    strGuess = Console.ReadLine();

                    //Checks to see if the guess was correct
                    if (strGuess == strWord)
                    {
                        //if it was correct, it prints outhow many guesses it took and ends the game
                        Console.WriteLine("You Got It in " + intCount + " Tries!");
                        inGame = false;
                    }
                    //checks to see if the player has exceeded a certain number of guesses only if the player hasn't already gotten the right answer
                    else if (intCount > strWord.Length / 2 + 7)
                    {
                        //if the player exceed the guess limit, it will print a line saying so and end the game
                        Console.WriteLine("Sorry, that's incorrect and you've gone above the guess limit.\nThe word was: " + strWord);
                        inGame = false;
                    }
                    //f checks to see if the player hasn't gotten it and hasn't exceeded the guess limit
                    else
                    {
                        //sets the number of correct characters in wrong places to 0
                        intCorrectChars = 0;
                        //creates a copy of the word to process, this leaves the original word intact
                        String strTempWord = strWord;
                        //runs through the entire length of the guess
                        for (int i = 0; i < strGuess.Length; i++)
                        {
                            //checks to see if the word has the letter at index i in the guess
                            if (strTempWord.Contains(strGuess[i]))
                            {
                                //if the word contains the letter at index i in the guess, it will remove this letter from the word and increment i. this prevents the same letter in the word coming up for multiple instances of the letter in the guess
                                strTempWord = strTempWord.Substring(0, strTempWord.IndexOf(strGuess[i])) + strTempWord.Substring(strTempWord.IndexOf(strGuess[i]) + 1);
                                intCorrectChars++;
                            }
                        }
                        //creates a secondary integer variable to hold the number of correct characters in the correct spots
                        int int2CorrectChars = 0;
                        //creates an instance of the secret word to be processed
                        strTempWord = strWord;
                        //checks to see if the guess is longer than the word itself
                        if (strGuess.Length > strTempWord.Length)
                        {
                            //if the guess is longer than the word itself, it will take a substring of the guess and deal with only that portion, as no letter at an index after the ending of the word can be in the right position
                            strGuess = strGuess.Substring(0, strTempWord.Length);
                        }
                        //will run through all the letters in the guess
                        for (int i = 0; i < strGuess.Length; i++)
                        {
                            //checks to see if the letters match at index i between the guess and the word
                            if (strGuess[i] == strTempWord[i])
                            {
                                //if they match, it will increment the double correct counter
                                int2CorrectChars++;
                            }
                        }
                        //subtracts the number of double correct letters from the total correct letters to get the number of correct letters in the wrong place
                        intCorrectChars -= int2CorrectChars;
                        //prints out that the answer is incorrect, as well as how many letters were correct, but in the wrong locationa dn how many were correct and in the right location
                        Console.WriteLine("Wrong answer\nYou got " + intCorrectChars + " letters correct but in the wrong place\nYou got " + int2CorrectChars + " correct in the correct place");
                    }
                    //writes an empty line for spacing
                    Console.WriteLine("");
                }
                //asks the player if they wish to play again and takes input from the console before storing the input in a new variable
                Console.Write("Do you want to continue playing? (Y yes, N no)");
                string strContinue = Console.ReadLine();
                //will continue looping until a valid input is entered
                while (!strContinue.Equals("n") && !strContinue.Equals("N") && !strContinue.Equals("y") && !strContinue.Equals("Y"))
                {
                    //as long as the input is invalid, it will keep asking for input and saving the input in the variable
                    Console.Write("Invalid Entry\nDo you want to continue playing? (Y yes, N no)");
                    strContinue = Console.ReadLine();
                }
                //if the input was either an n or an N, it will update the isPlaying variable to be  false, signifying that the game is no longer being played
                if (strContinue.Equals("N") || strContinue.Equals("n"))
                {
                    isPlaying = false;
                }
            }

        }
    }
}

