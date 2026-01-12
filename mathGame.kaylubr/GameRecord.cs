namespace MathGame;

internal class GameRecord
{
  internal int Score { get; }
  internal MathGameDifficulty Difficulty { get; }

  internal GameRecord(int score, MathGameDifficulty difficulty)
  {
    Score = score;
    Difficulty = difficulty;
  }
}