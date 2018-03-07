using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    GameObject moveSquareHolder;
    GameObject attackSquareHolder;
    Map map=new Map();

    int range = 4;//move range
    float moveSpeed = 2;

    enum characterState { turnStart,selected,moveStart,moveEnd,turnEnd}//未行动，被选中，已移动，已行动 
    characterState curState;
 
    void Start () {
        //select = GameObject.FindGameObjectWithTag("selectSquare").GetComponent<SelectController>();
        moveSquareList = new List<Vector3>();
        attackSpuareList= new List<Vector3>();
        curState = characterState.turnStart;
        GetComponent<Renderer>().sortingOrder = -(int)transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
       
        setState();


    }

    void SelectCharacter()
    {
        CreateMoveSquares();
        CreateAttackSquare();
    }


    void CreateMoveSquares()
    {
        GetSpuareListInRange(range,moveSquareList);
        moveSquareHolder = new GameObject("moveSpuares");
        foreach(Vector3 v in moveSquareList)
        {
            Vector3 spuarePos = map.MapToWorld(v,squareOffset);
            GameObject instance= Instantiate(moveSquare, spuarePos, Quaternion.identity);
            instance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            instance.transform.SetParent(moveSquareHolder.transform);

        }
    }
    void CreateAttackSquare()
    {
        attackSquareHolder = new GameObject("attackSquareHolder");
        List<Vector3> temp = new List<Vector3>();
        GetSpuareListInRange(range + 1, temp);
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



    IEnumerator CharacterMove(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed*Time.deltaTime);
            GetComponent<Renderer>().sortingOrder = -(int)transform.position.y;
            yield return null;
        }
        curState = characterState.moveEnd;
        print("Pop Menu!");

    }
    void MoveTo(Vector3 targetPos)
    {
        curState = characterState.moveStart;
        StartCoroutine(CharacterMove(map.MapToWorld(targetPos,characterOffset)));
        Destroy(moveSquareHolder);
        Destroy(attackSquareHolder);
    }
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
}
