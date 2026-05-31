using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance; 
    [SerializeField] GameObject pfBall;
    private Vector3 startPosition = new Vector3(5.9f, 41.5f, -59.1f);
    public GameObject PfBall { get => pfBall; set => pfBall = value; }

    [Header("Score Settings")]
    [SerializeField] private int targetScore = 300;
    private int score, currentScore;
    private bool gameWon = false;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI winScoreText;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;
    [SerializeField] GameObject pinballObject;

    [Header("Audio")]
    [SerializeField] AudioSource soundPoints;
    

    void Awake()
    {
        Instance = this;

        if (soundPoints == null)
        {
            soundPoints = GetComponent<AudioSource>();
        }

        if (textScore == null)
        {
            var go = GameObject.Find("CanvasGame/Panel/TextScore");
            if (go != null) textScore = go.GetComponent<TextMeshProUGUI>();
        }

        if (winPanel == null)
        {
            var canvas = GameObject.Find("CanvasGame");
            if (canvas != null)
            {
                var t = canvas.transform.Find("WinPanel");
                if (t != null) winPanel = t.gameObject;
            }
        }
    }

    private void Start() 
    {
        Time.timeScale = 1; 
        Physics.gravity = new Vector3(0, -50, 0);
        SpawnBall();
        UpdateScoreDisplay();
    }

    public void IncreaseScore(int amount)
    {
        if (gameWon) return;

        if (soundPoints != null)
        {
            soundPoints.Play();
        }
        score += amount;
        Debug.Log("Score increased by " + amount + ". Total: " + score + "/" + targetScore);

        if (score >= targetScore)
        {
            score = targetScore;
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("WIN CONDITION REACHED!");
        gameWon = true;
        
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("WinPanel reference is missing in Game script!");
        }

        // Destroy requested objects
        if (leftPanel != null) Destroy(leftPanel);
        if (rightPanel != null) Destroy(rightPanel);
        if (pinballObject != null) Destroy(pinballObject);

        // Also destroy any remaining balls
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            Destroy(ball);
        }

        currentScore = score;
        UpdateScoreDisplay();
        
        Time.timeScale = 0; 
    }

    private void UpdateScoreDisplay()
    {
        if (textScore != null)
        {
            textScore.text = currentScore + " / " + targetScore;
        }
        if (winScoreText != null)
        {
            winScoreText.text = "Score: " + currentScore + " / " + targetScore;
        }
    }

    void Update()
    {
        if (gameWon) return;

        if (currentScore < score)
        {
            currentScore += (int)(1000 * Time.deltaTime);
            if (currentScore > score)
            {
                currentScore = score;
            }
            UpdateScoreDisplay();
        }
        else
        {
            if (soundPoints != null && soundPoints.isPlaying && score == currentScore)
            {
                soundPoints.Stop();
            }
        }
    }

    public void SpawnBall()
    {
        Instantiate(pfBall, startPosition, Quaternion.identity);
    }
}
