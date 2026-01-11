public enum MathOperation
{
  Addition,
  Subtraction,
  Multiplication,
  Division,
}

public enum MathGameDifficulty
{
  Normal,
  Advanced
}

public class Program
{
  static List<dynamic> history = [];

  public static void Main()
  {
    string? gameModeChoice = null;

    while (true)
    {
      string? choice;

      Console.Clear();
      Console.WriteLine("╔════════════════════════════════╗");
      Console.WriteLine("║                                ║");
      Console.WriteLine("║     WELCOME TO MATH GAME       ║");
      Console.WriteLine("║                                ║");
      Console.WriteLine("╚════════════════════════════════╝\n");
      Console.WriteLine("1. Addition");
      Console.WriteLine("2. Subtraction");
      Console.WriteLine("3. Multiplication");
      Console.WriteLine("4. Division");
      Console.WriteLine("5. Game history");
      Console.WriteLine("6. Exit");
      Console.Write("\nEnter your choice (1 - 6): ");
      choice = Console.ReadLine();

      if (choice == "6")
      {
        Console.WriteLine("\nExiting game..\n");
        break;
      }

      // Prompt for choosing game difficulty if the option is not the game history
      if (choice != "5" && choice != "")
      {
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
      }

      MathGameDifficulty gameMode = gameModeChoice == "1" ? MathGameDifficulty.Normal : MathGameDifficulty.Advanced;

      switch (choice)
      {
        case "1":
          PlayGame(MathOperation.Addition, gameMode, "ADDITION ROUND");
          break;
        case "2":
          PlayGame(MathOperation.Subtraction, gameMode, "SUBTRACTION ROUND");
          break;
        case "3":
          PlayGame(MathOperation.Multiplication, gameMode, "MULTIPLICATION ROUND");
          break;
        case "4":
          PlayGame(MathOperation.Division, gameMode, "DIVISION ROUND");
          break;
        case "5":
          PrintHistory();
          break;
      }
    }
  }

