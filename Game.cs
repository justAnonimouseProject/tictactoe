using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Game
    {
        static void Main(string[] args)
        {
            //GameEngine engine = new GameEngine();

            int gameFieldSize = 3;
            uint movesCounter = 0;
            char[,] field = InitGameField(gameFieldSize);
            char firstTurn = ChooseWhoIsFirstTurn();
            while (true)
            {
                Console.Clear();
                PrintField(field, movesCounter);
                if (IsGameOver(movesCounter, field)) 
                {
                    if (IsPlayerWin(field)) 
                    {
                        Console.WriteLine("Player's win!");
                    }
                    else if (IsComputerWin(field))
                    {
                        Console.WriteLine("Computer's win!");
                    }
                    else 
                    {
                        Console.WriteLine("No winner!");
                    }

                    break;
                }

                if (IsPlayersTurn(firstTurn, movesCounter))
                {
                    int cellInField = ReadInput();
                    int row = (cellInField - 1) / gameFieldSize;
                    int col = (cellInField - 1) % gameFieldSize;
                    if (field[row, col] == '.')
                    {
                        field[row, col] = 'x';
                    }
                    else 
                    {
                        Console.WriteLine("Invalid move!");
                        movesCounter--;
                    }
                }
                else
                {
                    int cellInField = GenerateMove(field);
                    int row = (cellInField - 1) / gameFieldSize;
                    int col = (cellInField - 1) % gameFieldSize;
                    field[row, col] = 'o';
                }

                movesCounter++;
            }
        }

        private static int GenerateMove(char[,] currentField)
        {
            int result = 0;
            result = TryToMadeThree(currentField);
            
            if (result == 0)
            {
                result = BlockPlayersPossibleThree(currentField);
            }
            
            if (result == 0)
            {
                for (int row = 0; row < currentField.GetLength(0); row++)
                {
                    for (int col = 0; col < currentField.GetLength(1); col++)
                    {
                        if (currentField[row, col] == '.')
                        {
                            result = row * 3 + col + 1;
                            break;
                        }
                    }
                    if (result != 0) 
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private static int BlockPlayersPossibleThree(char[,] currentField)
        {
            int cellNumer = CheckForTwoAndReturnThird(currentField, 'x');
            return cellNumer;
        }

        private static int CheckForTwoAndReturnThird(char[,] currentField, char symbol)
        {
            int result = 0;
            for (int row = 0; row < currentField.GetLength(0); row++)
            {
                if (currentField[row, 0] == symbol) 
                {
                    if (currentField[row, 1] == symbol) 
                    {
                        if (currentField[row, 2] == '.')
                        {
                            result = row * 3 + 3;
                        } 
                    }
                    else if(currentField[row, 2] == symbol)
                    {
                        if (currentField[row, 1] == '.')
                        {
                            result = row * 3 + 2;
                        } 
                    }
                }
                else if (currentField[row, 1] == symbol) 
                {
                    if (currentField[row, 2] == symbol) 
                    {
                        if (currentField[row, 0] == '.')
                        {
                            result = row * 3 + 1;
                        } 
                    }
                }
            }

            for (int col = 0; col < currentField.GetLength(1); col++)
            {
                if (currentField[0, col] == symbol)
                {
                    if (currentField[1, col] == symbol)
                    {
                        if (currentField[2, col] == '.')
                        {
                            result = 7 + col;
                        } 
                    }
                    else if (currentField[2, col] == symbol)
                    {
                        if (currentField[1, col] == '.')
                        {
                            result = 4 + col;
                        } 
                    }
                }
                else if (currentField[1, col] == symbol)
                {
                    if (currentField[2, col] == symbol)
                    {
                        if (currentField[0, col] == '.')
                        {
                            result = 1 + col;
                        }
                    }
                }
            }

            if (currentField[0, 0] == symbol)
            {
                if (currentField[1, 1] == symbol)
                {
                    if (currentField[2, 2] == '.')
                    {
                        result = 9;
                    }
                }
                else if (currentField[2, 2] == symbol)
                {
                    if (currentField[1, 1] == '.')
                    {
                        result = 5;
                    }
                }
            }
            else 
            {
                if (currentField[1, 1] == symbol) 
                {
                    if (currentField[2, 2] == symbol) 
                    {
                        if (currentField[0, 0] == '.') 
                        {
                            result = 1;
                        }
                    }
                }
            }

            if (currentField[2, 0] == symbol)
            {
                if (currentField[1, 1] == symbol)
                {
                    if (currentField[0, 2] == '.')
                    {
                        result = 3;
                    }
                }
                else if (currentField[0, 2] == symbol)
                {
                    if (currentField[1, 1] == '.')
                    {
                        result = 5;
                    }
                }
            }
            else
            {
                if (currentField[1, 1] == symbol)
                {
                    if (currentField[0, 2] == symbol)
                    {
                        if (currentField[2, 0] == '.')
                        {
                            result = 7;
                        }
                    }
                }
            }

            return result;
        }

        private static int TryToMadeThree(char[,] currentField)
        {
            int cellNumer = CheckForTwoAndReturnThird(currentField, 'o');
            return cellNumer;
        }

        private static bool IsComputerWin(char[,] currentField)
        {
            bool result = false;
            if(IsThreeMade(currentField, 'o'))
            {
                result = true;
            }

            return result;
        }

        private static bool IsThreeMade(char[,] currentField, char symbol)
        {
            bool hasRow = false;
            for (int row = 0; row < currentField.GetLength(0); row++)
            {
                if (currentField[row, 0] == symbol && currentField[row, 1] == symbol && currentField[row, 2] == symbol) 
                {
                    hasRow = true;
                }
            }

            bool hasCol = false;
            for (int col = 0; col < currentField.GetLength(0); col++)
            {
                if (currentField[col, 0] == symbol && currentField[col, 1] == symbol && currentField[col, 2] == symbol)
                {
                    hasCol = true;
                }
            }
            
            bool hasDiagonal = false;
            if ((currentField[0, 0] == symbol && currentField[1, 1] == symbol && currentField[2, 2] == symbol) ||
                (currentField[2, 0] == symbol && currentField[1, 1] == symbol && currentField[0, 2] == symbol))
            {
                hasDiagonal = true;
            }

            bool result = false;
            if (hasRow || hasCol || hasDiagonal) 
            {
                result = true;
            }

            return result;
        }

        private static bool IsPlayerWin(char[,] currentField)
        {
            bool result = false;
            if (IsThreeMade(currentField, 'x'))
            {
                result = true;
            }

            return result;
        }

        private static bool IsGameOver(uint movesMade, char[,] currentField)
        {
            bool result = false;
            if(movesMade >= 9 || IsComputerWin(currentField) || IsPlayerWin(currentField))
            {
                result = true;
            }

            return result;
        }

        private static char ChooseWhoIsFirstTurn()
        {
            char result = 'n';
            while (result != 'P' && result != 'C') 
            {
                Console.Write("Who is first? Enter P for player or C for computer: ");
                result = char.Parse(Console.ReadLine());
            }

            return result;
        }

        private static bool IsPlayersTurn(char firstTurn, uint madeMoves)
        {
            bool result = false;
            if (firstTurn == 'P' && madeMoves % 2 == 0) 
            {
                result = true;
            }

            if (firstTurn == 'C' && madeMoves % 2 == 1)
            {
                result = true;
            }

            return result;
        }

        private static void PrintField(char[,] field, uint movesMade)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write("{0,2}", field[row, col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(movesMade);
        }

        private static int ReadInput()
        {
            int choise = 0;
            while (choise < 1 || 9 < choise)
            {
                Console.Write("Player's turn: ");
                choise = int.Parse(Console.ReadLine());
            } 
            
            return choise;
        }
        
        static char[,] InitGameField(int size)
        {            
            char[,] gameField = new char[size, size];
            for (int row = 0; row < gameField.GetLength(0); row++)
            {
                for (int col = 0; col < gameField.GetLength(1); col++)
                {
                    gameField[row, col] = '.';
                }
            }

            return gameField;
        }
    }
}
    

