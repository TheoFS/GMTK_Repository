using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string nextLevelName;
    public int wienerPoints;
    public bool defeated, victorious, shrinking;
    public LayerMask layerMask;
    public List<GameObject> torsoList;

    [SerializeField]
    float hopDuration;
    [SerializeField]
    float shrinkDuration;
    [SerializeField]
    float defeatDuration;
    [SerializeField]
    float victoryDuration;
    [SerializeField]
    GameObject torsoPrefab;



    GameObject tailObject;

    ChangeSceneBehaviour changeSceneScript;

    Vector3 newDirection, lastPosition, currentPosition, newPosition;

    public Vector3 currentDirection;

    float hopTimer, shrinkTimer, inputX, inputZ, defeatTimer, victoryTimer;

    int torsoIndex;


    // Start is called before the first frame update
    void Start()
    {
        //Mais tarde associar isso com a posição do check point
        lastPosition = new Vector3(-10, 0, -10);

        tailObject = GameObject.FindGameObjectWithTag("Tail");
        changeSceneScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ChangeSceneBehaviour>();        
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        

        if(!EmptyNewPosition() || wienerPoints <= 0)
        {
            defeated = true;
            Debug.Log("Defeated");
        }

        DefineMovementDirection();

        if (!defeated)
        {
            if (!shrinking)
            {
                if (hopTimer < hopDuration)
                {
                    hopTimer += Time.deltaTime;
                }
                else
                {
                    //...caso tenha uma direção ele se move nessa direção.
                    if (currentDirection != Vector3.zero)
                    {
                        Strech();
                        Hop();
                    }
                    //...do contrário fica parado.
                    hopTimer = 0f;
                }
            }
            else
            {
                Shrink();
            }
        }
        else
        {
            ManageDefeat();
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
        //if (!contracting) { }

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
        /*
        else
        {

        }*/

        //Se a nova direção não for para trás, ou seja o cão não estiver voltando, ele pode mudar sua direção.
        if(transform.position + newDirection != lastPosition)
        {
            currentDirection = newDirection;
        }
    }

    
    private bool  EmptyNewPosition()
    {
        if (Physics.Raycast(transform.position, currentDirection, out RaycastHit hitInfo, 1f, layerMask))
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
        GameObject newTorso;
        
        newTorso = Object.Instantiate(torsoPrefab, transform.position, transform.rotation);

        torsoList.Add(newTorso);

        wienerPoints--;
    }
    private void Hop()
    {
        lastPosition = transform.position;
        transform.position += currentDirection;

    }

    private void Shrink()
    {
        if (torsoIndex < torsoList.Count)
        {
            if (shrinkTimer < shrinkDuration)
            {
                shrinkTimer += Time.deltaTime;
            }
            else
            {
                //GameObject torsoBit = torsoList[0];
                tailObject.transform.position = torsoList[torsoIndex].transform.position;
                torsoList[torsoIndex].GetComponent<BoxCollider>().enabled = false;
                torsoList[torsoIndex].transform.localScale = Vector3.zero;           
                shrinkTimer = 0f;
                torsoIndex++;
            }
        }
        else
        {
            if (victorious)
            {
                newDirection = Vector3.zero;
                ManageVictory();
            }
            else
            {
                shrinking = false;
                torsoIndex = 0;
                torsoList.Clear();
                newDirection = Vector3.zero;
            }
            
        }
    }

    private void ManageVictory()
    {
        if (victoryTimer < victoryDuration)
        {
            victoryTimer += Time.deltaTime;
            //Comporamentos de VITÓRIA!!!
        }
        else
        {
            changeSceneScript.ChangeScene(nextLevelName);
        }
    }
    private void ManageDefeat()
    {        
        if(defeatTimer < defeatDuration)
        {
            defeatTimer += Time.deltaTime;
            //COMPORTAMENTOS DE DERROTA!
        }
        else
        {
            changeSceneScript.ReloadScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Im triggerd by " + other.name);
        
        if(other.tag == "CheckPoint")
        {
            Debug.Log("It was a checkpoint!");
            shrinking = true;
        }

        if(other.tag == "FinishLine")
        {
            shrinking = true;
            victorious = true;
        }
    }
}
