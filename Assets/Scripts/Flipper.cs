/* using UnityEngine;
using UnityEngine.InputSystem;

public class Flipper : MonoBehaviour
{

    [SerializeField] float hitStrength = 80000f;
    [SerializeField] float dampening = 250f;
    [SerializeField] HingeJoint hingeJointLeft;
    [SerializeField] HingeJoint hingeJointRight;
    private JointSpring jointSpringReleased = new();
    private JointSpring jointSpringPressed = new();
    private bool leftFlipperPressed, rightFlipperPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jointSpringReleased.spring = hitStrength;
        jointSpringReleased.damper = dampening;
        jointSpringReleased.targetPosition = 0f;

        jointSpringPressed.spring = hitStrength;
        jointSpringPressed.damper = dampening;
        jointSpringPressed.targetPosition = 60f;

        hingeJointLeft.useSpring = true;
        hingeJointRight.useSpring = true;
    }

    private void OnLeftFlipper(InputValue value)
    {
        leftFlipperPressed = value.isPressed;
    }

    private void OnRightFlipper(InputValue value)
    {
        rightFlipperPressed = value.isPressed;
    }


    // Update is called once per frame
    void Update()
    {
        hingeJointLeft.spring = leftFlipperPressed ? jointSpringPressed : jointSpringReleased;
        hingeJointRight.spring = rightFlipperPressed ? jointSpringPressed : jointSpringReleased;
    }
}
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flipper : MonoBehaviour
{
    [SerializeField] AudioSource soundFlipper;
    [SerializeField] float hitStrength = 80000f;
    [SerializeField] float dampening = 250f;
    [SerializeField] HingeJoint hingeJointLeft;
    [SerializeField] HingeJoint hingeJointRight;
    private JointSpring jointSpringReleased = new();
    private JointSpring jointSpringPressed = new();
    private bool leftFlipperPressed, rightFlipperPressed;

    // Start is called before the first frame update
    void Start()
    {
        jointSpringPressed.spring = jointSpringReleased.spring = hitStrength;
        jointSpringPressed.damper = jointSpringReleased.damper = dampening;
        jointSpringPressed.targetPosition = hingeJointLeft.limits.max;
        jointSpringReleased.targetPosition = hingeJointLeft.limits.min;
    }

    private void OnLeftFlipper(InputValue value)
    {
        soundFlipper.Play();
        leftFlipperPressed = value.isPressed;
    }
    private void OnRightFlipper(InputValue value)
    {
        soundFlipper.Play();
        rightFlipperPressed = value.isPressed;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftFlipperPressed)
        {
            hingeJointLeft.spring = jointSpringPressed;
        }
        else
        {
            hingeJointLeft.spring = jointSpringReleased;
        }
        if (rightFlipperPressed)
        {
            hingeJointRight.spring = jointSpringPressed;
        }
        else
        {
            hingeJointRight.spring = jointSpringReleased;
        }
    }
}