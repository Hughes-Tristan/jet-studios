
// Developer(s): Tristan Hughes
// Last Updated: 11-21-24
// Customer Manager Class

// the intended purpose of this class is to manage the customer line, spawning of customers and seating
// it also provides a method for spawning customers, seating customers, updating the line, managing the line, and checking if seats are available

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance { get; private set; }
    public Transform spawn;
    public Transform exit;

    [SerializeField] private GameObject customer;
    [SerializeField] private Transform[] seatPos;
    [SerializeField] private Transform linePos;
    [SerializeField] private int lineLength = 3;


    private Queue<GameObject> customerLine = new Queue<GameObject>();
    private List<Transform> seatsAvailable = new List<Transform>();

    // this function is used to check if an instance exists or not
    // then it determines whether to set the instance or destroy the duplicate
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    // this function is used to initialize available seats
    // it also uses a coroutinedefine how many customers should spawn, and spawn customers
    void Start()
    {
        seatsAvailable.AddRange(seatPos);
        StartCoroutine(manageLine());

    }

    // this function is used spawn the customers
    // it instantiates a game object
    // sets the game object to inactive then add a customer to the line
    // once it adds a customer to the line it checks if the customer can be assigned to a seat
    // and if the seat should be updated or not
    public void spawnCustomer()
    {
        GameObject spawnedCustomer;
        spawnedCustomer = Instantiate(customer);
        //spawnedCustomer.SetActive(false);
        Customers customers;
        customers = spawnedCustomer.GetComponent<Customers>();
        spawnedCustomer.transform.position = spawn.position;
        customers.exitpoint = exit;
        customerLine.Enqueue(spawnedCustomer);
        Debug.Log("Customer Spawned!");
        seatCustomer();
        updateLine();
    }

    // this function is used to try to assign a customer to a seat
    // if there is a seat available and a customer in line then it proceeds
    // it takes the next customer plus the first seat and assigns that seat to the customer
    public void seatCustomer()
    {
        bool canSeat = false;
        while(seatsAvailable.Count > 0 && customerLine.Count > 0)
        {
            GameObject customerNext;
            Customers customers;
            Transform seat;


            canSeat = true;
            customerNext = customerLine.Dequeue();
            seat = seatsAvailable[0];
            
            seatsAvailable.RemoveAt(0);
            customers = customerNext.GetComponent<Customers>();

            customers.assignSeat(seat);
            customers.changePos(seat.position);
            //customerNext.transform.position = seat.position;
            //customerNext.GetComponent<Customers>().assignSeat(seat);
            //customerNext.SetActive(true);
            
        }
        if (canSeat)
        {
            updateLine();
        }
        
    }

    // this function is used to set a seat to available and seat a new customer
    public void seatAvailable(Transform seat)
    {
        seatsAvailable.Add(seat);
        seatCustomer();
    }

    // this function is used to update the customer position in the line
    // it loops through each customer in the customerline and 
    // it then spaces the line out and moves the customers positions
    private void updateLine()
    {
        int i = 0;
        foreach (GameObject customer in customerLine) 
        {
            Vector3 customerPos;
            customerPos = linePos.position + new Vector3(i * 1.5f, 0, 0);
            Customers customers;
            customers = customer.GetComponent<Customers>();
            customers.changePos(customerPos);
            i++;
        }
    }


    // this function is a coroutine
    // it is used to check the length of the line
    // if the line length is more that the customer count
    // then spawn another customer
    private IEnumerator manageLine()
    {
        while (true)
        {
            float randomizedTime;
            float countLine;
            randomizedTime = Random.Range(1, 5);
            yield return new WaitForSeconds(randomizedTime);
            countLine = customerLine.Count;
            if (lineLength > countLine)
            {
                spawnCustomer();
            }
        }

    }
}
