using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // able to check radius
    // able to attack
    Collider[] _enemies;
    public float _radius = 5.0f;
    public LayerMask _enemyLayer;
    public float reach;
    private float damage = 0;

    public Transform PlayerTransform;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnemyDetermine();
        }

    }
    private void OnDrawGizmos()
    {
        // Draw the sphere.
        Gizmos.DrawWireSphere(transform.position + (PlayerTransform.forward * reach), _radius);


    }

    void EnemyDetermine()
    {
        _enemies = Physics.OverlapSphere(transform.position + (PlayerTransform.forward * reach), _radius, _enemyLayer);

        foreach (Collider enemy in _enemies)
        {
            if (enemy.name == "Dummy")
            {
                enemy.GetComponent<DummyEnemy>().TakeDamage();
                return;
            }
            damage = 20f;
            enemy.GetComponent<EnemyHP>().TakeDamage(damage);
        }
    }
}