  public static void PlayGame(MathOperation operation, MathGameDifficulty difficulty, string message)
  {
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

  public static int[] GenerateNumbers(MathOperation operation, MathGameDifficulty difficulty)
  {
    Random random = new();

    int num1;
    int num2;

    if (difficulty == MathGameDifficulty.Normal)
    {
      if (operation == MathOperation.Division)
      {
        num1 = random.Next(2, 101);
        num2 = random.Next(1, 11);
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
        num1 = random.Next(100, 1001);
        num2 = random.Next(50, 101);
      }
      else
      {
        num1 = random.Next(50, 1001);
        num2 = random.Next(50, 1001);
      }
    }


    return [num1, num2];
  }

  public static string ValidateAnswer(string result)
  {
    try
    {
      int resultToValidate = int.Parse(result);
      return result;
    }
    catch (FormatException)
    {
      throw new ArgumentException("Error! Answer should be a number..");
    }
    catch (ArgumentNullException)
    {
      throw new ArgumentException("Error! Can't be null..");
    }
  }

  public static void CheckPoints(int points, MathGameDifficulty difficulty)
  {
    Console.WriteLine($"\nYou earned {points} points!");
    if (points > 3)
    {
      Console.WriteLine("Very good!\n");
      PrintHappyFace();
    }
    else if (points > 1)
    {
      Console.WriteLine("\nNice! there's still room for improvements");
      PrintNeutralFace();
    }
    else
    {
      Console.WriteLine("\nBetter luck next time.");
      PrintSadFace();
    }

    AddToHistory(points, difficulty);
  }

  static void PrintHappyFace()
  {
    string happyFace = @"
    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠤⢔⣶⣖⢂⢒⡐⠢⠤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠤⢊⠵⠒⣩⠟⠛⠙⠂⠀⠀⠉⠒⢤⣾⣖⠤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⣀⡤⠄⣀⠀⠀⠀⠀⠀⢀⠔⡡⠊⠀⠀⠀⠁⣀⣀⠀⠀⠀⠀⠀⠀⠈⠉⠻⡆⠈⠢⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⢠⠋⠁⠀⠀⠈⠱⡄⠀⠀⡠⠃⡜⠀⠀⠀⠀⢀⣾⠗⠋⠛⢆⠀⠀⠀⣠⣤⣤⡄⠉⢢⠀⠑⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⢼⠀⠀⠀⠀⠀⠀⠱⠀⢠⠃⢠⠃⠀⠀⠀⠀⢸⠋⣠⣤⡀⠘⡆⠀⢰⡿⠋⠉⠳⣄⠈⣆⠀⠐⡄⠀⠀⢀⠔⠂⠐⠲⢄⠀⠀⠀
⠀⠀⠀⠈⢆⠀⠀⠀⠀⠀⢀⢃⠆⠀⠀⠁⠀⠀⢄⣀⣹⠀⣷⣼⣿⠀⢻⠀⢿⣖⣹⣷⡀⠈⡆⠈⠀⠀⢰⡀⠰⠃⠀⠀⠀⠀⠀⡇⠀⠀
⠀⠀⠀⠀⠈⣆⠤⠤⠤⠤⠾⣼⡀⠀⠀⠀⠀⠀⢀⡀⠂⠙⠻⡓⠋⢀⡏⠀⠀⢿⢿⡽⠃⠀⡜⠀⠀⠀⠀⡇⡇⠀⠀⠀⠀⠀⡠⠁⠀⠀
⠀⠀⢀⠔⡩⠀⠀⠀⠀⠀⠀⠀⠉⠓⢄⠀⠀⠊⠁⠙⢕⠂⠀⠘⡖⠊⠀⠀⠀⠀⠑⡤⠔⠊⡉⠐⠀⠀⢀⣰⡼⠤⠤⠤⢄⣰⠁⠀⠀⠀
⠀⡰⠁⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⡇⠀⠀⠀⠀⠈⣶⡤⣀⠀⠀⠀⠀⠀⠀⠀⠁⠠⣲⠖⠤⢠⠞⠉⠀⠀⠀⠀⠀⠀⠀⠁⠢⡀⠀
⢰⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠉⠛⠒⢧⡀⠀⠀⠀⠀⠘⣷⣀⠉⠑⠒⠂⠒⢐⣦⠖⠋⠀⠀⠀⡗⠀⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠀
⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢳⠀⠀⠀⠀⠀⠸⣿⣷⣦⣤⣤⣤⣾⠇⠀⠀⠀⠀⡴⠛⠉⠀⠀⠀⠀⠉⠐⠂⠀⠀⠀⠀⢠
⢰⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⠄⣀⡀⢀⠞⢄⠀⠀⠀⠀⠀⠘⢾⣿⣻⣿⣿⡟⠀⠀⠀⠀⢸⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸
⠈⢆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠓⢎⠀⠈⠢⡀⠀⠀⠀⠀⠈⠛⠿⠿⢛⠁⠀⠀⠀⠀⠈⢆⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈
⠀⠈⢢⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡜⠻⢤⡀⠈⠲⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠔⢻⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠉⠢⢄⡀⠀⠀⠀⠀⢀⡠⠔⠊⠀⠀⠀⠉⠓⠦⣀⣁⠀⠀⠀⠀⠀⢀⣀⠤⠒⠊⠀⠀⠈⠢⡀⠀⠀⠀⠀⠀⠀⠀⠀  ⢀⠔⠁⠀
⠀⠀⠀⠀⠀⠀⠀⠉⠉⠉''⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀   ⠈⠑⠒⠤⠤⠤⠤⠒⠊⠁⠀⠀⠀
  ";

    Console.WriteLine(happyFace);
  }

  static void PrintNeutralFace()
  {
    string neutralFace = @"
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⡿⣿⣿⣿
    ⡿⠟⠫⠋⢉⣁⣉⡉⠉⠉⠋⠛⣿⣿⣿⡛⠋⠋⠉⠉⣁⣈⣉⡐⠩⠛⢻
    ⣷⣦⣶⣿⡿⠯⠭⠭⠭⠭⣝⢻⣿⣿⣿⡿⢫⠭⠭⠭⠭⠭⠿⣿⣷⣦⣼
    ⣿⣿⣿⣩⡚⠃⢀⠀⡘⠌⢻⣸⣿⣿⣿⣷⣼⣋⢚⢀⣀⢀⠛⣊⣽⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⣼⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⢋⣿⡟⣸⣿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⢙⣛⣛⣛⣛⣛⣛⣛⣉⣩⣭⣴⣾⣿⣿⢣⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣼⣿⣿⣿⣿
  ";

    Console.WriteLine(neutralFace);
  }

  static void PrintSadFace()
  {
    string sadFace = @"
    ⣿⣿⣿⣿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⣽⣿⡟⠁⠀⣠⣾⣿⣿⣿⡿⠟⠁⠀⢀⠠⠀⠂⢀⡀⢤⠒⠀⠂⠄⠀⡀⠀⠀⢀⠀⠀⠊⢶⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⡟⡛
    ⣿⣿⡟⣂⣷⣘⣦⠙⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⡟⠀⠀⣰⢿⣿⡿⠟⠉⠀⠀⠐⠈⢀⣀⣠⣤⣥⣬⣤⣤⣤⣤⣄⣀⣀⠀⠀⠀⠀⠀⠀⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⡿⠛⡟⢫⡙⢡⠊⠤⠁⠀⠄
    ⣿⣿⣷⠰⣼⠿⢮⡱⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣹⣿⡟⠀⠀⠰⠙⠾⠽⠁⠀⠀⣀⣠⠴⣞⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣉⠁⠀⠠⢀⠀⠀⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠗⡈⠃⠐⠁⠈⠄⠐⠀⠈⠀⠈⢀⣶
    ⣿⣟⣿⣷⣬⣞⣦⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⣿⣿⠀⠀⡌⠱⠀⡀⠀⠀⢀⢮⡱⠎⠛⠉⣀⣤⠀⠠⣄⣄⣀⣈⣉⣉⡉⠉⣁⣁⠈⠀⠀⠀⠀⠀⠀⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⢄⠡⢂⠐⠠⠀⠂⠠⠐⠀⢁⠨⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⣸⣿⠇⠀⣸⠀⢀⡠⠀⠀⢠⡐⠂⠀⠐⠂⠀⠀⠈⠑⠲⢤⣈⠉⠙⠋⣁⡤⠴⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡐⢌⡰⢀⠎⡠⢁⡄⢡⠐⠈⢀⢸⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⣿⡏⠀⠀⠃⢠⣾⠃⠀⠀⠰⡰⠀⠆⠲⠖⠚⠉⠉⠀⠀⠀⠈⠛⠛⠛⠉⠀⠀⠀⠉⠙⠒⠐⠂⠀⠀⠀⠁⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣾⣯⣿⣷⣻⣮⡳⢎⠐⣄⢺⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⠸⠟⣁⣠⣴⣾⣿⡟⠀⠀⠀⣡⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⡀⣄⣀⢀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⡇⡈⢦⣹⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⢁⣤⣶⣿⣿⣿⣿⣿⠏⠀⠀⠀⠐⢀⣠⣤⠆⡄⠀⠀⠀⠀⠀⢰⢫⠞⡵⢊⡜⢯⡛⠅⠀⠀⠀⠀⠀⠀⠀⠀⢀⡀⠀⠀⠀⠸⣿⣿⣿⣿⣿⣿⣿⡙⢿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿⣿⢂⠱⣎⡼⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⠉⢉⣀⣤⣴⣶⣿⣿⣿⣿⣿⣿⠏⠁⠀⠀⠀⠀⣠⢾⠟⠁⠀⠀⠀⠀⠈⠐⠀⠀⠀⠀⠈⠁⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠁⡃⠀⠀⠀⠀⠙⠻⣿⣿⣿⣿⣿⣶⣌⣉⠻⣿⣿⣿⣿⣟⣿⣿⣿⢂⡳⣜⡼⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣥⣴⣶⣿⣿⣿⣿⠿⠛⣿⣿⣿⠿⠋⠀⠀⠀⠀⠀⠀⢸⠅⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⢿⣿⣿⣿⣿⢞⣿⣿⣿⣿⣿⣿⣿⣿⣆⣟⣼⣻⣿
    ⣯⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠛⠛⢉⡁⢤⢤⢒⡹⠏⠃⠀⡀⠀⠀⠀⠀⠀⠀⠸⡌⠀⠀⣴⠇⠀⠀⢸⣀⠀⠀⠀⠠⠀⢀⣤⣄⠀⠀⠀⠀⠀⣰⣿⠀⠀⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠤⡛⢟⠉⠀⢚⠿⡟⣻⠟⡛⡿⢿⠿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⡿⠿⡛⠏⠉⠁⠀⠀⠀⠀⠒⠈⠈⠀⠌⢂⠀⢀⡐⠂⡄⠀⠀⠀⠀⠀⠀⠸⠀⣀⠀⠻⣦⠀⢀⣴⠇⠀⠀⠀⣠⣶⣿⣿⣿⣶⠀⠀⠀⠀⠉⠻⢄⢀⠀⠏⠀⠀⠀⠀⠀⠀⣀⣀⣤⣤⣄⠈⠀⠑⠈⢂⠀⠀⠁⠈⠁⢋⠴⣉⠦⡑⢤⡈⢆⡡⢛
    ⠫⠙⠘⡀⠀⠂⠐⠈⠀⠀⠀⠀⠀⠀⠀⠀⢀⠁⠈⠀⠀⢂⠄⢣⠀⣧⣤⣤⡀⠀⠀⠘⢠⣟⡀⠀⠀⠀⠀⠀⠀⠀⣰⣾⣿⣿⣿⣿⣿⣏⠳⠀⠈⠀⠀⠀⠀⠀⠀⠀⢀⠀⠄⠀⠀⢸⣿⣿⣿⣿⠿⠓⠀⠀⠀⠀⡈⠐⠢⠄⡠⠀⠀⠁⢈⠑⠂⠘⠠⢌⠡
    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠠⠀⠈⠄⢃⠰⣩⠛⣏⠇⠀⠀⢈⠜⣻⣦⡀⠠⢀⠄⠀⢠⣼⣳⣏⣷⣿⣿⣿⣿⣿⣤⣀⠀⠀⠀⠀⠀⠠⠐⡈⠄⡈⠄⠀⠀⣼⣿⡿⢏⠍⠀⢀⠠⠁⠀⠀⠀⠐⠀⠂⠄⠁⠄⠀⠀⠀⠀⠀⠀⠀⠀
    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣀⣀⣠⣀⣀⡈⠠⠑⢄⠃⠈⠀⠀⠀⠎⢆⠈⠿⣿⣄⡈⢀⣴⣿⡻⠿⠛⠻⠿⠿⠿⠛⠛⠛⠛⠛⠀⢶⣦⣄⡡⢒⡀⠆⡁⢂⠀⠀⠈⠒⠁⠀⡠⠐⠌⢀⣀⣀⣠⣀⣄⡀⠁⠠⠁⡈⠄⠀⠀⠀⠀⠀⠀⠀
    ⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠢⣤⣀⡀⠀⠀⠘⡷⠀⠈⠄⡀⠓⢎⡿⣽⣿⣏⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡲⣇⠧⠘⠠⠐⠀⠀⠀⠀⠀⣀⠀⠀⣁⣠⣤⣿⣿⣿⡆⠁⢀⠠⡐⠠⠀⠀⠀⣠⣤⣀⡀⠀⠀
    ⠀⠀⣀⣠⣴⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⢸⠿⣿⣿⣦⠀⠄⡀⠀⠀⠀⠀⠈⢞⣿⣿⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢺⡵⡛⠄⠁⠀⠀⠀⠀⠀⣰⣾⣿⣿⣶⣿⣿⣿⣿⣿⣿⣿⣿⠀⠠⢁⠂⠁⠀⢤⠀⣿⠿⣿⣿⣿⣿
    ⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠱⣿⣿⣿⣧⣴⣶⠀⠀⠀⠂⠀⠘⡼⣿⣿⡅⢀⡶⣴⣦⣤⣄⣀⡤⣤⠄⡀⠀⠀⠷⠁⡀⠂⠄⠃⠀⣠⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠀⢀⣠⠄⣾⠃⠜⠀⠈⠀⠀⠀⠀⠀
    ⣿⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣷⡀⠹⣿⣿⣿⣿⠏⠀⢀⠡⠘⡄⠀⠘⢵⠻⢭⢫⣼⣷⣿⣿⣿⣿⡿⣿⣦⡀⠀⠀⠈⠀⠄⡉⠌⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠂⢸⣿⠀⣿⠀⡃⠀⠀⠀⠀⠀⠀⠀
    ⣿⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠻⢿⣿⣿⣷⡀⠀⢃⠱⢌⡒⣀⠈⠓⠎⠋⠉⠉⠉⠉⠉⠉⠀⠁⠘⠑⠀⠀⠀⠀⠀⠱⠂⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⠛⠁⢀⠲⠜⢀⣿⡏⢸⡟⠀⠄⠀⠄⠀⠠⠀⠀⠄
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⠀⠻⣿⣿⣿⣆⠀⠍⣆⠸⡄⠀⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⠠⣍⠀⣰⣿⣿⣿⣿⣿⣿⣿⢛⣿⠟⠀⠀⠠⠀⠈⠃⠀⠚⣻⠁⢛⣣⣤⣤⣀⣠⣤⡀⠀⣀⣠
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡀⠙⣿⣿⣿⡀⠈⢒⠧⣜⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢞⠄⢠⣿⣿⣿⣿⣿⣿⡿⣡⣾⠏⠀⣀⣀⣤⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⠈⠙⠉⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠻⣿⣿⣿⣿⣦⡈⠿⢿⡇⠀⠈⢙⢦⡁⠀⢠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⠏⠀⣸⣿⣿⣿⣿⣤⡥⠀⠘⠁⠲⢞⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣦⣄⣀⠀⠘⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠛⠟⠛⢋⣡⣤⣤⣾⣷⠀⠀⠈⢎⠷⢀⡓⢮⠐⡀⠀⡀⣀⠀⢀⠠⣄⠰⠂⢠⡙⠀⠀⣿⣿⣿⣿⣿⡇⣾⡿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣼⣿⣆⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣿⣿⣿⡇⣿⣿⡀⠀⠀⠀⢻⠀⢝⢢⠑⢌⠱⠰⢄⡉⠆⡱⠀⡀⢆⠡⠂⠀⠀⣿⣿⣿⣿⣿⠀⢹⠁⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⢿⣻⢿⣿⡀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠹⣿⣷⡀⠀⠀⠀⠁⠈⠢⡉⢄⠢⠄⠠⡘⢠⠐⠠⠁⠂⠀⠀⠀⣰⣿⣿⣿⣿⣿⣦⠀⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⢎⢏⡳⣋⢾⡁⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⠹⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠐⠈⠐⠀⠀⠈⠀⠀⠀⠀⣀⣴⣿⣿⣿⣿⣿⣿⠻⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⠈⠆⠱⡈⠦⡁⠐⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠙⣿⣿⣷⣦⣤⣄⣀⣀⣀⡀⠀⠀⠀⢀⣀⣠⣤⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⠃⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⠀⠌⡐⠠⢁⠐⠀⣿⣿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣭⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⠀⠠⠀⠂⠀⠂⠀⢻⣿⣷⡈⣿⣟⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣄⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣀⣀⠀⠀⠁⠀⠀⠘⣿⣿⣿⣿⣿⣧⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⡿⣹⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣷⣄⡀⠀⠹⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⣾⡟⢠⣿⢿⣾⣿⣿⣿⣿⠋
    ⣿⣿⣿⣿⣿⣿⣿⣶⡀⠻⣿⡇⢿⣿⣷⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⠏⣰⡟⢠⡿⠋⣼⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⡿⢷⡦⠟⠃⠈⠻⣿⠀⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢸⣿⣿⠏⢰⣿⡴⠊⠀⠀⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⠿⠋⠀⠈⡁⠀⡀⠀⠀⠀⢆⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⣼⣿⣿⣿⣿⠀⣿⣿⡇⢀⣾⠛⢀⠀⠀⠀⣽⣿⣿⣿⣿⣿⣿
    ⣿⣿⡿⠛⣁⣠⣄⣠⠀⠉⠁⠀⡀⠀⣤⠈⠆⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⣰⣿⣿⣿⣿⡏⢠⣿⣿⡄⠊⢁⡼⣫⠀⠀⠀⣿⣿⣿⣿⣿⣷⣝
    ⠿⠋⠀⠾⣿⣿⠿⠋⠀⡀⢀⠀⠀⠈⢁⡁⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿⣿⣿⣿⣿⣿⡟⢀⣿⣿⣿⣿⣿⠁⣾⣿⣿⠁⢠⢏⡶⠁⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿
    ⣤⣤⣤⣤⣄⣀⣀⣀⠀⠀⠚⠀⠠⠄⠀⠉⢁⡀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢰⣿⣿⣿⣿⣿⣿⡟⠀⣼⣿⣿⣿⣿⡏⢠⣿⣿⠃⢀⡠⢤⠤⠀⠄⠀⠀⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣤⣉⣉⣛⠛⠻⠿⠿⢿⣷⣶⣶⣶⣦⣤⣤⣀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⣿⠁⣼⣿⣿⣿⣿⣿⠃⣼⣿⠏⠀⠀⠀⠉⠀⣀⢀⣀⣠⣾⣿⣿⣿⣿⣿⣿
    ⠻⣿⢻⣿⠿⠻⣿⣿⣷⣶⣤⣌⣉⣛⢛⠛⠻⠿⣿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠃⣼⣿⣿⣿⣿⣿⠇⣰⣿⣿⠀⠐⠻⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⠀⠉⠈⠙⠛⠳⠷⢿⣽⣿⣿⣿⡿⣿⣿⣿⣿⣶⣦⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⣰⣿⣿⣿⣿⣿⡟⢰⣿⣿⣿⠀⡐⢢⠀⡄⡀⣈⢉⠛⠛⠿⠿⣿⣿⣿⣿⣿
    ⠹⣌⢣⢇⠶⣠⢄⡄⣀⠈⠉⠙⠛⠛⠷⢶⣭⣿⣟⡂⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢿⠿⢿⠁⠰⢿⡿⢿⣿⣟⡝⢁⣾⣿⣿⡇⠀⡐⡡⢊⠔⡡⢎⣏⣟⡳⣶⢶⣾⣿⣿⣿⣿
    ⠈⢶⡩⢎⡳⡱⢎⡼⢡⢏⡝⢣⢖⡰⢢⡀⢀⠈⠉⠉⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿⠿⠿⣿⢿⣿⣿⣿⣿⣿⡿⣚⢰⣾⢿⣷⣿⣽⠟⠠⢃⣿⣿⣿⠁⠀⡔⢡⠎⡜⠴⣋⠶⣭⢳⣏⡿⣾⢽⣿⣿⣿
  ";

    Console.WriteLine(sadFace);
  }

  public static void PrintHistory()
  {
    Console.Clear();
    Console.WriteLine("==== Game history ====\n");

    if (history.Count < 1)
    {
      Console.WriteLine("No games recorded.");
    }
    else
    {
      for (int i = 0; i < history.Count; i++)
      {
        Console.Write($"{i + 1}. {history[i].Score} total points ");
        Console.WriteLine($"on {(history[i].Difficulty == MathGameDifficulty.Normal ? "Normal" : "Advanced")} mode.");
      }
    }

    Console.Write("\nPress enter to continue...");
    Console.ReadLine();
  }

  public static void AddToHistory(int score, MathGameDifficulty difficulty)
  {
    history.Add(new { Score = score, Difficulty = difficulty });
  }
}