using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image pulseBar;
    public Image gravityBar;
    public Image phaseBar;

    public PlayerController player;

    private void Update()
    {
        if (player == null) return;

        pulseBar.fillAmount = GetRatio(player.pulseCd, player.pulseCooldown);
        pulseBar.color = pulseBar.fillAmount >= 1 ? Color.white : Color.cyan;
        gravityBar.fillAmount = GetRatio(player.gravityCd, player.gravityCooldown);
        phaseBar.fillAmount = GetRatio(player.phaseCd, player.phaseCooldown);
    }

    private float GetRatio(float current, float max)
    {
        if (max <= 0) return 1f;
        return 1f - (current / max);
    }
}