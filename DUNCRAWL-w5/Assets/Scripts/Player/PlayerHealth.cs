using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public LevelClearController levelClearController;
    public Slider healthBar;

    void Start()
    {

    }

    void Update()
    {
        if (health <= 0)
        {
            levelClearController.Lose();
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        healthBar.value = Mathf.Clamp(health, 0, healthBar.maxValue);
    }
}
