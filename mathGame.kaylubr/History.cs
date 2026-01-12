namespace MathGame;

internal static class History
{
  static List<GameRecord> history = new();

  internal static void AddToHistory(int score, MathGameDifficulty difficulty)
  {
    history.Add(new GameRecord(score, difficulty));
  }

  internal static void PrintHistory()
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
}