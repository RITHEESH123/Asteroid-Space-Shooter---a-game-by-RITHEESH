using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public float min_Y, max_Y;


    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private Transform attackPoint;

    public float attack_Timer = 1.8f;
    private float current_Attack_Timer;
    private bool canAttack;
    private bool canShoot;

    public AudioSource shootSound;
    public AudioSource Destroyed;
    private Animator anim;

    private Text scoreText;
    public int score;

    void Awake()
    {
        //shootSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseScore());
        current_Attack_Timer = attack_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Attack();
    }

    void MovePlayer()
    {
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > max_Y)
            {
                temp.y = max_Y;
            }

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < min_Y)
            {
                temp.y = min_Y;
            }

            transform.position = temp;
        }
    }

    public void Up() //for mobile Input
    {
        Vector3 temp = transform.position;
        temp.y += speed * 7f * Time.deltaTime;

        if (temp.y > max_Y)
        {
            temp.y = max_Y;
        }

        transform.position = temp;
    }

    public void Down() //for mobile Input
    {
        Vector3 temp = transform.position;
        temp.y -= speed * 7f * Time.deltaTime;

        if (temp.y < min_Y)
        {
            temp.y = min_Y;
        }

        transform.position = temp;
    }

    void Attack()
    {
        attack_Timer += Time.deltaTime;
        if (attack_Timer > current_Attack_Timer)
        {
            canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) || canShoot)
        {
            if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0;

                Instantiate(playerBullet, attackPoint.position, Quaternion.identity);

                //play the soundFX
                shootSound.Play();
            }
            canShoot = false;
        }
    }

    public void Shoot()
    {
        canShoot = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag=="Enemy" || target.tag == "EnemyBullet")
        {
            anim.Play("Destroy");
            Destroyed.Play();
            Invoke("DeactivatePlayer", 0.5f);
            Invoke("Restart", 2f);
        }
    }

    void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }

    void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator IncreaseScore()
    {
        yield return new WaitForSeconds(1);
        score ++;
        scoreText.text = "YOUR SCORE : " + score;
        StartCoroutine(IncreaseScore());
    }
}
