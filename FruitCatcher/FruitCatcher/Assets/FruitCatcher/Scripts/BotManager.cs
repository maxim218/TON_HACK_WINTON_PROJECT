using System.Collections;
using UnityEngine;

namespace FruitCatcher
{
    public class BotManager : MonoBehaviour
    {
        private const float DelayLoopSeconds = 0.85f;
        private const int BonusTakePoints = 10;
        private const int NegativePointsHitWithEnemy = -50;

        private bool _loopBotWorking = false;
        private ScoreController _enemyScoreController = null;

        private static int GetRandomDeltaScore()
        {
            int rnd = Random.Range(2000, 8000);
            int value = rnd % 10;
            return (value < 7) ? BonusTakePoints : NegativePointsHitWithEnemy;
        }

        private IEnumerator AsyncScoreChanging()
        {
            while (_loopBotWorking)
            {
                yield return new WaitForSeconds(DelayLoopSeconds);
                if (!_loopBotWorking) yield break;

                int currentScore = _enemyScoreController.GetScore();
                int deltaScore = (currentScore > 50) ? GetRandomDeltaScore() : BonusTakePoints;
                _enemyScoreController.AddToScore(deltaScore);
            }
        }

        public void BeginBotWorking(ScoreController enemyScoreController)
        {
            _enemyScoreController = enemyScoreController;
            _loopBotWorking = true;
            StartCoroutine(AsyncScoreChanging());
        }

        public void StopBotWorking() => _loopBotWorking = false;
    }
}