using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rb;

    public bool isMoving = false;

    public AudioSource audioSource;
    public AudioClip endSound;
    public AudioClip hittedSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
     if (isMoving == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity = Vector2.up * speed;
                isMoving = true;
                audioSource.PlayOneShot(hittedSound);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rb.velocity = Vector2.down * speed;
                isMoving = true;
                audioSource.PlayOneShot(hittedSound);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb.velocity = Vector2.right * speed;
                isMoving = true;
                audioSource.PlayOneShot(hittedSound);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rb.velocity = Vector2.left * speed;
                isMoving = true;
                audioSource.PlayOneShot(hittedSound);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("出界!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.name == "Wall-up")
        {
            rb.velocity = Vector2.zero;SceneManager.LoadScene(3);
            isMoving = false;
        }
        if (other.gameObject.name == "Play")
        {
            SceneManager.LoadScene(3);
        }
        if (other.gameObject.name == "Setting")
        {
            SceneManager.LoadScene(1);
        }
        if (other.gameObject.tag == "Play")
        {
            SceneManager.LoadScene(4);
        }
        if (other.gameObject.name == "Quit")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            Application.Quit();
        }

        if (other.gameObject.tag == "Box")
        {
            audioSource.Play();
            rb.velocity = Vector2.zero;
            isMoving = false;
        }
        if (other.gameObject.name == "End")
        {
            audioSource.PlayOneShot(endSound);
            rb.velocity = Vector2.zero;
            Invoke("NextLevel",0.5f);
        }
    }
    void NextLevel()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}


