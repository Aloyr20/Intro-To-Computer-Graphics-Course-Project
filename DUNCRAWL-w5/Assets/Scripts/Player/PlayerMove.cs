using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    Vector3 Movement = Vector3.zero;

    CharacterController controller;

    public Transform GroundCheck;
    Collider[] Grounds;
    public LayerMask GroundLayer;
    float Gravity = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Dagger"), true);

    }

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        Grounds = Physics.OverlapSphere(GroundCheck.position, 0.5f, GroundLayer);

        if (Grounds.Length < 1)
        {
            Movement = Movement.normalized * MoveSpeed * Time.deltaTime * 0.5f;

            Movement = transform.right * Movement.x + transform.forward * Movement.y;

            Movement = Movement - new Vector3(0, Gravity * Time.deltaTime, 0);

            if (Gravity < 15)
            {
                Gravity += 0.25f;
            }

        }
        else
        {
            Movement = Movement.normalized * MoveSpeed * Time.deltaTime;

            Movement = transform.right * Movement.x + transform.forward * Movement.y;
            Gravity = 1;

        }
        controller.Move(Movement);


    }
    private void FixedUpdate()
    {

    }

    private void OnDrawGizmos()
    {
        // Draw ground check.
        Gizmos.DrawWireSphere(GroundCheck.position, 0.5f);


    }
}