using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] AudioSource soundSpring;
    void OnShoot()
    {
        soundSpring.Play();
        Debug.Log("Spring: OnShoot called");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);
        Debug.LogFormat("Spring: Found {0} colliders in range", colliders.Length);
        foreach (Collider collider in colliders)
        {
            Ball ball = collider.GetComponent<Ball>();
            if (ball != null)
            {
                Debug.Log("Spring: Found ball, calling Shoot()");
                ball.Shoot();
            }
        }
    }
}