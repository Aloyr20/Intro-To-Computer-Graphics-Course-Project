using UnityEngine;
using UnityEngine.UI;

public class DummyEnemy : MonoBehaviour
{
    public Slider healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

    void HealthBarFill()
    {
        healthBar.value += 20;
    }

    public void TakeDamage()
    {
        healthBar.value -= 20;
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        Invoke("HealthBarFill", 0.5f);
    }


}
