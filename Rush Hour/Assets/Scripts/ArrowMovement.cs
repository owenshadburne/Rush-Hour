using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 allignment;
    SpriteRenderer sr;
    Color reg;
    InputManager inputManager;

    [SerializeField] Type type;
    Vector2 tempMove;

    bool running = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        allignment = transform.position;
        sr = GetComponent<SpriteRenderer>();
        reg = sr.color; 
        inputManager = InputManager.instance;
    }

    void Update()
    {
        Recolor();
        Move();
    }
    void Recolor()
    {
        if (transform == inputManager.ClickedCar())
        {
            sr.color = Color.yellow;
        }
        else
        {
            sr.color = reg;
        }
    }
    void Move()
    {
        if (transform == inputManager.ClickedCar())
        {
            Vector2 move = GetVector();
            float difference = Mathf.Abs(move.x - transform.position.x) + Mathf.Abs(move.y - transform.position.y);
            if (difference >= 0.1f && IsValidMove())
            {
                inputManager.Moves++;
                allignment = move;
                transform.position = Vector2.MoveTowards(rb.position, move, 1);
            }
        }
    }

    bool IsValidMove()
    {
        float len = type == Type.Horizontal ? transform.localScale.x: transform.localScale.y;
        len = (len - 1) * .5f + 1;
        return !Physics2D.Raycast(transform.position, tempMove, len, InputManager._moveable) && !Physics2D.Raycast(transform.position, tempMove, len, InputManager._defRay);
    }

    private void FixedUpdate()
    {
        if(transform == inputManager.ClickedCar())
        {
            gameObject.layer = 7;
        }
        else
        {
            gameObject.layer = 6;
            transform.position = allignment;
        }
    }

    Vector2 GetVector()
    {
        float x = 0, y = 0;
        if (type == Type.Horizontal) 
        { 
            x = Input.GetKeyDown(KeyCode.D) ? 1 : Input.GetKeyDown(KeyCode.A) ? -1 : 0; 
        }
        else if (type == Type.Vertical) 
        { 
            y = Input.GetKeyDown(KeyCode.W) ? 1 : Input.GetKeyDown(KeyCode.S) ? -1 : 0; 
        }
        tempMove = new Vector2(x, y);
        return new Vector2(x + allignment.x, y + allignment.y);
    }
}

public enum Type
{
    Horizontal,
    Vertical
}
