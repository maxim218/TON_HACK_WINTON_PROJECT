using UnityEngine;
using UnityEngine.UI;

namespace AimShooter
{
    public class FinishGameManager : MonoBehaviour
    {
        [Header("Block Finish Game")] [SerializeField]
        private GameObject block = null;

        [Header("Player Label")] [SerializeField]
        private Text playerLabel = null;

        [Header("Enemy Label")] [SerializeField]
        private Text enemyLabel = null;

        public void FinishGameRun(ScoreDataController playerScoreController, ScoreDataController enemyScoreController)
        {
            int playerScore = playerScoreController.GetScore();
            playerLabel.text = $"Player: {playerScore} points";

            int enemyScore = enemyScoreController.GetScore();
            enemyLabel.text = $"Enemy: {enemyScore} points";

            block.SetActive(true);
            ResultGameMesseger.SendGameResult("AimShooter", playerScore, enemyScore);
        }
    }
}