using UnityEngine;
using UnityEngine.UI;

public class SpellUIController : MonoBehaviour
{
    public Image spellIcon;

    public Sprite iceSprite;
    public Sprite fireSprite;
    public Sprite poisonSprite;

    public void ShowSpell(string spellName)
    {
        switch (spellName)
        {
            case "Ice": spellIcon.sprite = iceSprite; break;
            case "Fire": spellIcon.sprite = fireSprite; break;
            case "Poison": spellIcon.sprite = poisonSprite; break;
        }
    }
}
