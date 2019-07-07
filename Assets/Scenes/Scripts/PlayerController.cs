using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    //public Text countText;
    //public Text winText;
    public TextMeshProUGUI winCustomText;
    public TextMeshProUGUI countCustomText;
    public TextMeshProUGUI playerScoreText;
    private Rigidbody rb;
    private int count;
    private int score;
    private int yellowPickUp;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        yellowPickUp = 0;
        SetCountText();
        //winText.text = "";
        winCustomText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Application.LoadLevel(0);
            SceneManager.LoadScene(0);
        }
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 1;
            yellowPickUp = yellowPickUp + 1;
            SetCountText();
        }

        else if (other.gameObject.CompareTag("Enemy Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score - 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        //countText.text = "Count: " + count.ToString();
        countCustomText.text = "Count: " + count.ToString();
        playerScoreText.text = "Player Score: " + score.ToString();

        if (yellowPickUp == 12)
        {
            //this will move the player to a new playfield ONLY after collecting 12 yellow pickups
            transform.position = new Vector3(54.0f, transform.position.y, 0.0f);
        }

        if (yellowPickUp >= 25)
        {
            //winText.text = "You Win!";
            winCustomText.SetText("You Win!");
        }
    }
}