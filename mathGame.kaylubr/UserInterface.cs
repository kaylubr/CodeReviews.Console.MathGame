namespace MathGame;

internal static class UserInterface
{
  internal static void RunGame()
  {
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
      choice = (Console.ReadLine() ?? string.Empty).Trim();

      if (choice == "6")
      {
        Console.WriteLine("\nExiting game..\n");
        break;
      }

      switch (choice)
      {
        case "1":
          MathGameController.PlayGame(MathOperation.Addition, "ADDITION ROUND");
          break;
        case "2":
          MathGameController.PlayGame(MathOperation.Subtraction, "SUBTRACTION ROUND");
          break;
        case "3":
          MathGameController.PlayGame(MathOperation.Multiplication, "MULTIPLICATION ROUND");
          break;
        case "4":
          MathGameController.PlayGame(MathOperation.Division, "DIVISION ROUND");
          break;
        case "5":
          History.PrintHistory();
          break;
      }
    }
  }
}