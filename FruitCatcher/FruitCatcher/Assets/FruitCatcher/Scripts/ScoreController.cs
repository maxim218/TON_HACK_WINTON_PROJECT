using UnityEngine.UI;

namespace FruitCatcher
{
    public class ScoreController
    {
        private int _score = 0;
        private readonly Text _label = null;

        public ScoreController(Text label)
        {
            _score = 0;
            _label = label;
        }

        public void AddToScore(int delta)
        {
            _score += delta;
            _label.text = $"{_score}";
        }

        public int GetScore() => _score;
    }
}