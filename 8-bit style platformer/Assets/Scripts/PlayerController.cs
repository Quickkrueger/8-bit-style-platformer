using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{

    public float speed;

    Rigidbody2D m_RigidBody2D;
    BoxCollider2D m_BoxCollider2D;
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
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = m_PlayerSprite;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_" + m_PlayerNum.ToString());
        float jump = Input.GetAxis("Jump_" + m_PlayerNum.ToString());
        if(Mathf.Abs(moveHorizontal) > 0.1f)
        {
            if(moveHorizontal < 0)
            {
                m_SpriteRenderer.flipX = true;
            }
            else {
                m_SpriteRenderer.flipX = false;
            }
            if (!m_Walking)
            {
                m_Walking = true;
                StartCoroutine("WalkCycle");
            }
            m_RigidBody2D.velocity = new Vector2(moveHorizontal * speed, m_RigidBody2D.velocity.y);
        }
        else if(Mathf.Abs(moveHorizontal) < 0.1f)
        {
            m_Walking = false;
            StopCoroutine("WalkCycle");
            m_PlayerSprite = Sprite.Create(m_GameController.SetPalette(m_PlayerNum, 0, 120), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
            m_SpriteRenderer.sprite = m_PlayerSprite;
            m_RigidBody2D.velocity = new Vector2(0f, m_RigidBody2D.velocity.y);
        }

        if (Mathf.Abs(jump) > 0.1f && !m_Jumping)
        {
            if (jump < 0)
            {
                m_SpriteRenderer.flipX = true;
            }
            else
            {
                m_SpriteRenderer.flipX = false;
            }
            if (!m_Jumping)
            {
                m_Jumping = true;
                m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 8f);
                StartCoroutine("JumpCycle");
            }
          }
        //else if(Mathf.Abs(jump) < 0.1f && m_Jumping)
        //{
        //    m_Jumping = false;
        //    StopCoroutine("JumpCycle");
        //}
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

        m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y -1f);

        StartCoroutine("JumpCycle");
    }




}
