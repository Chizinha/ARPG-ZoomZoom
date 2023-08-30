using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private float projDuration;
    private Vector3 projDirection;
    private float projSpeed;
    private float projTimer;
    //private Rigidbody Rigidbody;

    public delegate void OnDisableCallback(Projectile Instance);
    public OnDisableCallback Disable;

    private void Awake()
    {
        //Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position += projSpeed * Time.deltaTime * projDirection;

        if (Time.time > projTimer + projDuration)
        {
            Disable?.Invoke(this);
        }
    }

    public void Setup(Vector3 projDirection, float projSpeed, float projDuration)
    {
        //Rigidbody.AddForce(projDirection * projSpeed, ForceMode.VelocityChange);
        
        this.projDirection = projDirection;
        this.projSpeed = projSpeed;
        this.projDuration = projDuration;

        projTimer = Time.time;
        //Destroy(gameObject, 3f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Projectile>() && !other.GetComponent<CharacterController>())
        {
            //Destroy(gameObject);
            Disable?.Invoke(this);
        }
    }
}
