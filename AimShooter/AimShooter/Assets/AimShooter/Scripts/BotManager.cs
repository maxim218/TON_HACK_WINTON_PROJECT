using System;
using System.Collections;
using UnityEngine;

namespace AimShooter
{
    public class BotManager : MonoBehaviour
    {
        private const float DelaySeconds = 4f;

        private ScoreDataController _scoreController = null;
        private int _turnsNumber = 0;
        private Action _onAfterBotMakeTurn = null;

        public int GetTurns() => _turnsNumber;
        public void SetBotScoreController(ScoreDataController controller) => _scoreController = controller;
        public void SetBotTurnsNumber(int turnsValue) => _turnsNumber = turnsValue;
        public void SetActionAfterBotTurn(Action actionAfterTurn) => _onAfterBotMakeTurn = actionAfterTurn;
        public void RunBot() => StartCoroutine(AsyncBotWorking());

        private void ChangeBotScore()
        {
            int rnd = UnityEngine.Random.Range(2000, 8000);
            int deltaScore = (rnd % 3 == 0) ? 8 : 6;
            _scoreController?.AddScore(deltaScore);
        }

        private IEnumerator AsyncBotWorking()
        {
            while (true)
            {
                if (_turnsNumber <= 0) yield break;

                yield return new WaitForSeconds(DelaySeconds);
                _turnsNumber -= 1;
                ChangeBotScore();
                _onAfterBotMakeTurn?.Invoke();

                if (_turnsNumber <= 0) yield break;
            }
        }
    }
}