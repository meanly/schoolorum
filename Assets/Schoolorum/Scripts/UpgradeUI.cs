using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI instance;

    public GameObject upgradePanel;

    public Button damageButton;
    public Button healthButton;
    public Button speedButton;
    public Button attackSpeedButton;


    private Player player;

    void Awake()
    {
        // Proper singleton check
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Hide the upgrade panel initially
        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        // Find player instance
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("Player not found in scene! Please ensure a Player script is present.");
        }
    }

    public void ShowUpgradePanel()
    {
        if (upgradePanel == null) return;

        upgradePanel.SetActive(true);

        // Clear previous listeners
        damageButton.onClick.RemoveAllListeners();
        healthButton.onClick.RemoveAllListeners();
        speedButton.onClick.RemoveAllListeners();
        attackSpeedButton.onClick.RemoveAllListeners();

        // Add new listeners
        damageButton.onClick.AddListener(() => ApplyUpgrade("damage"));
        healthButton.onClick.AddListener(() => ApplyUpgrade("health"));
        speedButton.onClick.AddListener(() => ApplyUpgrade("speed"));
        attackSpeedButton.onClick.AddListener(() => ApplyUpgrade("attackSpeed"));
    }

    public void ApplyUpgrade(string type)
    {
        if (player == null)
        {
            Debug.LogError("Cannot apply upgrade because Player reference is missing!");
            return;
        }

        switch (type)
        {
            case "damage":
                player.damage += 5;
                break;
            case "health":
                player.maxHealth += 20;
                player.curHealth = player.maxHealth;
                break;
            case "speed":
                player.moveSpeed += 1f;
                break;
            case "attackSpeed":
                player.attackCooldown *= 0.9f; // Reduce cooldown by 10%
                if (player.attackCooldown < 0.3f)
                    player.attackCooldown = 0.3f; // Minimum cooldown limit
                break;
        }

        // Hide the panel and resume time
        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        Time.timeScale = 1f;
    }
}
