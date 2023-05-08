using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Define delegate types and events here

    public Node CurrentNode { get; private set; }
    public Node TargetNode { get; private set; }

    [SerializeField] private float speed = 4;
    [SerializeField] private float stoppingDistance;
    private bool moving = false;
    private Vector3 currentDir;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Node node in GameManager.Instance.Nodes)
        {
            if(node.Parents.Length > 2 && node.Children.Length == 0)
            {
                CurrentNode = node;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == false)
        {
            //Implement inputs and event-callbacks here
            PlayerMoveInput();
        }
        else
        {
            if (Vector3.Distance(transform.position, TargetNode.transform.position) > stoppingDistance)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                moving = false;
                CurrentNode = TargetNode;
            }
        }
    }
    
    //Implement mouse interaction method here


    /* <summary>
    * Sets the players target node and current directon to the specified node.
    * </summary>
    * <param name="node"></param>
    */

    public void MoveToNode(Node node)
    {
        if (moving == false)
        {
            TargetNode = node;
            currentDir = TargetNode.transform.position - transform.position;
            currentDir = currentDir.normalized;
            moving = true;
        }
    }

    public void PlayerMoveInput()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("left input detected");
            CheckForNode(-Vector3.right);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("right input detected");
            CheckForNode(Vector3.right);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Debug.Log("down input detected");
            CheckForNode(-Vector3.forward);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            Debug.Log("up input detected");
            CheckForNode(Vector3.forward);
        }
    }

    public void PlayerMouseInput(int direction)
    {
        /*take in int to determine direction
        * 0 = North
        * 1 = East
        * 2 = South
        * 3 = West
        */

        switch (direction)
        {
            case 0:
                CheckForNode(Vector3.forward);
                return;
            case 1:
                CheckForNode(Vector3.right);
                return;
            case 2:
                CheckForNode(-Vector3.forward);
                return;
            case 3:
                CheckForNode(-Vector3.right);
                return;
        }
    }

    public void CheckForNode(Vector3 checkDirection)
    {

        RaycastHit hit;
        Node node;

        if (Physics.Raycast(transform.position, checkDirection, out hit, 50f))
        {
            if (hit.collider.TryGetComponent<Node>(out node))
            {
                Debug.DrawRay(transform.position, checkDirection * 100000, Color.white);
                MoveToNode(node);
                Debug.Log("hit " + node.name);
            }
        }
        else
        {
            Debug.Log("no valid target");
        }
    }
}
