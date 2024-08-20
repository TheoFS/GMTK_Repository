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
    float shrinkDuration;
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

    Vector3 currentDirection, currentRotation;

    bool currentFrontFlip;

    float hopTimer, shrinkTimer, inputX, inputZ, defeatTimer, victoryTimer;

    int torsoIndex;

    


    // Start is called before the first frame update
    void Start()
    {
        //Calcular onde a bunda do dog está.
        lastPosition = new Vector3(transform.position.x - 1, 0, transform.position.z);


        currentRotation = new Vector3(90, 0, 0);

        tailObject = GameObject.FindGameObjectWithTag("Tail");
        changeSceneScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ChangeSceneBehaviour>();
        frontSPriteRenderer = frontGFX.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();

       

        
        DefineMovementDirection();
        ManageSnootRotation();

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

                    if (!EmptyNewPosition() || wienerPoints <= 0)
                    {

                        defeated = true;
                        Debug.Log("Defeated");
                    }


                    //...caso tenha uma direção ele se move nessa direção.
                    if (currentDirection != Vector3.zero && !defeated)
                    {
                        
                        frontGFX.transform.eulerAngles = currentRotation;
                        frontSPriteRenderer.flipX = currentFrontFlip;
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
       

        //Se a nova direção não for para trás, ou seja o cão não estiver voltando, ele pode mudar sua direção.
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

        if(lastTorsoDirection == Vector3.zero)
        {
            lastTorsoDirection = Vector3.right;
        }


        //Se a direção atual for diferente da direção do último torso...
        if (torsoScript.direction != lastTorsoDirection)
        {            
            torsoScript.spriteRenderer.sprite = curveSprite;
            
            //Movimento Horizontal
            if(currentDirection.z == 0)
            {
                //Se o jogador estiver indo para a direita...
                if (currentDirection.x == 1)
                {
                    //...e estava descendo no último torso.
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
                    //...e estava descendo no último torso.
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
                //Rotation
                Vector3 currentTorsoDirection = torsoList[torsoIndex].GetComponent<Torso>().direction; 
                if(currentTorsoDirection.z == 0)
                {
                    tailObject.GetComponent<Tail>().tailGFX.transform.eulerAngles = new Vector3(90, 0, 0);

                    if (currentTorsoDirection.x == 1)
                    {
                        tailObject.GetComponent<Tail>().spriteRenderer.flipX = false;
                        
                    }
                    else if(currentTorsoDirection.x == -1)
                    {
                        tailObject.GetComponent<Tail>().spriteRenderer.flipX = true;
                    }
                }
                else
                {
                    tailObject.GetComponent<Tail>().spriteRenderer.flipX = false;
                    if (currentTorsoDirection.z == 1)
                    {
                        tailObject.GetComponent<Tail>().tailGFX.transform.eulerAngles = new Vector3(90,-90,0);

                    }
                    else if (currentTorsoDirection.z == -1)
                    {
                        tailObject.GetComponent<Tail>().tailGFX.transform.eulerAngles = new Vector3(90, 90, 0);
                    }
                }
                //tailObject.transform.eulerAngles = ;
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
                defeated = false;
                Debug.Log("I win!!");
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

    private void ManageSnootRotation()
    {
        ////////////////////////Parte da Frente
        if(lastDirection == Vector3.zero)
        {
            lastDirection = new Vector3(1, 0, 0);
        }
        
        //Movimento Horizontal 
        if(currentDirection.z == 0)
        {
            //Reseta a rotação do sprite.
            currentRotation = new Vector3(90, 0, 0);

            //Flipa o sprite caso esteja indo para a esquerda.
            if (currentDirection.x == 1 && frontSPriteRenderer.flipX == true)
            {
                //frontSPriteRenderer.flipX = false;
                currentFrontFlip = false;
            }
            else if(currentDirection.x == -1 && frontSPriteRenderer.flipX == false)
            {
                //frontSPriteRenderer.flipX = true;
                currentFrontFlip = true;
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
                    //frontGFX.transform.eulerAngles = new Vector3(90, -90,0);
                    currentRotation = new Vector3(90, -90, 0);
                }
                else if(lastDirection.x == -1)
                {
                    //frontGFX.transform.eulerAngles = new Vector3(90, 90, 0);
                    currentRotation = new Vector3(90, 90, 0);
                }
            }
            else if(currentDirection.z == -1)
            {
                //Se veio da direita.
                if (lastDirection.x == 1)
                {
                    //frontGFX.transform.eulerAngles = new Vector3(90, 90, 0);
                    currentRotation = new Vector3(90, 90, 0);
                }
                else if (lastDirection.x == -1)
                {
                    //frontGFX.transform.eulerAngles = new Vector3(90, -90, 0);
                    currentRotation = new Vector3(90, -90, 0);
                }
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
        if (other.tag == "FinishLine")
        {
            shrinking = true;
            victorious = true;
            defeated = false;
        }

        Debug.Log("Im triggerd by " + other.name);
        
        if(other.tag == "CheckPoint")
        {
            Debug.Log("It was a checkpoint!");
            shrinking = true;
        }

        

        if(other.tag == "HotDog")
        {
            wienerPoints = 20;
            Destroy(other.gameObject);
        }
    }
}
