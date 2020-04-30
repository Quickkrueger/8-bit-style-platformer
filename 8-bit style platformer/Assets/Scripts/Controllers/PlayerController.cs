using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float speed;

    Rigidbody2D m_RigidBody2D;
    int m_PlayerNum;
    GameController m_GameController;
    SpriteRenderer m_SpriteRenderer;
    Sprite m_PlayerSprite;
    bool m_Walking;
    bool m_Jumping;

    public void IntializePlayer(int number)
    {
        m_PlayerNum = number;
        m_Jumping = false;
        m_Walking = false;
        m_GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        m_PlayerSprite = Sprite.Create(m_GameController.SetPalette(m_PlayerNum, 0, 120), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = m_PlayerSprite;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_" + m_PlayerNum.ToString());

        if (Input.GetButton("Horizontal_" + m_PlayerNum.ToString()))
        {
            Walk(moveHorizontal);
        }
        else if (Input.GetButtonUp("Horizontal_" + m_PlayerNum.ToString()))
        {
            StopWalk();
        }

        if (Input.GetButton("Jump_" + m_PlayerNum.ToString()) && !m_Jumping)
        {
            Jump();
        }

        CheckCollisions();

    }

    private void Jump()
    {
        m_Jumping = true;
        transform.Translate(transform.up * 0.1f);
        m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 8f);
        StartCoroutine("JumpCycle");
        if (m_Walking)
        {
            StopCoroutine("WalkCycle");
        }
    }

    private void Falling()
    {
        m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y - 10 * Time.deltaTime);
    }

    private void CheckCollisions()
    {
        //{UP, DOWN, LEFT, RIGHT}
        string[] collisions = CollisionHandler.CheckForCollisions(transform);
        CheckForCieling(collisions[0]);
        CheckForGround(collisions[1]);
        CheckLeftCollision(collisions[2]);
        CheckRightCollision(collisions[3]);

    }

    private void CheckForCieling(string tag)
    {
        if(tag == "Ground" && m_Jumping)
        {
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 0f);
            transform.Translate(transform.up * -1f * Time.deltaTime);
        }
    }

    private void CheckForGround(string tag)
    {
        if(tag == "Ground" && m_Jumping)
        {
            m_Jumping = false;
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 0f);
            StopCoroutine("JumpCycle");
            m_PlayerSprite = Sprite.Create(m_GameController.SetPalette(m_PlayerNum, 0, 120), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
            m_SpriteRenderer.sprite = m_PlayerSprite;
            if (m_Walking)
            {
                StartCoroutine("WalkCycle");
            }
        }
        else if(tag != "Ground")
        {
            Falling();
            if (!m_Jumping)
            {
                m_Jumping = true;
                StartCoroutine("JumpCycle");
            }
        }
    }

    private void CheckLeftCollision(string tag)
    {
        if (tag == "Ground" && m_Jumping)
        {
            m_RigidBody2D.velocity = new Vector2(0f, m_RigidBody2D.velocity.y);
            transform.Translate(transform.right * Time.deltaTime);
        }
    }

    private void CheckRightCollision(string tag)
    {
        if (tag == "Ground" && m_Jumping)
        {
            m_RigidBody2D.velocity = new Vector2(0f, m_RigidBody2D.velocity.y);
            transform.Translate(transform.right * -1f * Time.deltaTime);
        }
    }

    private void Walk(float moveHorizontal)
    {
        if (moveHorizontal < 0)
        {
            m_SpriteRenderer.flipX = true;
        }
        else
        {
            m_SpriteRenderer.flipX = false;
        }
        if (!m_Walking && !m_Jumping)
        {
            m_Walking = true;
            StartCoroutine("WalkCycle");
        }
        m_RigidBody2D.velocity = new Vector2(moveHorizontal * speed * Time.deltaTime, m_RigidBody2D.velocity.y);
    }

    private void StopWalk()
    {
            m_Walking = false;
            StopCoroutine("WalkCycle");
            m_PlayerSprite = Sprite.Create(m_GameController.SetPalette(m_PlayerNum, 0, 120), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
            m_SpriteRenderer.sprite = m_PlayerSprite;
            m_RigidBody2D.velocity = new Vector2(0f, m_RigidBody2D.velocity.y);
    }

    IEnumerator WalkCycle()
    {
        m_PlayerSprite = Sprite.Create(m_GameController.SetPalette(m_PlayerNum, 8, 120), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
        m_SpriteRenderer.sprite = m_PlayerSprite;
        yield return new WaitForSeconds(0.3f);
        m_PlayerSprite = Sprite.Create(m_GameController.SetPalette(m_PlayerNum, 0, 120), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
        m_SpriteRenderer.sprite = m_PlayerSprite;
        yield return new WaitForSeconds(0.3f);

        StartCoroutine("WalkCycle");
    }

    IEnumerator JumpCycle()
    {
        m_PlayerSprite = Sprite.Create(m_GameController.SetPalette(m_PlayerNum, 8, 120), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
        m_SpriteRenderer.sprite = m_PlayerSprite;
        yield return new WaitForSeconds(0.3f);
        m_PlayerSprite = Sprite.Create(m_GameController.SetPalette(m_PlayerNum, 16, 120), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
        m_SpriteRenderer.sprite = m_PlayerSprite;
        yield return new WaitForSeconds(0.3f);

        StartCoroutine("JumpCycle");
    }




}
