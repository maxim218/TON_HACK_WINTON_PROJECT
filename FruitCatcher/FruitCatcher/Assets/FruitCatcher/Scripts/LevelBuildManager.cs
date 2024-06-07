using System.Collections.Generic;
using UnityEngine;

namespace FruitCatcher
{
    public class LevelBuildManager : MonoBehaviour
    {
        private const float Z = 5;

        private const float StartY = 2;
        private const float DeltaY = 4;

        private const float Left = -1.5f;
        private const float Center = 0;
        private const float Right = 1.5f;

        [Header("Bonus")] [SerializeField] private BonusController bonusPrefab = null;

        [Header("Enemy")] [SerializeField] private EnemyController enemyPrefab = null;

        private readonly List<BonusController> bonusesList = new List<BonusController>();
        private readonly List<EnemyController> enemiesList = new List<EnemyController>();

        public IEnumerable<BonusController> GetBonuses() => bonusesList;
        public IEnumerable<EnemyController> GetEnemies() => enemiesList;

        private void CreateBonus((Vector3, TypePosition) tuple, int index)
        {
            var (position, typePosition) = tuple;
            BonusController script = Instantiate(bonusPrefab);
            GameObject g = script.gameObject;
            g.transform.position = position;
            g.name = $"Bonus-{index}";
            script.SetTypePosition(typePosition);
            bonusesList.Add(script);
        }

        private void CreateEnemy((Vector3, TypePosition) tuple, int index)
        {
            var (position, typePosition) = tuple;
            EnemyController script = Instantiate(enemyPrefab);
            GameObject g = script.gameObject;
            g.transform.position = position;
            g.name = $"Enemy-{index}";
            script.SetTypePosition(typePosition);
            enemiesList.Add(script);
        }

        private static (Vector3, TypePosition) GetPosition(TypePosition typePosition, float y)
        {
            Vector3 position = Vector3.zero;
            position.x = typePosition switch
            {
                TypePosition.Left => Left,
                TypePosition.Center => Center,
                TypePosition.Right => Right,
                _ => position.x
            };
            position.y = y;
            position.z = Z;
            (Vector3, TypePosition) resultTuple = (position, typePosition);
            return resultTuple;
        }

        public void BuildLevel(List<int> elementsList)
        {
            float y = StartY;
            int index = 0;

            foreach (int element in elementsList)
            {
                switch (element)
                {
                    case 1:
                        CreateBonus(GetPosition(TypePosition.Left, y), index);
                        break;
                    case 2:
                        CreateBonus(GetPosition(TypePosition.Center, y), index);
                        break;
                    case 3:
                        CreateBonus(GetPosition(TypePosition.Right, y), index);
                        break;
                    case 4:
                        CreateEnemy(GetPosition(TypePosition.Left, y), index);
                        break;
                    case 5:
                        CreateEnemy(GetPosition(TypePosition.Center, y), index);
                        break;
                    case 6:
                        CreateEnemy(GetPosition(TypePosition.Right, y), index);
                        break;
                }

                index++;
                y += DeltaY;
            }
        }
    }
}