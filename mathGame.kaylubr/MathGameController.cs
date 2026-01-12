using MathGame.Utils;

namespace MathGame;

internal static class MathGameController
{
  internal static void PlayGame(MathOperation operation, string message)
  {
    MathGameDifficulty difficulty = GetGameMode();

    Console.Clear();
    Console.WriteLine($"➤  {message}");

    int points = 0;

    for (int i = 0; i < 5; i++)
    {
      int[] numbers = GenerateNumbers(operation, difficulty);
      int num1 = numbers[0];
      int num2 = numbers[1];

      string operatorSymbol = operation switch
      {
        MathOperation.Addition => "+",
        MathOperation.Subtraction => "-",
        MathOperation.Multiplication => "*",
        MathOperation.Division => "/",
        _ => throw new ArgumentException("Invalid Operation")
      };

      Console.Write($"\n{i + 1}. {num1} {operatorSymbol} {num2} = ");
      string? result = Console.ReadLine();
      result = ValidateAnswer(result);

      int correctAnswer = operation switch
      {
        MathOperation.Addition => num1 + num2,
        MathOperation.Subtraction => num1 - num2,
        MathOperation.Multiplication => num1 * num2,
        MathOperation.Division => num1 / num2,
        _ => throw new ArgumentException("Invalid operation")
      };

      if (int.Parse(result) == correctAnswer)
      {
        Console.WriteLine("Your answer was correct!");
        points += 1;
      }
      else
      {
        Console.WriteLine("Your answer wasn't correct!");
      }
    }

    CheckPoints(points, difficulty);

    Console.Write("Press enter to continue...");
    Console.ReadLine();
  }

  private static MathGameDifficulty GetGameMode()
  {
    string? gameModeChoice;

    do
    {
      Console.Clear();
      Console.WriteLine("Choose your difficulty.\n");
      Console.WriteLine("1. Normal");
      Console.WriteLine("2. Advanced");
      Console.Write("\nEnter your choice: ");
      gameModeChoice = Console.ReadLine();
    } while (gameModeChoice != "1" && gameModeChoice != "2");

    Console.WriteLine($"\nPlaying on {(gameModeChoice == "1" ? "normal" : "advanced")} difficulty..");
    Console.Write("\nPress enter to continue..");

    return gameModeChoice == "1" ? MathGameDifficulty.Normal : MathGameDifficulty.Advanced;
  }

  private static int[] GenerateNumbers(MathOperation operation, MathGameDifficulty difficulty)
  {
    Random random = new();

    int num1;
    int num2;

    if (difficulty == MathGameDifficulty.Normal)
    {
      if (operation == MathOperation.Division)
      {
        do
        {
          num1 = random.Next(2, 101);
          num2 = random.Next(1, 51);
        } while (num1 < num2 || num1 % num2 != 0);
      }
      else
      {
        num1 = random.Next(0, 51);
        num2 = random.Next(0, 51);
      }
    }
    else
    {
      if (operation == MathOperation.Division)
      {
        do
        {
          num1 = random.Next(100, 1001);
          num2 = random.Next(50, 101);
        } while (num1 < num2 || num1 % num2 != 0);
      }
      else
      {
        num1 = random.Next(50, 1001);
        num2 = random.Next(50, 1001);
      }
    }


    return [num1, num2];
  }

  private static string ValidateAnswer(string? result)
  {
    int resultAsNumber;
    while (!int.TryParse(result, out resultAsNumber))
    {
      Console.Write("Input should be a number, try again: ");
      result = Console.ReadLine();
    }

    return result;
  }

  private static void CheckPoints(int points, MathGameDifficulty difficulty)
  {
    Console.WriteLine($"\nYou earned {points} points!");
    if (points > 3)
    {
      Console.WriteLine("Very good!\n");
      Faces.PrintHappyFace();
    }
    else if (points > 1)
    {
      Console.WriteLine("\nNice! there's still room for improvements");
      Faces.PrintNeutralFace();
    }
    else
    {
      Console.WriteLine("\nBetter luck next time.");
      Faces.PrintSadFace();
    }

    History.AddToHistory(points, difficulty);
  }
}