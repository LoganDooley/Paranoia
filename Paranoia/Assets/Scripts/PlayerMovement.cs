using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float shadowSpeed = 10f;
    public Rigidbody2D rb;
    public Rigidbody2D mrb;
    public Rigidbody2D shadowrb;
    public Rigidbody2D shadow2rb;
    public Animator animator;
    public Animator manimator;
    public Collider2D hkdoor;
    public Collider2D khdoor;
    public Collider2D hbdoor;
    public Collider2D bhdoor;
    public Collider2D mirror;
    public Collider2D window;
    public Collider2D picture;
    public GameObject iprompt;
    public GameObject shadow;
    public GameObject shadow2;
    public GameObject health;
    public Text display;
    private string interracting = null;
    private Queue<string> mirrorwords;
    private Queue<string> windowwords;
    private Queue<string> picturewords;
    Vector2 movement;
    float facing = -1f;
    private bool mirrori = false;
    private bool picturei = false;
    private bool windowi = false;
    private bool shadowAct = false;
    private bool shadow2Act = false;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        this.SetMirrorQueue();
        this.SetWindowQueue();
        this.SetPictureQueue();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(interracting);
        this.CalculatePitch();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        manimator.SetFloat("Horizontal", movement.x);
        manimator.SetFloat("Vertical", movement.y);
        manimator.SetFloat("Speed", movement.sqrMagnitude);
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            facing = 1f;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            facing = -1f;
        }
        animator.SetFloat("Facing", facing);
        manimator.SetFloat("Facing", facing);
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
        {
            if (interracting == null)
            {
                iprompt.SetActive(false);
                display.text = "Press E to interract";
            }
        }
        if(interracting == "mirror")
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                if(mirrorwords.Count != 0)
                {
                    display.text = mirrorwords.Dequeue();
                    if(mirrorwords.Count == 2)
                    {
                        manimator.SetBool("Blood", true);
                        mirrori = true;
                    }
                    if(mirrorwords.Count == 1)
                    {
                        manimator.SetBool("Blood", false);
                    }
                }
                else
                {
                    this.IntStop();
                    this.SetMirrorQueue();
                }
            }
        }
        else if (interracting == "window")
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                if (windowwords.Count != 0)
                {
                    display.text = windowwords.Dequeue();
                    if (windowwords.Count == 3)
                    {
                        windowi = true;
                        shadow.SetActive(true);
                        shadowAct = true;
                    }
                    else if (windowwords.Count == 2)
                    {
                        shadowAct = false;
                        shadow.SetActive(false);
                    }
                }
                else
                {
                    this.IntStop();
                    this.SetWindowQueue();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) || interracting == "picture")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (picturewords.Count != 0)
                {
                    display.text = picturewords.Dequeue();
                    if (picturewords.Count == 1)
                    {
                        picturei = true;
                        shadow2.SetActive(true);
                        shadow2Act = true;
                    }
                    else if (picturewords.Count == 0)
                    {
                        shadow2Act = false;
                        shadow2.SetActive(false);
                    }
                }
                else
                {
                    this.IntStop();
                    this.SetPictureQueue();
                }
            }
        }
        else if(picturei && windowi && mirrori)
        {
            SceneManager.LoadScene("End");
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);
        if (shadowAct)
        {
            Vector2 shadowmove = new Vector2(0.1f, 0f);
            shadowrb.MovePosition(shadowrb.position - shadowmove * shadowSpeed * Time.fixedDeltaTime);
        }
        if (shadow2Act)
        {
            Vector2 shadow2move = new Vector2(0.1f, 0f);
            shadow2rb.MovePosition(shadow2rb.position - shadow2move * shadowSpeed * Time.fixedDeltaTime);
        }
    }

    public void Teleport(float x, float y)
    {
        Vector2 transport;
        transport.x = rb.position.x - x;
        transport.y = rb.position.y - y;
        rb.MovePosition(rb.position + transport);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == hkdoor){
            transform.position = new Vector3(-46.5f, 11.54f, -3f);
        }
        if(collision == khdoor)
        {
            transform.position = new Vector3(-0.96f, 18.11f, -3f);
        }
        if (collision == hbdoor)
        {
            transform.position = new Vector3(47.54f, 11.6f, -3f);
        }
        if (collision == bhdoor)
        {
            transform.position = new Vector3(8.65f, 20.42f, -3f);
        }
        if(collision == mirror)
        {
            this.MirrorInt();
        }
        if(collision == window)
        {
            this.WindowInt();
        }
        if(collision == picture)
        {
            this.PictureInt();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.IntStop();
    }
    private void MirrorInt()
    {
        iprompt.SetActive(true);
        interracting = "mirror";
    }
    private void IntStop()
    {
        iprompt.SetActive(false);
        display.text = "Press E to interract";
        this.SetMirrorQueue();
        this.SetWindowQueue();
        this.SetPictureQueue();
        interracting = null;
        shadowAct = false;
        shadow.SetActive(false);
        shadow2Act = false;
        shadow2.SetActive(false);
    }
    private void WindowInt()
    {
        iprompt.SetActive(true);
        interracting = "window";
    }
    private void PictureInt()
    {
        iprompt.SetActive(true);
        interracting = "picture";
    }
    public void SetMirrorQueue()
    {
        if (!mirrori)
        {
            mirrorwords = new Queue<string>();
            mirrorwords.Enqueue("You look in the mirror.");
            mirrorwords.Enqueue("There is something on your face.");
            mirrorwords.Enqueue("");
            mirrorwords.Enqueue("\"What was that?\"");
            mirrorwords.Enqueue("\"Must have been my imagination.\"");
        }
        else
        {
            mirrorwords = new Queue<string>();
            mirrorwords.Enqueue("It's just a mirror.");
        }
    }
    public void SetWindowQueue()
    {
        if (!windowi)
        {
            windowwords = new Queue<string>();
            windowwords.Enqueue("You look outside.");
            windowwords.Enqueue("The sunset is kind of cool.");
            windowwords.Enqueue("");
            windowwords.Enqueue("\"Who was that?!\"");
            windowwords.Enqueue("You look outside again. Nobody is there.");
            windowwords.Enqueue("\"My eyes are playing tricks on me.\"");
        }
        else
        {
            windowwords = new Queue<string>();
            windowwords.Enqueue("Nobody is there.");
        }
    }
    public void SetPictureQueue()
    {
        if (!picturei)
        {
            picturewords = new Queue<string>();
            picturewords.Enqueue("You look at the photograph.");
            picturewords.Enqueue("\"The beach was always so much fun\"");
            picturewords.Enqueue("");
            picturewords.Enqueue("\"...\"");
        }
        else
        {
            picturewords = new Queue<string>();
            picturewords.Enqueue("It's a picture of you at the beach.");
        }
    }
    private void CalculatePitch()
    {
        float count = 1;
        float count2 = 1;
        if (mirrori)
        {
            count -= 0.25f;
            count2 -= 0.33f;
        }
        if (windowi)
        {
            count -= 0.25f;
            count2 -= 0.33f;
        }
        if (picturei)
        {
            count -= 0.25f;
            count2 -= 0.33f;
        }
        audio.pitch = count;
        health.transform.localScale = new Vector3(count2, 1, 1);
    }
}
