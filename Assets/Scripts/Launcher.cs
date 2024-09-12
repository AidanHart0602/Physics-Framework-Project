using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private SimPhys _lineSimulator;
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private float _launchPower;
    private bool _cooldown = true;
    [SerializeField]
    private float _cooldownTime = 3.0f;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        _lineSimulator.LineCalculation(_projectile, transform.position, transform.forward * _launchPower);
    }

    // Update is called once per frame
    void Update()
    {
        if(_cooldown != false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var Spawned = Instantiate(_projectile, transform.position, transform.rotation);
                Spawned.transform.parent = Spawned.transform.root;
                Spawned.Launch(transform.forward * _launchPower);
                StartCoroutine(Cooldown());

            }
        }    
    }

    IEnumerator Cooldown()
    {
        _cooldown = false;
        yield return new WaitForSeconds(_cooldownTime);
        _cooldown = true;
    }
}
