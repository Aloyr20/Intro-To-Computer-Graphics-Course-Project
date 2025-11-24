using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana = 100f;

    public float regenRate = 4f;

    void Update()
    {
        RegenerateMana();
    }

    void RegenerateMana()
    {
        currentMana += regenRate * Time.deltaTime;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
    }

    public bool TryUseMana(float amount)
    {
        if (currentMana < amount) return false;

        currentMana -= amount;
        return true;
    }

    public void AddMaxMana(float amount)
    {
        maxMana += amount;
        currentMana = maxMana;
    }
}
