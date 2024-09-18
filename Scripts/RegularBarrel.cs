using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBarrel : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Hit");
            StartCoroutine(Despawn());
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
