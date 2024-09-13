using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    public void Launch(Vector3 Velocity)
    {
        _rb.AddForce(Velocity, ForceMode.Impulse);
    }
}
