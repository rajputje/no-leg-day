using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class NewFistMovements : MonoBehaviour
{
    private FistController joystickAssigned;
    public FistController JoystickAssigned 
    { 
        get { return joystickAssigned; }
        set
        {
            if (joystickAssigned != null)
            {
                joystickAssigned.OnJoystickDown -= JoystickAssigned_OnJoystickDown;
                joystickAssigned.OnJoystickUp -= JoystickAssigned_OnJoystickUp;
            }
            joystickAssigned = value;
            joystickAssigned.OnJoystickDown += JoystickAssigned_OnJoystickDown;
            joystickAssigned.OnJoystickUp += JoystickAssigned_OnJoystickUp;
            JoystickAssigned.ChangeColor(JoystickAssigned.DefaultColour);
        }
    } //joystick assigned to this fist
    
    [SerializeField] private Transform shoulderTransform = null;    //position where forces to torso will be applied
    [SerializeField] private float armLength = 0.75f;
    
    private FixedJoint2D fixedJoint2D;

    private LayerMask grabbableLayer;

    private Rigidbody2D grabbedRb;

    private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D torsoRb = null;             //to apply push/pull forces to torso
    [SerializeField] private Rigidbody2D upperHandRb = null;
    [SerializeField] private Rigidbody2D lowerHandRb = null;
    
    [SerializeField] private float fistForce = 150f;
    [SerializeField] private float pullForce = 700f;
    [SerializeField] private float pushForce = 450f;
    [SerializeField] private float carryForce = 200f;
    [SerializeField] private float minPushForce = 250f;

    [Tooltip("Acceptable angular difference between fist and object when grabbing")]
    [SerializeField] private float grabDeltaDirection = 60f;

    private bool joystickPressed;
    private bool IsColliding => collisionCount != 0;
    private bool IsHandStretching => Vector2.SqrMagnitude(rb.position - torsoRb.position) > 3;

    private bool IsGrabbing => fixedJoint2D;
    private bool isCarrying = false;

    [SerializeField] private PhysicsMaterial2D relaxed_mat = null;   //when the hand is not pressing
    [SerializeField] private PhysicsMaterial2D pressed_mat = null;   //when the hand is pressing (more friction)

    private int collisionCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        grabbableLayer = LayerMask.GetMask("Grabbable");
        rb.sharedMaterial = relaxed_mat;
    }

    private void OnEnable()
    {
        if (JoystickAssigned == null)
        {
            Debug.LogWarning("No joystick assigned to fist.");
        }
        else
        {
            JoystickAssigned.OnJoystickDown += JoystickAssigned_OnJoystickDown;
            JoystickAssigned.OnJoystickUp += JoystickAssigned_OnJoystickUp;
        }
    }

    private void OnDisable()
    {
        if (JoystickAssigned == null)
        {
            Debug.LogWarning("No joystick found.");
        }
        else
        {
            JoystickAssigned.OnJoystickDown -= JoystickAssigned_OnJoystickDown;
            JoystickAssigned.OnJoystickUp -= JoystickAssigned_OnJoystickUp;
        }
    }

    public void Disable()
    {
        Release();
        enabled = false;
    }

    private void JoystickAssigned_OnJoystickUp()
    {
        joystickPressed = false;
        Release();

        if (rb != null)
            rb.sharedMaterial = relaxed_mat;
    }

    private void JoystickAssigned_OnJoystickDown()
    {
        joystickPressed = true;

        if (rb != null)
            rb.sharedMaterial = pressed_mat;
    }

    private void FixedUpdate()
    {
        if (joystickPressed)
        { 
            Vector2 direction = JoystickAssigned.Direction;
            upperHandRb.velocity = new Vector2(upperHandRb.velocity.x * 0.8f, upperHandRb.velocity.y);  //adding drag on x-axis
            lowerHandRb.velocity = new Vector2(lowerHandRb.velocity.x * 0.8f, lowerHandRb.velocity.y);  //drag on y-axis causes issues when falling
            if (isCarrying)
                rb.AddForce(direction * carryForce);
            else
            {
                rb.AddForce(direction * fistForce);
            }
            if (IsColliding || (IsGrabbing && !isCarrying))
            {
                AddForceToTorso(direction);
            }
            if (fixedJoint2D != null && IsHandStretching)
            {
                Release();
            }
        }
    }

    private void AddForceToTorso(Vector2 joystickDirection)
    {
        bool isPulling = Vector2.Dot(rb.position - upperHandRb.position, joystickDirection) < 0;
        float distanceSqr = Vector2.SqrMagnitude(rb.position - (Vector2)shoulderTransform.position);
        float force;
        if (isPulling)
        {
            force = pullForce;
        }
        else if (distanceSqr < armLength * armLength)
        {
            force = pushForce;
        }
        else
        {
            force = minPushForce;
        }
        torsoRb.AddForceAtPosition(force * -joystickDirection, shoulderTransform.position);
    }

    private bool IsGrabbingPossible(int objectLayer, Vector2 touchPosition)
    {
        return fixedJoint2D == null && 1 << objectLayer == grabbableLayer && joystickPressed &&
            AIBehaviour.IsAimingAt(transform.position, JoystickAssigned.Direction, touchPosition, grabDeltaDirection);
    }

    private void Grab(Rigidbody2D objectRb, Vector2 contactPosition)
    {
        fixedJoint2D = gameObject.AddComponent<FixedJoint2D>();
        fixedJoint2D.autoConfigureConnectedAnchor = true;
        fixedJoint2D.enableCollision = true;
        fixedJoint2D.connectedBody = objectRb;
        fixedJoint2D.anchor = contactPosition - (Vector2)transform.position;
        
        grabbedRb = objectRb;
        if (grabbedRb)
        {
            Carrieable c = objectRb.gameObject.GetComponent<Carrieable>();
            if (c)
            {
                isCarrying = true;
                c.Carry(joystickAssigned, torsoRb, shoulderTransform, minPushForce);
            }
        }
        JoystickAssigned.ChangeColor(JoystickAssigned.GrabbedColour);
    }

    private void Release()
    {
        if (fixedJoint2D != null)
            fixedJoint2D.breakForce = 0;
        if (isCarrying)
        {
            isCarrying = false;
            grabbedRb.gameObject.GetComponent<Carrieable>().Uncarry();
        }
        grabbedRb = null;
        JoystickAssigned.ChangeColor(JoystickAssigned.DefaultColour);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.sqrMagnitude > 5f)
        {
            AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.SmallImpact);
        }
        collisionCount++;
        if(IsGrabbingPossible(collision.gameObject.layer, collision.GetContact(0).point))
        {
            Grab(collision.rigidbody, collision.GetContact(0).point);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;
    }
}