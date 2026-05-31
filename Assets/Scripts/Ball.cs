/* using UnityEngine;

public class Ball : MonoBehaviour
{

    Vector2 previous_position;
    private new Rigidbody rigidbody;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        previous_position = new Vector2(transform.position.x, transform.position.y);
    }

    public void Shoot()
    {
        Debug.Log("Ball: Shoot called! Adding force.");
        rigidbody.AddForce(new Vector3(0, 130, 0), ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 speed = position - previous_position;
        Vector2 rotationAxis = Vector2.Perpendicular(speed);
        transform.Rotate( new Vector3(rotationAxis.x, rotationAxis.y, 0), -speed.magnitude * 40f, Space.World);

        previous_position = position;

        // Check if the ball has fallen out of bounds (below the table)
        if (transform.position.y < 30f)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            if (balls.Length <= 1)
            {
                Game.Instance.SpawnBall();
            }
            Destroy(gameObject);
        }
    }
} */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] AudioSource soundBallLost;
    Vector2 previousPosition;
    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        if (soundBallLost == null)
        {
            GameObject lostBallObject = GameObject.Find("lostball");
            if (lostBallObject != null)
            {
                soundBallLost = lostBallObject.GetComponent<AudioSource>();
            }
        }

        rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        rigidbody.AddForce(new Vector3(0, 130, 0), ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 speed = position - previousPosition;
        Vector2 rotationAxis = Vector2.Perpendicular(speed);
        transform.Rotate(new Vector3(rotationAxis.x, rotationAxis.y, 0), -speed.magnitude * 40f, Space.World);
        previousPosition = position;

        if (transform.position.y < 30)
        {
            if (soundBallLost != null)
            {
                soundBallLost.Play();
            }

            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            if (balls.Length <= 1) 
            { 
                Game.Instance.SpawnBall();
            }
            Destroy(gameObject);
        }
    }
}