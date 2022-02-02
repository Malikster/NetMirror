using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring settings: ")]
    /* [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position; */
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings (jointSpring);
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate movement velocity as 3d vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as 3d vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3 (0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as 3d vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3 (_xRot, 0f, 0f) * lookSensitivity;

        //Apply rotation
        motor.RotateCamera(_cameraRotation);

        //Calculate Thruster Force as a 3d Vector
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else 
        {
            SetJointSettings(jointSpring);
        }

        //Apply the thruster force
        motor.ApplyThruster(_thrusterForce);

        
    }

    private void SetJointSettings (float _jointSpring)
        {
            joint.yDrive = new JointDrive
            { 
                //mode = jointMode, 
                positionSpring = _jointSpring,
                maximumForce = jointMaxForce
            };
        }
}
