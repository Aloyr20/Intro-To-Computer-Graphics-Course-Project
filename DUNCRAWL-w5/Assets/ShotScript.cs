using Unity.VisualScripting;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    ParticleSystem particles;
    public GameObject DropEffect;

    public bool AoE;
    public float AoEradius;

    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (AoE)
        {
            Collider[] hit = Physics.OverlapSphere(transform.position, AoEradius, LayerMask.GetMask("Enemy"));
            foreach (Collider col in hit)
            {

                if (col.gameObject.layer == 9 && col.gameObject.GetComponent<EnemyHP>() != null)
                {
                    col.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
                }
                
                //change to variable damage
            }
        }



        if (particles != null)
        {
            particles.Stop();
        }

        GameObject blast = Instantiate(DropEffect, transform.position, Quaternion.identity);
        Destroy(blast, 0.5f);

        GetComponent<Light>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 1.5f);
    }
}
