using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIsetup : MonoBehaviour
{
    [SerializeField]
    private Image bossHealth;
    //[SerializeField]
    //private Image playerAbilityRecharge;
    [SerializeField]
    private TextMeshProUGUI playerHealthText;
    private void OnEnable()
    {
        FindFirstObjectByType<Boss>().bossHealthBar = bossHealth;
        FindFirstObjectByType<PlayerHealth>().playerHPText = playerHealthText;
    }
}
