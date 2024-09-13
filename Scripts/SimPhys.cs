using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SimPhys : MonoBehaviour
{
    private Scene _testScene;
    private PhysicsScene _lineCalc;

    [SerializeField]
    private LineRenderer _lineRend;
    [SerializeField]
    private int _maxLineIterations;
    // Start is called before the first frame update
    void Start()
    {
        CreatePhysScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePhysScene()
    {
        _testScene = SceneManager.CreateScene("Trajectory Simulator", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _lineCalc = _testScene.GetPhysicsScene();
    }

    public void LineCalculation(Projectile Prefab, Vector3 POS, Vector3 Velocity)
    {
        var SimLine = Instantiate(Prefab, POS, Quaternion.identity);
        SimLine.GetComponent<Renderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(SimLine.gameObject, _testScene);
        SimLine.Launch(Velocity);

        _lineRend.positionCount = _maxLineIterations;

        for (int i = 0; i < _maxLineIterations; i++)
        {
            _lineCalc.Simulate(Time.fixedDeltaTime * 4);
            _lineRend.SetPosition(i, SimLine.transform.position);
        }
        Destroy(SimLine);
    }

}
