using UnityEngine;
using UnityEngine.UI;

namespace FruitCatcher
{
    public class FinishGameManager : MonoBehaviour
    {
        [Header("Finish Game Block")] [SerializeField]
        private GameObject block = null;

        [Header("Score labels")] [SerializeField]
        private Text labelHeroScore = null;

        [SerializeField] private Text labelEnemyScore = null;

        public void OnFinishGame(ScoreController heroScoreController, ScoreController enemyScoreController)
        {
            int heroScore = heroScoreController.GetScore();
            labelHeroScore.text = $"{heroScore}";

            int enemyScore = enemyScoreController.GetScore();
            labelEnemyScore.text = $"{enemyScore}";

            block.SetActive(true);
            ResultGameMesseger.SendGameResult("FruitCatcher", heroScore, enemyScore);
        }
    }
}