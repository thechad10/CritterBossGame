using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIsetup : MonoBehaviour
{
    [SerializeField]
    private Image bossHealth;
    [SerializeField]
    private TextMeshProUGUI playerHealthText;
    private void OnEnable()
    {
        //if(FindFirstObjectByType<Boss>().bossHealthBar)
            FindFirstObjectByType<Boss>().bossHealthBar = bossHealth;
        //if(FindFirstObjectByType<PlayerHealth>().playerHPText)
            FindFirstObjectByType<PlayerHealth>().playerHPText = playerHealthText;
    }
}
