using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{


    public static Game Instance; 
    [SerializeField] GameObject pfBall;
    private Vector3 startPosition = new Vector3(5.9f, 41.5f, -59.1f); // Aligned with circles and bumpers
    public GameObject PfBall { get => pfBall; set => pfBall = value; }
    private int score, currentScore;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] AudioSource soundPoints;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;

        if (soundPoints == null)
        {
            soundPoints = GetComponent<AudioSource>();
        }

        if (textScore == null)
        {
            textScore = FindAnyObjectByType<TextMeshProUGUI>();
        }
    }
    private void Start() 
    {
        Physics.gravity = new Vector3(0, -50, 0);
        SpawnBall();
        
    }

    public void IncreaseScore(int amount)
    {
        if (soundPoints != null)
        {
            soundPoints.Play();
        }
        score += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScore < score)
        {
            currentScore += (int)(1000 * Time.deltaTime);
            if (currentScore > score)
            {
                currentScore = score;
            }

            if (textScore != null)
            {
                textScore.text = currentScore.ToString("00000000");
            }
        }else
        {
            if (soundPoints != null)
            {
                soundPoints.Stop();
            }
        }
    }
    public void SpawnBall()
    {
       // Vector3(5.9000001,41.5029984,-59.368) 
       Instantiate(pfBall, startPosition, Quaternion.identity);
    }
}
