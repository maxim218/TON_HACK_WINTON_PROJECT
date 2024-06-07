using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FruitCatcher
{
    public class MainManager : MonoBehaviour
    {
        private bool _allowPlaying = false;

        private const int BonusTakePoints = 10;
        private const int NegativePointsHitWithEnemy = -50;

        private const float StartSpeed = -4.5f;
        private const float Acceleration = -1.25f;
        private const float MaxSpeed = -14.5f;

        private float _speed = StartSpeed;

        [Header("Bot Manager")] [SerializeField]
        private BotManager botManager = null;

        [Header("Finish Game Manager")] [SerializeField]
        private FinishGameManager finishGameManager = null;

        [Header("Timer manager")] [SerializeField]
        private TimerManager timerManager = null;

        [Header("Level builder")] [SerializeField]
        private LevelBuildManager levelBuildManager = null;

        [Header("Main hero")] [SerializeField] private GameObject hero = null;

        [Header("UI score labels")] [SerializeField]
        private Text scoreHeroLabel = null;

        [SerializeField] private Text scoreEnemyLabel = null;

        [Header("UI timer label")] [SerializeField]
        private Text timerLabel = null;

        [Header("Ground Decoration")] [SerializeField]
        private GameObject groundDecoration = null;
        
        private ScoreController _heroScoreController = null;
        private ScoreController _enemyScoreController = null;
        private HeroController _heroController = null;

        private void Awake()
        {
            _heroScoreController = new ScoreController(scoreHeroLabel);
            _enemyScoreController = new ScoreController(scoreEnemyLabel);
            _heroController = new HeroController(hero);
            timerManager.InitTimerParams(timerLabel, OnGameFinish);
        }

        private IEnumerator Start()
        {
            int rnd = Random.Range(2000, 8000);
            int levelIndex = rnd % 4;
            List<int> levelContentList = Levels.GetLevel(levelIndex);
            levelBuildManager.BuildLevel(levelContentList);

            while (!_allowPlaying)
                yield return null;

            timerManager.BeginTimerCounting();
            botManager.BeginBotWorking(_enemyScoreController);
        }

        private void Update()
        {
            if (!_allowPlaying)
                return;

            IEnumerable<BonusController> bonusesList = levelBuildManager.GetBonuses();
            IEnumerable<EnemyController> enemiesList = levelBuildManager.GetEnemies();

            if (bonusesList is null) return;
            if (enemiesList is null) return;

            OnUpdateForBonuses(bonusesList);
            OnUpdateForEnemies(enemiesList);
            groundDecoration.transform.Translate(0, 0, _speed * Time.deltaTime);

            OnUpdateSpeedChange();
        }

        private void OnUpdateSpeedChange()
        {
            if (_speed > MaxSpeed)
                _speed += (Acceleration * Time.deltaTime);
            else
                _speed = MaxSpeed;
        }

        private void OnUpdateForBonuses(IEnumerable<BonusController> bonusesList)
        {
            foreach (BonusController bonus in bonusesList)
            {
                if (bonus.gameObject.activeSelf)
                {
                    bonus.gameObject.transform.Translate(0, _speed * Time.deltaTime, 0);
                    bool isHit = HitCheckController.IsHit(_heroController, bonus.GetPosY(), bonus.GetTypePosition());
                    if (isHit)
                    {
                        bonus.OnHitWithHero();
                        _heroScoreController.AddToScore(BonusTakePoints);
                    }
                }
            }
        }

        private void OnUpdateForEnemies(IEnumerable<EnemyController> enemiesList)
        {
            foreach (EnemyController enemy in enemiesList)
            {
                if (enemy.gameObject.activeSelf)
                {
                    enemy.gameObject.transform.Translate(0, _speed * Time.deltaTime, 0);
                    bool isHit = HitCheckController.IsHit(_heroController, enemy.GetPosY(), enemy.GetTypePosition());
                    if (isHit)
                    {
                        enemy.OnHitWithHero();
                        _heroScoreController.AddToScore(NegativePointsHitWithEnemy);
                    }
                }
            }
        }

        public void OnUserScreenClick(int direction)
        {
            if (!_allowPlaying)
                return;
            _heroController.MoveHero(direction);
        }

        private void OnGameFinish()
        {
            _allowPlaying = false;
            finishGameManager.OnFinishGame(_heroScoreController, _enemyScoreController);
            botManager.StopBotWorking();
        }

        public void RunStartGamePlay() => _allowPlaying = true;
    }
}