using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    [Header("Damage")]
    public int damage = 10;

    [Header("AoE Settings")]
    public bool AoE = false;
    public float AoERadius = 3f;

    [Header("Spell Effect Receiver")]
    public SpellEffectReceiver.SpellType spellType;

    [Header("Effects")]
    public GameObject dropEffect;
    private ParticleSystem particles;

    private Rigidbody rb;
    private bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particles = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        {
            hasHit = true;
        }

        SpellEffectReceiver target = collision.collider.GetComponent<SpellEffectReceiver>();
        if (target != null)
        {
            target.ApplyEffect(spellType);
        }
        if (AoE)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, AoERadius, LayerMask.GetMask("Enemy"));

            foreach (Collider col in hits)
            {
                EnemyHP hp = col.GetComponent<EnemyHP>();
                if (hp != null)
                {
                    hp.TakeDamage(damage);
                }
            }
        }
        else
        {
            EnemyHP hp = collision.collider.GetComponent<EnemyHP>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
        }

        if (particles != null)
        {
            particles.Stop();
        }

        if (dropEffect != null)
        {
            GameObject impact = Instantiate(dropEffect, transform.position, Quaternion.identity);
            Destroy(impact, 0.75f);
        }

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Collider col2 = GetComponent<Collider>();

        if (col2 != null)
        {
            col2.enabled = false;
        }

        Light l = GetComponent<Light>();
        if (l != null)
        { 
            l.enabled = false;
        }

        Destroy(gameObject, 1.5f);
    }
}
