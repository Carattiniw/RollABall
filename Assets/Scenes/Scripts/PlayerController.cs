using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public float speed;
    //public Text countText;
    //public Text winText;
    public TextMeshProUGUI winCustomText;
    public TextMeshProUGUI countCustomText;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI playerLivesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public AudioSource pickupSound;
    public float outOfBounds;
    public static float timer;
    //public static bool timeStarted = false;
    private Rigidbody rb;
    private int count;
    private int score;
    private int lives;
    private int yellowPickUp;
    private bool gameOver;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lives = 3;
        yellowPickUp = 0;
        gameOver = false;
        pickupSound = GetComponent<AudioSource> ();
        SetCountText();
        //winText.text = "";
        winCustomText.text = "";
        gameOverText.text = "";
        timerText.text = "";
        

        /*
        if (timeStarted == true)
        {
            //timer += Time.deltaTime;
            timer = Time.time;
        }
        */
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce (movement * speed);

        if (transform.position.y < outOfBounds)
        {
            transform.position = new Vector3(54.0f, 0.5f, 0.0f);
            lives = lives - 1;
            SetCountText();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        restartGame();
        OnGUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 1;
            yellowPickUp = yellowPickUp + 1;
            
            SetCountText();
        }

        else if (other.gameObject.CompareTag("Enemy Pick Up"))
        {
            GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score - 1;
            lives = lives - 1;
            
            if (score <= 1)
            {
                score = 0;
            }

            SetCountText();
        }
    }

    void OnGUI()
    {
        if(gameOver)
        {
            return;
        }

        //float t = Time.time - timer;
        float t = Time.timeSinceLevelLoad - timer;
        //int minutes = Mathf.FloorToInt(timer / 60F);
        //int seconds = Mathf.FloorToInt(timer - minutes * 60);
        int minutes = Mathf.FloorToInt(t / 60F);
        int seconds = Mathf.FloorToInt(t - minutes * 60);
        //int milli = Mathf.FloorToInt(t * 1000);
        //milli = milli % 1000;
        //milli = milli.ToString("f2");
        //string niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milli);
        string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        //GUI.Label(new Rect(10,10,250,100), niceTime);
        //timerText.text = "Timer: " + niceTime.ToString();
        timerText.text = "Timer: " + niceTime;
    }

    void SetCountText()
    {
        //countText.text = "Count: " + count.ToString();
        countCustomText.text = "Count: " + count.ToString();
        playerScoreText.text = "Player Score: " + score.ToString();
        playerLivesText.text = "Lives: " + lives.ToString();

        if (yellowPickUp == 12)
        {
            //this will move the player to a new playfield ONLY after collecting 12 yellow pickups
            transform.position = new Vector3(54.0f, transform.position.y, 0.0f);
        }

        if (yellowPickUp >= 25)
        {
            //winText.text = "You Win!";
            winCustomText.SetText("You Win!");
            gameOver = true;
        }

        if (lives == 0)
        {
            gameObject.SetActive(false);
            winCustomText.SetText("Game Over!");
            gameOver = true;
            //gameOverText.SetText("Game will restart in five seconds.");
            //gameOverText.SetText("Press escape key to exit.");
            //StartCoroutine(restartGame());
        }
    }

    void restartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        /*
        if (gameOver == true)
        {
            //gameOverText.SetText("Press R to Restart");
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(0);
        }
        */
    }
}