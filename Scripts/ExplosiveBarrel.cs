using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float Radius = 10;
    public float Power = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        Vector3 ExplosionPosition = transform.position;
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Collider[] RadiusCollider = Physics.OverlapSphere(ExplosionPosition, Radius);
            foreach (Collider Hit in RadiusCollider)
            {
                Rigidbody RigidB = Hit.GetComponent<Rigidbody>();
                if (RigidB != null)
                {
                    RigidB.AddExplosionForce(Power, ExplosionPosition, Radius, 3.0f, ForceMode.Impulse);
                }

                if(Hit.CompareTag("Barrel"))
                {
                    var barrel = Hit.GetComponent<RegularBarrel>();
                    barrel.DestroyBarrel();
                }
            }
            Destroy(this.gameObject);
        }
    }
}