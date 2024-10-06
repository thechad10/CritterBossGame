using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIsetup : MonoBehaviour
{
    [SerializeField]
    private Image bossHealth;
    [SerializeField]
    private TextMeshProUGUI playerHealthText;
    private bool bRefilling = false;
    private float refillTimer = 0;
    private void OnEnable()
    {
        //if(FindFirstObjectByType<Boss>().bossHealthBar)
            FindFirstObjectByType<Boss>().bossHealthBar = bossHealth;
        //if(FindFirstObjectByType<PlayerHealth>().playerHPText)
            FindFirstObjectByType<PlayerHealth>().playerHPText = playerHealthText;
    }
    private void Update()
    {
        if (bRefilling)
        {
            refillTimer += Time.deltaTime;
            bossHealth.fillAmount = Mathf.Clamp(refillTimer, 0, 1);
            if (refillTimer >= 1)
            {
                refillTimer = 0;
                bRefilling = false;
                ResetUIComponents();
            }

        }
        
    }
    public void RefillBar()
    {
        refillTimer = 0;
        bRefilling = true;
    }

    private void ResetUIComponents()
    {
        FindFirstObjectByType<Boss>().bossHealthBar = bossHealth;
        FindFirstObjectByType<PlayerHealth>().playerHPText = playerHealthText;
    }
}
