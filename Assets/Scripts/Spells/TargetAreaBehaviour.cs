using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(menuName = "Game Data/Targeting Behaviours/Target Area Behavior")]
    public class TargetAreaBehaviour : TargetingBehaviour
    {
        public int maxTargets;
        public float hitRadius;
        public LayerMask hitmask;

        public override List<Unit> GetTargets(Vector3 position)
        {
            Collider2D[] colliders = new Collider2D[maxTargets];
            var returnList = new List<Unit>();
            if (Physics2D.OverlapCircleNonAlloc(position, hitRadius, colliders, hitmask) == 0) return returnList;

            foreach (var collider in colliders)
            {
                if (collider == null || !collider.TryGetComponent<Unit>(out var component)) continue;
                if (returnList.Contains(component)) continue;

                returnList.Add(component);
            }

            return returnList;
        }
    }
}
