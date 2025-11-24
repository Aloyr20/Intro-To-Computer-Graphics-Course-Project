using UnityEngine;
using System.Collections;

public class SpellEffectReceiver : MonoBehaviour
{
    public enum SpellType { None, Ice, Fire, Poison }
    private SpellType currentEffect = SpellType.None;

    private float originalSpeed;
    public float enemySpeed = 5f;
    private Coroutine activeEffect;

    void Start()
    {
        originalSpeed = enemySpeed;
    }

    public void ApplyEffect(SpellType newEffect)
    {
        if (currentEffect == newEffect)
        {
            return;
        }
        if (currentEffect != SpellType.None)
        {
            return;
        }

        if (activeEffect != null)
        {
            StopCoroutine(activeEffect);
        }

        activeEffect = StartCoroutine(HandleEffect(newEffect));
    }

    IEnumerator HandleEffect(SpellType effect)
    {
        currentEffect = effect;

        switch (effect)
        {
            case SpellType.Ice:

                enemySpeed *= 0.5f;
                yield return new WaitForSeconds(5f);
                enemySpeed = originalSpeed;
                DealDamage(50);
                break;

            case SpellType.Fire:

                for (int i = 0; i < 3; i++)
                {
                    DealDamage(20);
                    yield return new WaitForSeconds(2f);
                }
                break;

            case SpellType.Poison:

                for (int i = 0; i < 15; i++)
                {
                    DealDamage(5);
                    yield return new WaitForSeconds(1f);
                }
                break;
        }

        currentEffect = SpellType.None;
    }

    public void DealDamage(float dmg)
    {
        Debug.Log($"{gameObject.name} took {dmg} damage");
    }
}
