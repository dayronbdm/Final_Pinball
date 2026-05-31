using UnityEngine;

public class Triple : MonoBehaviour
{
    [SerializeField] Transform spawnPosition1, spawnPosition2;
    [SerializeField] AudioSource soundThree;

    private void OnCollisionEnter(Collision collision)
    {
        soundThree.Play();
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length< 3)
        {
            Instantiate(Game.Instance.PfBall, spawnPosition1.position, Quaternion.identity);
            Instantiate(Game.Instance.PfBall, spawnPosition2.position, Quaternion.identity);
        }
        collision.collider.GetComponent<Rigidbody>().AddExplosionForce(4000f, transform.position, 4);
    }
}
