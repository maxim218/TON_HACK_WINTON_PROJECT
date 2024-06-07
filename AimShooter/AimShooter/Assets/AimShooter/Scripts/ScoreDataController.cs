using System;

namespace AimShooter
{
    public class ScoreDataController
    {
        private int _score = 0;
        private readonly Action _onScoreChanged = null;

        public ScoreDataController(Action onScoreChangedAction)
        {
            _score = 0;
            _onScoreChanged = onScoreChangedAction;
        }

        public void SetScore(int value)
        {
            _score = value;
            _onScoreChanged?.Invoke();
        }

        public void AddScore(int value)
        {
            _score += value;
            _onScoreChanged?.Invoke();
        }

        public int GetScore() => _score;
    }
}