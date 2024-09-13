using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Launcher : MonoBehaviour
{
    private LauncherActionMap _input;
    [SerializeField]
    private SimPhys _lineSimulator;
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private float _launchPower;
    private bool _cooldown = true;
    [SerializeField]
    private float _cooldownTime = 3.0f;
    private float _currentRotation;
    [SerializeField]
    private int _aimLimit;
    [SerializeField]
    private int _speed = 3;
    // Start is called before the first frame update

    private void Start()
    {
        _input = new LauncherActionMap();
        _input.Controls.Enable();
        _input.Controls.Shoot.performed += Shoot_performed;
        _input.Controls.Aiming.performed += Aiming_performed;
    }



    private void Aiming_performed(InputAction.CallbackContext context)
    {
   
        var aim = _input.Controls.Aiming.ReadValue<float>(); 
        if(aim == -1 && _aimLimit != 2) 
        {
            _currentRotation = -3f;
            transform.Rotate(_currentRotation, 0, 0);
            _aimLimit++;
        }

        if (aim == 1 && _aimLimit != 0) 
        {
            _currentRotation = 3f;
            transform.Rotate(_currentRotation, 0, 0);
            _aimLimit--;
        }
    }

    private void Shoot_performed(InputAction.CallbackContext context)
    {
        if (_cooldown != false)
        {
            var Spawned = Instantiate(_projectile, transform.position, transform.rotation);
            Spawned.transform.parent = Spawned.transform.root;
            Spawned.Launch(transform.forward * _launchPower);
            StartCoroutine(Cooldown());
        }
    }


    void FixedUpdate()
    {
        _lineSimulator.LineCalculation(_projectile, transform.position, transform.forward * _launchPower);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        var Movement = _input.Controls.Movement.ReadValue<float>();
        if (Movement == 1)
        {
            transform.Translate(transform.right * _speed * -1 * Time.deltaTime);
        }

        else if (Movement == -1)
        {
            transform.Translate(transform.right * _speed * Time.deltaTime);
        }

        if (transform.position.x > 19f)
        {
            transform.position = new Vector3(19f, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -19f)
        {
            transform.position = new Vector3(-19f, transform.position.y, transform.position.z);
        }
    }
    IEnumerator Cooldown()
    {
        _cooldown = false;
        yield return new WaitForSeconds(_cooldownTime);
        _cooldown = true;
    }
}
