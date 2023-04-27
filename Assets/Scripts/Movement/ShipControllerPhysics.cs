using UnityEngine;

public class ShipControllerPhysics : MonoBehaviour
{
    [Header("CONST FIELDS")]
    [SerializeField] private const float FORCE_MULTIPLIER = 100f; // related with mass of gameobject
    [SerializeField] private const float THROTTLE_INCREASE_RATE = 100f;
    [SerializeField] private const float THROTTLE_REDUCE_RATE = 65f;

    [Header("RIGIDBODY SETTINGS")]
    [SerializeField] private float maxAngularVelocity; // rigidbody's angularVelocity field => default is 7
    [SerializeField] private float defaultDrag;

    [Header("SHIP COMPONENTS")]
    // CAREFUL USING THIS ARRAY MAKE SURE THAT ARRAY INDEXES ARE IN THE RIGHT ORDER !
    [SerializeField] private Transform[] subThrusters; // for this spaceship we have 2 subThrusters placed at one front and one back
    [SerializeField] private Transform mainThruster;
    private new Rigidbody rigidbody;

    [Header("CONTROL COEFFICIENTS")]
    [SerializeField] private float horizontalThrust;
    [SerializeField] private float verticalThrust;
    [SerializeField] private float verticalSubThrust;

    [SerializeField] private float torqueMouseSensitivity;
    [SerializeField] private float yawSpeed;

    [Header("SPEED ATTRIBUTES")]
    [SerializeField] private float cruiseSpeed;
    [SerializeField] private float throttleMouseWheelUpdateRatio;
    [SerializeField][Range(0f, 100f)] private float throttle;

    [Header("AFTERBURNER SETTINGS")]
    [SerializeField] private float horizontalAfterburnerMultiplier;
    [SerializeField] private float verticalAfterburnerMultiplier;

    private float defaultHorizontalThrust;
    private float defaultVerticalThrust;

    private bool isHorizontalAfterburnerActive = false;
    private bool isVerticalAfterburnerActive = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidbody.maxAngularVelocity = maxAngularVelocity;
        defaultDrag = rigidbody.drag;

        defaultHorizontalThrust = horizontalThrust;
        defaultVerticalThrust = verticalThrust;
    }

    private void FixedUpdate()
    {
        GetMouseInput(); // MOUSE CONTROL
        GetYawInputs(); // LEFT RIGHT CONTROLS
        GetThrusterInputs(); // THRUSHTER INPUTS
    }

    private void Update()
    {
        cruiseSpeed = rigidbody.velocity.magnitude;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ResetDrag();
        }
        GetThrottleInputs(KeyCode.W, KeyCode.S, KeyCode.F); // FORWARD FORCE
        GetMouseWheelInput();
        GetAfterburnerInputs();
    }


    public void ResetDrag()
    {
        rigidbody.drag = 0.0f;
        throttle = 0.0f; // when drag is zero we disable the engine so that it will not get infinite force 
    }

    public void GetMouseInput()
    {
        float roll = Input.GetAxis("Mouse X"); // rotate right - left
        float pitch = Input.GetAxis("Mouse Y"); // rotate up - down
        rigidbody.AddRelativeTorque(Vector3.back * torqueMouseSensitivity * roll * FORCE_MULTIPLIER);
        rigidbody.AddRelativeTorque(Vector3.left * torqueMouseSensitivity * pitch * FORCE_MULTIPLIER);
    }

    public void GetYawInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddRelativeTorque(Vector3.down * yawSpeed * FORCE_MULTIPLIER); // left
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddRelativeTorque(Vector3.up * yawSpeed * FORCE_MULTIPLIER); // right
        }
    }

    public void GetThrottleInputs(KeyCode goToMaximum, KeyCode goToMinimum, KeyCode jumpToZero)
    {
        if (Input.GetKey(goToMaximum))
        {
            if (rigidbody.drag == 0.0f)
            {
                rigidbody.drag = defaultDrag;
            }
            throttle = Mathf.MoveTowards(throttle, 100.0f, Time.deltaTime * THROTTLE_INCREASE_RATE);
        }

        else if (Input.GetKey(goToMinimum))
        {
            throttle = Mathf.MoveTowards(throttle, 0.0f, Time.deltaTime * THROTTLE_REDUCE_RATE);
        }

        if (Input.GetKeyDown(jumpToZero))
        {
            throttle = 0.0f;
        }
    }

    public void GetMouseWheelInput()
    {
        if (rigidbody.drag > 0.0f)
        {
            throttle += Input.GetAxis("Mouse ScrollWheel") * throttleMouseWheelUpdateRatio * 10f; // will be incremented by 1 when Input called
            throttle = Mathf.Clamp(throttle, 0.0f, 100.0f);
        }
    }

    private void GetAfterburnerInputs()
    {
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool isSpacePressed = Input.GetKey(KeyCode.Space);


        if (isShiftPressed && isSpacePressed && throttle <= 0f)
        {
            ActivateVerticalAfterburner();
        }
        else if (isShiftPressed || (isShiftPressed && isSpacePressed))
        {
            ActivateHorizontalAfterburner();
        }
        else
        {
            DeactivateHorizontalAfterburner();
            DeactivateVerticalAfterburner();
        }
    }


    private void ActivateHorizontalAfterburner()
    {
        if (!isHorizontalAfterburnerActive)
        {
            horizontalThrust *= horizontalAfterburnerMultiplier;
            isHorizontalAfterburnerActive = true;
            // Visual Effect Management...
        }
    }

    private void DeactivateHorizontalAfterburner()
    {
        if (isHorizontalAfterburnerActive)
        {
            horizontalThrust = defaultHorizontalThrust;
            isHorizontalAfterburnerActive = false;
            // Visual Effect Management...
        }
    }

    private void ActivateVerticalAfterburner()
    {
        if (!isVerticalAfterburnerActive)
        {
            verticalThrust *= verticalAfterburnerMultiplier;
            isVerticalAfterburnerActive = true;
            // Visual Effect Management...
        }
    }

    private void DeactivateVerticalAfterburner()
    {
        if (isVerticalAfterburnerActive)
        {
            verticalThrust = defaultVerticalThrust;
            isVerticalAfterburnerActive = false;
            // Visual Effect Management...
        }
    }

    public void GetThrusterInputs()
    {
        if (rigidbody.drag > 0.0f) // WHEN DRAG IS ZERO DEACTIVATE THIS FORCES
        {
            // HORIZONTAL
            if (throttle > 0.0f)
            {
                rigidbody.AddRelativeForce(Vector3.forward * horizontalThrust * (throttle / 100.0f) * FORCE_MULTIPLIER);
            }

            // VERTICAL
            if (Input.GetKey(KeyCode.Space)) // MAIN THRUSTER
            {
                rigidbody.AddRelativeForce(Vector3.up * verticalThrust * FORCE_MULTIPLIER);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                rigidbody.AddRelativeForce(Vector3.down * verticalThrust / 2.0f * FORCE_MULTIPLIER);
            }
        }

        if (Input.GetKey(KeyCode.E)) // FRONT THRUSTER
        {
            rigidbody.AddForceAtPosition(transform.up * verticalSubThrust * FORCE_MULTIPLIER, subThrusters[0].position);
        }

        if (Input.GetKey(KeyCode.Q)) // BACK THRUSTER
        {
            rigidbody.AddForceAtPosition(transform.up * verticalSubThrust * FORCE_MULTIPLIER, subThrusters[1].position);
        }
    }
}
