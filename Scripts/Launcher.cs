using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Launcher : MonoBehaviour
{
    [SerializeField]
    private Transform _cannon;
    [SerializeField]
    private Transform _cannonBase;
    private LauncherActionMap _input;
    [SerializeField]
    private SimPhys _lineSimulator;
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private float _launchPower;
    private bool _cooldown = true;
    [SerializeField]
    public float CooldownTime = 3.0f;
    [SerializeField]
    private int _speed = 10;
    // Start is called before the first frame update

    private void Start()
    {
        _input = new LauncherActionMap();
        _input.Controls.Enable();
        _input.Controls.Shoot.performed += Shoot_performed;
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
        Rotation();
    }

    private void Rotation()
    {

        Vector3 RotRestrict = this.transform.rotation.eulerAngles;
        RotRestrict.z = Mathf.Clamp(RotRestrict.z, 0, 0);

        this.transform.rotation = Quaternion.Euler(RotRestrict);
        _cannon.transform.rotation = Quaternion.Euler(RotRestrict);
        _cannonBase.transform.rotation = Quaternion.Euler(RotRestrict);
        
        //Left and Right Aim Controls
        var Movement = _input.Controls.Movement.ReadValue<float>();
        if (Movement == 1)
        {
            _cannonBase.transform.Rotate(transform.up * -1 * _speed * Time.deltaTime);
            transform.Rotate(transform.up * _speed * -1 * Time.deltaTime);
        }

        else if (Movement == -1)
        { 
            _cannonBase.transform.Rotate(transform.up * _speed * Time.deltaTime);
            transform.Rotate(transform.up * _speed * Time.deltaTime);
        }

        //Up and Down aim controls
        var aim = _input.Controls.Aiming.ReadValue<float>();
        if (aim == 1)
        {
            _cannon.transform.Rotate(transform.right * _speed * Time.deltaTime);
            transform.Rotate(transform.right * _speed * Time.deltaTime);
        }

        if (aim == -1)
        {
            _cannon.transform.Rotate(transform.right * -1 * _speed * Time.deltaTime);
            transform.Rotate(transform.right * -1 * _speed * Time.deltaTime);
        }
    }
    IEnumerator Cooldown()
    {
        _cooldown = false;
        yield return new WaitForSeconds(CooldownTime);
        _cooldown = true;
    }
}
