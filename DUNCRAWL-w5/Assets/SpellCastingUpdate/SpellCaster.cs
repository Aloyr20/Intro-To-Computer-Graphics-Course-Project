using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [Header("References")]
    public Projectile projectileLauncher;
    public PlayerMana mana;
    public SpellUIController ui;

    [Header("Spell Prefabs")]
    public GameObject[] spellPrefabs;

    [Header("Mana Costs")]
    public int iceMana = 25;
    public int fireMana = 30;
    public int poisonMana = 45;

    [Header("Cooldowns")]
    public float iceCooldown = 5f;
    public float fireCooldown = 10f;
    public float poisonCooldown = 15f;

    private float lastIceTime;
    private float lastFireTime;
    private float lastPoisonTime;

    private float currentCooldownLeft = 0f;
    private bool cooldownReadyPrinted = false;

    public enum SpellType { None, Ice, Fire, Poison }
    public SpellType currentSpell = SpellType.None;

    void Update()
    {
        HandleSpellSwitch();
        HandleCooldownDisplay();
        HandleCasting();
    }

    void HandleSpellSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentSpell = SpellType.None;
            ui.ShowSpell("None");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSpell = SpellType.Ice;
            ui.ShowSpell("Ice");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSpell = SpellType.Fire;
            ui.ShowSpell("Fire");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSpell = SpellType.Poison;
            ui.ShowSpell("Poison");
        }
    }

    void HandleCooldownDisplay()
    {
        if (currentSpell == SpellType.None) return;

        float cooldown = 0f;
        float lastTime = 0f;

        switch (currentSpell)
        {
            case SpellType.Ice: cooldown = iceCooldown; lastTime = lastIceTime; break;
            case SpellType.Fire: cooldown = fireCooldown; lastTime = lastFireTime; break;
            case SpellType.Poison: cooldown = poisonCooldown; lastTime = lastPoisonTime; break;
        }

        currentCooldownLeft = cooldown - (Time.time - lastTime);

        if (currentCooldownLeft > 0f)
        {
            Debug.Log("Cooldown: " + currentCooldownLeft.ToString("F2"));
            cooldownReadyPrinted = false;
        }
        else
        {
            if (!cooldownReadyPrinted)
            {
                Debug.Log("Ready to use spell!");
                cooldownReadyPrinted = true;
            }
        }
    }

    void HandleCasting()
    {
        if (currentSpell == SpellType.None) return;
        if (!Input.GetMouseButton(1)) return;

        int spellIndex = (int)currentSpell - 1;
        GameObject prefabToFire = spellPrefabs[spellIndex];
        int manaCost = 0;
        float cooldown = 0f;
        float lastTime = 0f;

        switch (currentSpell)
        {
            case SpellType.Ice:
                manaCost = iceMana;
                cooldown = iceCooldown;
                lastTime = lastIceTime;
                break;
            case SpellType.Fire:
                manaCost = fireMana;
                cooldown = fireCooldown;
                lastTime = lastFireTime;
                break;
            case SpellType.Poison:
                manaCost = poisonMana;
                cooldown = poisonCooldown;
                lastTime = lastPoisonTime;
                break;
        }

        float timeSince = Time.time - lastTime;
        float cdLeft = cooldown - timeSince;

        if (cdLeft > 0f) return;

        if (!mana.TryUseMana(manaCost))
        {
            Debug.Log("Mana: " + mana.currentMana);
            return;
        }

        projectileLauncher.Fire(prefabToFire, projectileLauncher._speed);
        Debug.Log("Mana Used: " + manaCost);

        switch (currentSpell)
        {
            case SpellType.Ice: lastIceTime = Time.time; break;
            case SpellType.Fire: lastFireTime = Time.time; break;
            case SpellType.Poison: lastPoisonTime = Time.time; break;
        }
    }
}
