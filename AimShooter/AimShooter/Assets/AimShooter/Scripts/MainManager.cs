using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AimShooter
{
    public class MainManager : MonoBehaviour
    {
        [Header("Finish Game Manager")] [SerializeField]
        private FinishGameManager finishGameManager = null;

        [Header("Bot Manager")] [SerializeField]
        private BotManager botManager = null;

        [Header("Animator Moving")] [SerializeField]
        private Animator animatorMoving = null;

        [Header("Timer Manager")] [SerializeField]
        private TimerManager timerManager = null;

        [Header("Timer Label")] [SerializeField]
        private Text timerLabel = null;

        [Header("Delay seconds between shooting")] [SerializeField]
        private float delaySecondsBetweenFire = 1f;

        [Header("Render score and turns")] [SerializeField]
        private RendererInfoScoreAndTurns rendererInfoScoreAndTurns = null;

        [Header("Shoot Manager")] [SerializeField]
        private ShootManager shootManager = null;

        [Header("Moving Camera Manager")] [SerializeField]
        private MovingCameraManager _movingCameraManager = null;

        [Header("Main Camera")] [SerializeField]
        private Camera mainCamera = null;

        [Header("Weapon")] [SerializeField] private GameObject weapon = null;

        [Header("Aim UI")] [SerializeField] private GameObject aimUI = null;

        private ScoreDataController _playerScoreController = null;
        private ScoreDataController _enemyScoreController = null;

        private bool _allowStartGame = false;

        private void Awake()
        {
            _playerScoreController = new ScoreDataController(RenderInfoInTopBlock);
            _enemyScoreController = new ScoreDataController(RenderInfoInTopBlock);
            timerManager.InitTimerParams(timerLabel, OnTimerFinishCounting);
            _movingCameraManager.SetCamera(mainCamera);
            botManager.SetBotTurnsNumber(5);
            botManager.SetBotScoreController(_enemyScoreController);
            botManager.SetActionAfterBotTurn(RenderInfoInTopBlock);
        }

        private IEnumerator Start()
        {
            _movingCameraManager.SetAllowMoving(true);
            _playerScoreController.SetScore(0);
            _enemyScoreController.SetScore(0);
            RenderInfoInTopBlock();
            SetActiveOfFireWeaponAim(false);

            while (!_allowStartGame)
                yield return null;

            SetActiveOfFireWeaponAim(true);
            timerManager.BeginTimerCounting();
            RenderInfoInTopBlock();
            botManager.RunBot();
        }

        public void RunAllowStartGameTrue() => _allowStartGame = true;

        private void OnTimerFinishCounting()
        {
            animatorMoving.enabled = false;
            SetActiveOfFireWeaponAim(false);
            RenderInfoInTopBlock();
            const string message = "Timer finished counting";
            Debug.Log(message);
            finishGameManager.FinishGameRun(_playerScoreController, _enemyScoreController);
        }

        public void OnUserClickedToScreen()
        {
            if (animatorMoving.enabled)
            {
                if (weapon.activeSelf)
                {
                    bool isFireAllowedFlag = shootManager.IsFireAllowed();
                    if (isFireAllowedFlag)
                    {
                        Vector3 fireHitPosition = _movingCameraManager.GetHitPosition();
                        shootManager.CreateBullet(fireHitPosition, _playerScoreController);
                        SetActiveOfFireWeaponAim(false);
                        StartCoroutine(AsyncReloadArrow());
                    }

                    RenderInfoInTopBlock();
                }
            }
        }

        private void RenderInfoInTopBlock()
        {
            int playerTurns = shootManager.GetCanFireNumber();
            rendererInfoScoreAndTurns.RenderUserTurns(playerTurns);

            int playerScore = _playerScoreController.GetScore();
            rendererInfoScoreAndTurns.RenderUserScore(playerScore);

            int enemyScore = _enemyScoreController.GetScore();
            rendererInfoScoreAndTurns.RenderEnemyScore(enemyScore);

            int enemyTurns = botManager.GetTurns();
            rendererInfoScoreAndTurns.RenderEnemyTurns(enemyTurns);
        }

        private void SetActiveOfFireWeaponAim(bool active)
        {
            weapon.SetActive(active);
            aimUI.SetActive(active);
        }

        private IEnumerator AsyncReloadArrow()
        {
            if (animatorMoving.enabled)
            {
                yield return new WaitForSeconds(delaySecondsBetweenFire);
                bool isFireAllowedFlag = shootManager.IsFireAllowed();
                if (animatorMoving.enabled)
                    if (isFireAllowedFlag)
                        SetActiveOfFireWeaponAim(true);
            }
        }
    }
}