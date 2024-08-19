using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string nextLevelName;
    public int wienerPoints;
    public bool defeated, victorious, shrinking;
    public LayerMask layerMask;
    public GameObject frontGFX;
    SpriteRenderer frontSPriteRenderer;

    public List<GameObject> torsoList;


    [SerializeField]
    float hopDuration;
    [SerializeField]
    float defeatDuration;
    [SerializeField]
    float victoryDuration;
    [SerializeField]
    GameObject torsoPrefab;
    [SerializeField]
    Sprite curveSprite;



    GameObject tailObject;

    ChangeSceneBehaviour changeSceneScript;

    Vector3 newDirection, lastDirection,lastPosition,lastTorsoDirection, currentPosition, newPosition;

    public Vector3 currentDirection;

    float hopTimer, shrinkTimer, inputX, inputZ, defeatTimer, victoryTimer;

    int torsoIndex;

    


    // Start is called before the first frame update
    void Start()
    {
        //Mais tarde associar isso com a posi��o do check point
        lastPosition = new Vector3(-10, 0, -10);        

        tailObject = GameObject.FindGameObjectWithTag("Tail");
        changeSceneScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ChangeSceneBehaviour>();
        frontSPriteRenderer = frontGFX.GetComponent<SpriteRenderer>();
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
        ManageSpriteRotation();

        if (!defeated)
        {
            if (hopTimer < hopDuration)
            {
                if (hopTimer < hopDuration)
                {
                    hopTimer += Time.deltaTime;
                }
                else
                {
                   
                    //...caso tenha uma dire��o ele se move nessa dire��o.
                    if (currentDirection != Vector3.zero)
                    {
                        //frontGFX.transform.eulerAngles = currentRotation;
                        Strech();                        
                        Hop();                        
                    }
                    //...do contr�rio fica parado.
                    hopTimer = 0f;
                }
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
       

        //Se a nova dire��o n�o for para tr�s, ou seja o c�o n�o estiver voltando, ele pode mudar sua dire��o.
        if(transform.position + newDirection != lastPosition)
        {
            lastDirection = currentDirection;
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

        newTorso = Object.Instantiate(torsoPrefab, transform.position, frontGFX.transform.rotation);      
        
        Torso torsoScript = newTorso.GetComponent<Torso>();

        torsoScript.direction = currentDirection;

        torsoList.Add(newTorso);               

        //Se a dire��o atual for diferente da dire��o do �ltimo torso...
        if (torsoList.Count > 1 &&  torsoScript.direction != lastTorsoDirection)
        {            
            torsoScript.spriteRenderer.sprite = curveSprite;
            
            //Movimento Horizontal
            if(currentDirection.z == 0)
            {
                //Se o jogador estiver indo para a direita...
                if (currentDirection.x == 1)
                {
                    //...e estava descendo no �ltimo torso.
                    if (lastTorsoDirection.z == -1)
                    {
                        newTorso.transform.eulerAngles = new Vector3(90, 90, 0);                        
                    }
                    
                    else if(lastTorsoDirection.z == 1) 
                    {
                        newTorso.transform.eulerAngles = new Vector3(90, 180, 0);
                    }
                }
                //...se ojogador estiver indo para a esquerda...
                else if(currentDirection.x == -1)
                {
                    //...e estava descendo no �ltimo torso.
                    if (lastTorsoDirection.z == -1)
                    {
                        newTorso.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                    //...estava subindo no ultimo torso.
                    else if (lastTorsoDirection.z == 1)
                    {
                        newTorso.transform.eulerAngles = new Vector3(90, 180, 0);
                        torsoScript.spriteRenderer.flipX = true;
                    }
                }
            }
            else
            {
                //...estava sunindo.
                if(currentDirection.z == 1)
                {
                    if (lastTorsoDirection.x == 1)
                    {
                        newTorso.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                    else if(lastTorsoDirection.x == -1)
                    {
                        newTorso.transform.eulerAngles = new Vector3(90, 0, 0);
                        torsoScript.spriteRenderer.flipX = true;
                    }
                }
                //...estava descendo
                else if(currentDirection.z == -1)
                {
                    if (lastTorsoDirection.x == 1)
                    {
                        newTorso.transform.eulerAngles = new Vector3(90, -90, 0);
                    }
                    else if (lastTorsoDirection.x == -1)
                    {
                        newTorso.transform.eulerAngles = new Vector3(90, 90, 0);
                        torsoScript.spriteRenderer.flipX = true;
                    }
                }
            }
            
        }

        lastTorsoDirection =  torsoScript.direction;
        
        wienerPoints--;
    }
    private void Hop()
    {
        lastPosition = transform.position;
        transform.position += currentDirection;

    }

    private void Defeat()
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

    private void ManageSpriteRotation()
    {
        ////////////////////////Parte da Frente
        if(lastDirection == Vector3.zero)
        {
            lastDirection = new Vector3(1, 0, 0);
        }
        
        //Movimento Horizontal 
        if(currentDirection.z == 0)
        {
            //Reseta a rota��o do sprite.
            if(frontGFX.transform.rotation.y != 0f)
            {
                frontGFX.transform.eulerAngles = new Vector3(90, 0, 0);
            }

            //Flipa o sprite caso esteja indo para a esquerda.
            if(currentDirection.x == 1 && frontSPriteRenderer.flipX == true)
            {
                frontSPriteRenderer.flipX = false;
            }
            else if(currentDirection.x == -1 && frontSPriteRenderer.flipX == false)
            {
                frontSPriteRenderer.flipX = true;
            }
        }
        //Movimento Vertical
        else
        {
            //Se estiver subindo
            if(currentDirection.z == 1)
            {
                //Se veio da direita.
                if(lastDirection.x == 1)
                {
                    frontGFX.transform.eulerAngles = new Vector3(90, -90,0);
                    //currentRotation = new Vector3(90, -90, 0);
                }
                else if(lastDirection.x == -1)
                {
                    frontGFX.transform.eulerAngles = new Vector3(90, 90, 0);
                    //currentRotation = new Vector3(90, 90, 0);
                }
            }
            else if(currentDirection.z == -1)
            {
                //Se veio da direita.
                if (lastDirection.x == 1)
                {
                    frontGFX.transform.eulerAngles = new Vector3(90, 90, 0);
                    //currentRotation = new Vector3(90, 90, 0);
                }
                else if (lastDirection.x == -1)
                {
                    frontGFX.transform.eulerAngles = new Vector3(90, -90, 0);
                    //currentRotation = new Vector3(90, -90, 0);
                }
            }
        }
    }

    private void CalculateCurveRotation()
    {

    }

    private void ManageVictory()
    {
        if (victoryTimer < victoryDuration)
        {
            victoryTimer += Time.deltaTime;
            //Comporamentos de VIT�RIA!!!
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
