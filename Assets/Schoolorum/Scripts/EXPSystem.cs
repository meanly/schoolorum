using UnityEngine;
using UnityEngine.UI;

public class EXPSystem : MonoBehaviour
{
    public int currentEXP = 0;
    public int currentLevel = 1;
    public int expToNextLevel = 100;

    public Slider expBar;
    public Text levelText;
    public Text upgradeText;

    public Player player; // Assign via Inspector

    private bool canUpgrade = false;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        // No longer needed, because upgrades happen via UpgradeUI button clicks
        // if (canUpgrade) { ... input checks ... }
    }
    public void GainEXP(int amount)
    {
        currentEXP += amount;
        while (currentEXP >= expToNextLevel)
        {
            currentEXP -= expToNextLevel;
            LevelUp();
        }
        UpdateUI();
    }

    void LevelUp()
    {
        currentLevel++;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.5f);
        Debug.Log("Leveled up to: " + currentLevel);

        Time.timeScale = 0f; // Pause game
        UpgradeUI.instance.ShowUpgradePanel(); // Show upgrade UI
        canUpgrade = true;
    }

    void ShowUpgradeOptions()
    {
        canUpgrade = true;
        if (upgradeText != null)
        {
            upgradeText.text = "LEVEL UP!\nPress:\n1 - Damage\n2 - Max HP\n3 - Speed\n4 - Attack Speed";
            upgradeText.gameObject.SetActive(true);
        }
    }

    void ConfirmUpgrade(string message)
    {
        canUpgrade = false;
        if (upgradeText != null)
        {
            upgradeText.text = message;
            Invoke(nameof(HideUpgradeText), 2f);
        }

        Game.game.Shake(0.2f, 0.2f, 40f);
    }

    void HideUpgradeText()
    {
        if (upgradeText != null)
            upgradeText.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        if (expBar != null)
            expBar.value = (float)currentEXP / expToNextLevel;

        if (levelText != null)
            levelText.text = "Level: " + currentLevel;
    }
}
