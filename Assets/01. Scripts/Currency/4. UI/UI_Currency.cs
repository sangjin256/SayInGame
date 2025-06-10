using UnityEngine;
using TMPro;
using Unity.FPS.Gameplay;
using Unity.FPS.Game;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHealthText;

    private void Start()
    {
        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    private void Update()
    {
        // Å×½ºÆ®
        if (Input.GetKeyDown(KeyCode.Alpha3)) BuyHealth();
    }

    private void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);

        GoldCountText.text = $"Gold: {gold.Value}";
        DiamondCountText.text = $"Diamond: {diamond.Value}";

        BuyHealthText.color = CurrencyManager.Instance.Get(ECurrencyType.Gold).Value >= 500 ? Color.green : Color.red;
    }

    public void BuyHealth()
    {
        if(CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 500))
        {
            var player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            Health PlayerHealth = player.GetComponent<Health>();
            PlayerHealth.Heal(100);
        }
    }
}
