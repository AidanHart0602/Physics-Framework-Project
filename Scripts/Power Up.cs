using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Launcher _launcher;
    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _launcher = FindObjectOfType<Launcher>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            _renderer.enabled = false;
            StartCoroutine(FireRate());
        }
    }

    private IEnumerator FireRate()
    {
        _launcher.CooldownTime = .5f;
        yield return new WaitForSeconds(10f);
        _launcher.CooldownTime = 3f;
        Destroy(this.gameObject);
    }
}
