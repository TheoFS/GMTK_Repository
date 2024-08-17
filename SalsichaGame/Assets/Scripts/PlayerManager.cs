using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int wienerPoints;
    public bool defeated;

    [SerializeField]
    float hopDuration;
    [SerializeField]
    float defeatDuration;
    [SerializeField]
    GameObject torsoPrefab;

    Vector3 newDirection, currentDirection, lastPosition, currentPosition, newPosition;

    float hopTimer, inputX, inputZ, defeatTimer;

    // Start is called before the first frame update
    void Start()
    {
        //Mais tarde associar isso com a posição do check point
        lastPosition = new Vector3(-10, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        DefineMovementDirection();

        if (!defeated)
        {
            if (hopTimer < hopDuration)
            {
                hopTimer += Time.deltaTime;
            }
            else
            {
                if (EmptyNewPosition() && currentDirection != Vector3.zero && wienerPoints > 0)
                {
                    Strech();
                    Hop();
                }
                else
                {
                    //Comportamento de derrota
                    //Defeat();
                }

                hopTimer = 0f;
            }
        }
        else
        {
            //Defeat();
        }
    }

    private void GetPlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        //if()
    }

    private void DefineMovementDirection()
    {
        

        if(inputX != 0f || inputZ != 0f)
        {
            if(inputZ == 0)
            {
                newDirection = new Vector3(inputX, 0, 0);
            }
            else if(inputX == 0f)
            {
                newDirection = new Vector3(0, 0, inputZ);
            }
        }
        else
        {

        }

        //Se a nova direção não for para trás, ou seja o cão não estiver voltando, ele pode mudar sua direção.
        if(transform.position + newDirection != lastPosition)
        {
            currentDirection = newDirection;
        }
    }

    
    private bool  EmptyNewPosition()
    {
        if(Physics.Raycast(transform.position, currentDirection, out RaycastHit hitInfo, 1f))
        {
            Debug.Log("I hit the " + hitInfo.collider.name);
            return false;
        }
        else
        {
            return true;
        }
       
    }

    private void Strech()
    {
        Object.Instantiate(torsoPrefab, transform.position, transform.rotation);
        wienerPoints--;
    }
    private void Hop()
    {
        lastPosition = transform.position;
        transform.position += currentDirection;

    }

    private void Defeat()
    {
        if(defeatTimer < defeatDuration)
        {

        }
        else
        {

        }
    }
}
