using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Movement speed modifier.")]
    [SerializeField] private float speed = 3;
    private Node currentNode;
    private Vector3 currentDir;
    private bool playerCaught = false;

    public delegate void GameEndDelegate();
    public event GameEndDelegate GameOverEvent = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        InitializeAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCaught == false)
        {
            if (currentNode != null)
            {
                //If within 0.25 units of the current node.
                if (Vector3.Distance(transform.position, currentNode.transform.position) > 0.25f)
                {
                    transform.Translate(currentDir * speed * Time.deltaTime);
                }
                //Implement path finding here
            }
            else
            {
                Debug.LogWarning($"{name} - No current node");
            }

            Debug.DrawRay(transform.position, currentDir, Color.cyan);
        }
    }

    //Called when a collider enters this object's trigger collider.
    //Player or enemy must have rigidbody for this to function correctly.
    private void OnTriggerEnter(Collider other)
    {
        if (playerCaught == false)
        {
            if (other.tag == "Player")
            {
                playerCaught = true;
                GameOverEvent.Invoke(); //invoke the game over event
            }
        }
    }

    /// <summary>
    /// Sets the current node to the first in the Game Managers node list.
    /// Sets the current movement direction to the direction of the current node.
    /// </summary>
    void InitializeAgent()
    {
        //refer to the game manager without having reference just saying game.managerinstance index 0
        currentNode = GameManager.Instance.Nodes[0];
        //Vector3.direction, working out direction of one vector to another
        currentDir = currentNode.transform.position - transform.position;
        //giving vectro with a magnitude of 1, takes all values and it becomes a float between 0 and 1
        //its important to normalize because it gives consistent 
        currentDir = currentDir.normalized;
        //finding two vectors for direction is dir = b - a
        //finding two vector for distance is distance = a - b 
    }

    //Implement DFS algorithm method here


    //Variable for 'Node currently being searched'
    private Node nodeBeingSearched;

    //Boolean for 'target found'
    private bool targetFound; 

    //list of type 'Node' storing 'unsearched Nodes' (this is your stack)
    private List<Node> nodes;

    //set 'target found' false
    void DFSsearch()
    {
        targetFound = false;

        while ()
        {

        }
    }

    //assign GameManager.Instance.Nodes[0] to your 'unseached Nodes' list
    
    //loop starts here
    //while target is false, continue the loop
    
    //1. take last in 'unsearched node' list and assign it to 'Node currently being searched'

    //2. check if 'Node currently being searched' is the same as *either*
        //the target Node of the Player (Node they are heading towards)
        //the current Node of the Player (the last Node they visited)
    //if this is true ('Node currently being searched' is the one we want):
        //assign 'Node currently being searched' as 'currentNode'
        //break the loop and finish this method
    //if it isn't true continue 

    //3. use a for loop to add each child of 'Node currently being searched' to 'unsearched Nodes' list

    //4. remove 'Node currently being searched' from 'unsearched Nodes' list







    //access the nodes on gamemanager
    //add GameManager.Instance.Nodes[0] to a list of unsearched nodes (root node)
    //check if root node is the same as GameManager.Instance.Player.Targetnode/Currentnode
    //if it is the same: return that as the new destination for this enemy
    //add the children of the node being searched to the list of unserached nodes
    //remove the node being searched from list of unserached nodes
    //assign the node at the 'top' (last postion of the unserached list) as the node being searched
    //go back to line 74
}
