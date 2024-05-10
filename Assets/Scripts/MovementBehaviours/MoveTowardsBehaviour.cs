using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;

namespace MovementBehaviours
{
    [CreateAssetMenu(fileName = "new Move Towards Behaviour", menuName = "Game Data/Move Towards Behaviour")]
    public class MoveTowardsBehaviour : MovementBehaviour
    {
        public MoveTarget moveTarget;
        
        [ShowIf("moveTarget", MoveTarget.NearestInMask)] 
        public float enemyCheckRadius;
        [ShowIf("moveTarget", MoveTarget.NearestInMask)] 
        public LayerMask enemyLayerMask;
        
        public enum MoveTarget
        {
            Player,
            NearestInMask
        }
    
        public override Vector3 GetTargetDirection(Transform t)
        {
            return moveTarget switch
            {
                MoveTarget.Player => GetDirectionToPlayer(t),
                MoveTarget.NearestInMask => GetNearestEnemyDirection(t),
                _ => Vector3.zero
            };
        }

        private Vector3 GetDirectionToPlayer(Transform t)
        {
            if (GameManager.playerShip == null) return t.position;

            return GameManager.playerShip.transform.position - t.position;
        }

        private Vector3 GetNearestEnemyDirection(Transform t)
        {
            Collider2D[] colliders = new Collider2D[5];
            if (Physics2D.OverlapCircleNonAlloc(t.position, enemyCheckRadius, colliders, enemyLayerMask) == 0) return Vector3.zero;

            var nearestPos = ((Vector2)t.position).GetClosestCollider2D(colliders).transform.position;

            return nearestPos - t.position;
        }
    }
}
