using UnityEngine;
using UnityEngine.UI;

namespace AimShooter
{
    public class RendererInfoScoreAndTurns : MonoBehaviour
    {
        private const string PrefixScore = "Score: ";
        private const string PrefixTurns = "Turns: ";

        [Header("Player labels")] [SerializeField]
        private Text userScoreLabel = null;

        [SerializeField] private Text userTurnsLabel = null;

        [Header("Enemy labels")] [SerializeField]
        private Text enemyScoreLabel = null;

        [SerializeField] private Text enemyTurnsLabel = null;

        public void RenderUserScore(int score) => userScoreLabel.text = $"{PrefixScore}{score}";
        public void RenderUserTurns(int turns) => userTurnsLabel.text = $"{PrefixTurns}{turns}";
        public void RenderEnemyScore(int score) => enemyScoreLabel.text = $"{PrefixScore}{score}";
        public void RenderEnemyTurns(int turns) => enemyTurnsLabel.text = $"{PrefixTurns}{turns}";
    }
}