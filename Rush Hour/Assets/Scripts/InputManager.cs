using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moveText;
    int numMoves;

    [SerializeField] LayerMask layerMask;
    float distance = 100;

    Transform clickedCar;
    public static InputManager instance;

    [SerializeField] LayerMask moveable, defRay;
    public static LayerMask _moveable, _defRay;

    void Awake()
    {
        instance = this;
        moveText.text = "0 Moves";
        numMoves = 0;
        _moveable = moveable;
        _defRay = defRay;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, distance, layerMask);

            if (hit)
            {
                clickedCar = hit.transform;
            }
            else
            {
                clickedCar = null;
            }
        }
    }

    public int Moves
    {
        get
        {
            return numMoves;
        }
        set
        {
            numMoves = value;
            moveText.text = numMoves.ToString() + " Moves";
        }
    }

    public Transform ClickedCar()
    {
        return clickedCar;
    }
}
