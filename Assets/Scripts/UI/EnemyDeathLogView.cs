using TMPro;
using UnityEngine;

namespace UI
{
    public class EnemyDeathLogView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI logText;
        
        public void LogEnemyDeath(string enemyName)
        {
            logText.text += $"{enemyName} Died!\n";
        }
    }
}
