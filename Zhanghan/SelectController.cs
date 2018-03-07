using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Input cs file
public class SelectController : MonoBehaviour {
    public AnimationCurve ac;
    public Vector3 curPos;
    Transform originTransform;
    Vector3 targetPos;
    Vector3 offset;
    bool isMoving;

    public enum state { confirm, cancel,idle };
    public state curState;

    void Start () {
        isMoving = false;
        curState = state.idle;
        offset = new Vector3(0.5f, 0.5f, 0);
	}

	// Update is called once per frame
	void Update () {
        if (isMoving == false)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
                moveLeft();
            else if (Input.GetKeyUp(KeyCode.RightArrow))
                moveRight();
            else if (Input.GetKeyUp(KeyCode.UpArrow))
                moveUp();
            else if (Input.GetKeyUp(KeyCode.DownArrow))
                moveDown();



            if (Input.GetKeyUp(KeyCode.X))
                curState = state.confirm;
            else if (Input.GetKeyUp(KeyCode.Z))
                curState = state.cancel;
            else
                curState = state.idle;
        }
        curPos = transform.position;
        transform.localScale = new Vector3(ac.Evaluate(Time.time), ac.Evaluate(Time.time), ac.Evaluate(Time.time));
        print(curState);
    }
    void moveLeft()
    {

         targetPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        isMoving = true;
        StartCoroutine(SquareMove());
    }
    void moveRight()
    {

        targetPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        isMoving = true;
        StartCoroutine(SquareMove());
    }
    void moveUp()
    {

        targetPos = new Vector3(transform.position.x , transform.position.y+1, transform.position.z);
        isMoving = true;
        StartCoroutine(SquareMove());
    }
    void moveDown()
    {

        targetPos = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
        isMoving = true;
        StartCoroutine(SquareMove());
    }
    IEnumerator SquareMove()
    {
        while (this.transform.position != targetPos)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 0.5f);
            if ((this.transform.position - targetPos).magnitude <= 0.05)
                this.transform.position = targetPos;
            yield return null;
        }
        isMoving = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {

                print(collision.gameObject.name);

        }
    }

    // -MARK: move functions
    /*Keep the move functions down here*/

}
