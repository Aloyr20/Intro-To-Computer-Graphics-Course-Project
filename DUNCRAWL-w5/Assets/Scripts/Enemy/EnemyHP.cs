using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    [Header("Health")]
    public Slider healthBar;

    [Header("Status Effect Control")]
    public bool isPoisoned = false;
    public bool isBurning = false;
    public bool isSlowed = false;
    public LevelClearController levelClearController;

    public float originalSpeed;
    private EnemyAi ai;

    void Start()
    {
        ai = GetComponent<EnemyAi>();
        originalSpeed = ai.nav.speed;
    }

    public void TakeDamage(float damageamount)
    {
        healthBar.value -= damageamount;
        GetComponentInChildren<ParticleSystem>().Play();
        Alive();
    }

    public void ApplyPoison(float damagePerTick, float duration, float tickRate)
    {
        if (isPoisoned) return;
        StartCoroutine(PoisonRoutine(damagePerTick, duration, tickRate));
    }

    IEnumerator PoisonRoutine(float damagePerTick, float duration, float tickRate)
    {
        isPoisoned = true;

        float t = 0;
        while (t < duration)
        {
            TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickRate);
            t += tickRate;
        }

        isPoisoned = false;
    }

    public void ApplyBurn(float damagePerTick, float duration, float tickRate)
    {
        if (isBurning) return;
        StartCoroutine(BurnRoutine(damagePerTick, duration, tickRate));
    }

    IEnumerator BurnRoutine(float damagePerTick, float duration, float tickRate)
    {
        isBurning = true;

        float t = 0;
        while (t < duration)
        {
            TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickRate);
            t += tickRate;
        }

        isBurning = false;
    }

    public void ApplySlow(float slowPercent, float duration, float slowDamage)
    {
        if (isSlowed) return;

        StartCoroutine(SlowRoutine(slowPercent, duration, slowDamage));
    }

    IEnumerator SlowRoutine(float slowPercent, float duration, float slowDamage)
    {
        isSlowed = true;

        ai.nav.speed = originalSpeed * (1f - slowPercent);
        TakeDamage(slowDamage);

        yield return new WaitForSeconds(duration);

        ai.nav.speed = originalSpeed;
        isSlowed = false;
    }

    void Alive()
    {
        if (healthBar.value <= 0)
        {
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<EnemyAi>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Collider>().enabled = false;
            levelClearController.EnemyKilled();
            Destroy(gameObject, 5f);
        }
    }
}
