using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float Radius = 10;
    public float Power = 10;
    [SerializeField]
    private GameObject _explosion;
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
  
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        Vector3 ExplosionPosition = transform.position;
        GameObject explosion = Instantiate(_explosion);
        explosion.transform.parent = explosion.transform;
        explosion.transform.localPosition = ExplosionPosition;
        yield return new WaitForSeconds(.5f);
        Collider[] RadiusCollider = Physics.OverlapSphere(ExplosionPosition, Radius);
        foreach (Collider Hit in RadiusCollider)
        {
            Rigidbody RigidB = Hit.GetComponent<Rigidbody>();
            if (RigidB != null)
            {
                RigidB.AddExplosionForce(Power, ExplosionPosition, Radius, 3.0f, ForceMode.Impulse);
            }

            if (Hit.CompareTag("Barrel"))
            {
                var barrel = Hit.GetComponent<RegularBarrel>();
                barrel.DestroyBarrel();
            }
        }
        Destroy(this.gameObject);
    }
}