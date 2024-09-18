using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    private void Start()
    {
        StartCoroutine(Despawn());
    }
    public void Launch(Vector3 Velocity)
    {
        _rb.AddForce(Velocity, ForceMode.Impulse);
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
