using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIsetup : MonoBehaviour
{
    [SerializeField]
    private Image bossHealth;
    [SerializeField]
    private TextMeshProUGUI bossNameText;
    private string bossname;
    [SerializeField]
    private TextMeshProUGUI bossTitleText;
    private string bosstitle;
    [SerializeField]
    private TextMeshProUGUI playerHealthText;
    private bool bRefilling = false;
    private float refillTimer = 0;
    private void OnEnable()
    {
        /*//if(FindFirstObjectByType<Boss>().bossHealthBar)
            FindFirstObjectByType<Boss>().bossHealthBar = bossHealth;
        //if(FindFirstObjectByType<PlayerHealth>().playerHPText)
            FindFirstObjectByType<PlayerHealth>().playerHPText = playerHealthText;*/
        ResetUIComponents();
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
            }

        }
        
    }
    public void RefillBar()
    {
        ResetUIComponents();
        refillTimer = 0;
        bRefilling = true;
    }

    private void ResetUIComponents()
    {
        FindFirstObjectByType<Boss>().bossHealthBar = bossHealth;
        bossNameText.text = FindFirstObjectByType<Boss>().bossName;
        bossTitleText.text = FindFirstObjectByType<Boss>().bossTitle;
        FindFirstObjectByType<PlayerHealth>().playerHPText = playerHealthText;
    }
}
