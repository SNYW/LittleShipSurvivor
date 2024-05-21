using TMPro;
using UnityEngine;
using Utilities;

public class EnemyUnit : Unit
{
    public TMP_Text debugDistanceText;

    private void LateUpdate()
    {
        debugDistanceText.text = ((Vector2)transform.position).FastDistance(GameManager.playerShip.transform.position).ToString();
    }
}
