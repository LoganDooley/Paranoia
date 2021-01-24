using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBus : MonoBehaviour
{
    public float busSpeed = 3f;
    public Rigidbody2D rb;
    private bool active;
    public AudioSource theme;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    public void start()
    {
        active = true;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            rb.MovePosition(rb.position + Vector2.right * busSpeed * Time.fixedDeltaTime);

            if (rb.position.x >= 15f)
            {
                SceneManager.LoadScene("House");
            }
            theme.volume = theme.volume * 0.996f;
        }
    }
}
