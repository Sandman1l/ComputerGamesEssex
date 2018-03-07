using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create the squares
//Everything should be just functions
//To be called upon by something else
/*
SelectorMov.cs (SelectController) ¡¡¡¡JUST THE SELECTOR MOVEMENT!!!! already done GOOD JOB

SquareCreator.cs (CharacterController/2) ¡¡¡¡JUST THE SQUARES!!!! basically done GOOD JOB

SelectionDetection.cs: (new script)
This one will detect if the "selector square" is on top of something.

  //This section needs a flag or state, IT CAN ONLY BE RUN ONCE AT A TIME
    [ // fix if (CharacterConfirm is enabled dont check this)
      ^IF there are any ACCEPT input do something:
          ACCEPT:
            SquareCreator
              ^if "character" know that we are about to move.
              Keep a reference to that character<- Who am I looking at?
              Keep original position
              Activate CharacterConfirm script,
                  this would be -> GM.GetComponent<CharacterConfirm>().enabled = true (something like this)
    ]
          Cancel:
              ^SquareCreator Erase
               Character goes back to original position. (DOES Not matter if it moved or not)
              De-activate CharacterConfirm script
              clear flags

CharacterConfirm.cs (CharacterController/2)
Basically the "confirm action" for a movement or attack
  move the character to the SelectorMov pos
  //pop up appears here <--- if you don't have it, its alright
      ^make the other two work on this
      SelectorMov disables
  DO NOT WRITE THE CONTROLS FOR THE POP UP HERE. DO NOT DO THAT...please.
  
  Two more options:
    Attack:
      SelectorMov enables
    //Call CombatManager (Not implemented yet)
    ^SquareCreator Erase
    clear flags on SelectionDetection.cs
    De-activate CharacterConfirm script

    Wait:
      ^SquareCreator Erase
      clear flags on SelectionDetection.cs
      De-activate CharacterConfirm script
*/
public class CharacterController : MonoBehaviour {

    public GameObject moveSquare;
    public GameObject attackSquare;
    public SelectController select;

    List<Vector3> moveSquareList;
    List<Vector3> attackSpuareList;

    Vector3 curPos;
    Vector3 prePos;
    Vector3 characterOffset=new Vector3(0.5f,1f,0);
    Vector3 squareOffset = new Vector3(0.5f, 0.5f, 0);
    //This is harder to debug
    //try with List<GameObject>
    GameObject moveSquareHolder;
    GameObject attackSquareHolder;
    Map map=new Map();

    int range = 4;//move range
    float moveSpeed = 2;

    enum characterState { turnStart,selected,moveStart,moveEnd,turnEnd}//未行动，被选中，已移动，已行动
    characterState curState;

    void Start () {
        //select = GameObject.FindGameObjectWithTag("selectSquare").GetComponent<SelectController>();
        //use vector 2
        moveSquareList = new List<Vector3>();
        attackSpuareList= new List<Vector3>();
        curState = characterState.turnStart;
        //unecessary
        GetComponent<Renderer>().sortingOrder = -(int)transform.position.y;
    }

	// Update is called once per frame
	void Update () {

        setState();

    }

    //Plase not.
    void SelectCharacter()
    {
        CreateMoveSquares();
        CreateAttackSquare();
    }


    void CreateMoveSquares()
    {
        GetSpuareListInRange(range,moveSquareList);
        //Squares not spuares

        moveSquareHolder = new GameObject("moveSpuares");
        //CharController.vtPosMov <- List of Vector2 of possible movementes.
        foreach(Vector3 v in moveSquareList)
        {
            Vector3 spuarePos = map.MapToWorld(v,squareOffset);
            GameObject instance = Instantiate(moveSquare, spuarePos, Quaternion.identity);
            instance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            instance.transform.SetParent(moveSquareHolder.transform);
            // moveSquareHolder.Add(instance) maybe something like this?

        }
    }

    void CreateAttackSquare()
    {
        attackSquareHolder = new GameObject("attackSquareHolder");
        List<Vector3> temp = new List<Vector3>();
        GetSpuareListInRange(range + 1, temp);
        //CharController.vtPosAtk <- List of Vector2 of possible attacks.
        foreach(Vector3 v in temp)
        {
            if (!moveSquareList.Contains(v))
            {
                attackSpuareList.Add(v);
                Vector3 spuarePos = map.MapToWorld(v, squareOffset);
                GameObject instance = Instantiate(attackSquare, spuarePos, Quaternion.identity);
                instance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                instance.transform.SetParent(attackSquareHolder.transform);
            }
        }
    }
    //Please not. hue hue
    //IF youre going to do this, at three functions, so you have a more defendable argument.
    void DestroyMoveSquare()
    {
        Destroy(moveSquareHolder);
        moveSquareList.Clear();
    }
    void DestroyAttactSquare()
    {
        Destroy(attackSquareHolder);
        attackSpuareList.Clear();
    }


    //make it faster...little bit.
    IEnumerator CharacterMove(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed*Time.deltaTime);
            GetComponent<Renderer>().sortingOrder = -(int)transform.position.y;
            yield return null;
        }
        curState = characterState.moveEnd;
        //CharController.movManager.UpdateGrid(newposition,CharController.mov)
        print("Pop Menu!");

    }
    void MoveTo(Vector3 targetPos)
    {
        curState = characterState.moveStart;
        StartCoroutine(CharacterMove(map.MapToWorld(targetPos,characterOffset)));
        Destroy(moveSquareHolder);
        Destroy(attackSquareHolder);
    }
    //
    void GetSpuareListInRange(int range,List<Vector3> list)
    {

        int posX = (int)(transform.position.x - 0.5f);
        int posY = (int)(transform.position.y-1 );
        for(int i=posX-range;i<=posX+range;i++)
            for(int j = posY-range; j <= posY+range; j++)
            {
                if ((Mathf.Abs(posX - i) + Mathf.Abs(posY - j)) <= range){
                    list.Add(new Vector3(i, j, 0));
                }

            }
    }

    void setState()
    {
        print("characterState:"+curState);
        if (select.curState == SelectController.state.confirm)
        {
            if (curState == characterState.turnStart&&InSelect())
            {
                print("selected");
                SelectCharacter();
                prePos = transform.position;
                curState = characterState.selected;
            }
            else if (curState == characterState.selected&&InMoveRange())
            {
                print("move to"+ map.WorldToMap(select.transform.position, squareOffset));
                MoveTo(map.WorldToMap(select.transform.position,squareOffset));
            }
            else if (curState == characterState.moveEnd)
            {
                print("show PopMenu!");
            }
        }
        else if (select.curState == SelectController.state.cancel)
        {
            if(curState == characterState.selected)
            {
                DestroyMoveSquare();
                DestroyAttactSquare();
                curState = characterState.moveStart;
            }
            if (curState == characterState.moveEnd)
            {
                transform.position = prePos;
                SelectCharacter();
                curState = characterState.selected;
            }
        }
    }


    bool InSelect()
    {
        return map.WorldToMap(select.curPos, squareOffset) == map.WorldToMap(transform.position, characterOffset);
    }
    bool InMoveRange()
    {
        return moveSquareList.Contains(map.WorldToMap(select.curPos, squareOffset));
    }




    // -MARK: Testing
    /* testing functions*/

}
