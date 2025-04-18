using UnityEngine;
using UnityEngine.UI;

public class EXPSystem : MonoBehaviour
{
    public int currentEXP = 0;
    public int currentLevel = 1;
    public int expToNextLevel = 100;

    public Slider expBar; // UI Slider for EXP
    public Text levelText; // UI Text to show current level

    void Start()
    {
        UpdateUI();
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
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.5f); // Scales difficulty
        Debug.Log("Leveled up to: " + currentLevel);

        // You can add level-up bonuses here:
        // Example: PlayerController.instance.IncreaseFireRate();
    }

    void UpdateUI()
    {
        if (expBar != null)
            expBar.value = (float)currentEXP / expToNextLevel;

        if (levelText != null)
            levelText.text = "Level: " + currentLevel;
    }
}
