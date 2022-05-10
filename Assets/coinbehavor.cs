using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinbehavor : MonoBehaviour
{
    public int rotationSpeed = 30;
    private GameObject player;
    private coinSpwaner coinSpwanerScript;
    private score scoreScript;
     [SerializeField] [Range(0,1)] float speed = 1f;
     [SerializeField] [Range(0,100)] float range = 1f;   
    public int yStart=1;
    public int coinLifeSpanInSeconds=60;
    private float timer = 0.0f;
    public int CoinsCollected=0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player") ;
        coinSpwanerScript = player.GetComponent<coinSpwaner>();
        scoreScript= player.GetComponent<score>();
    }


    public void OnTriggerEnter(Collider Col)
    {
        //Debug.Log(Col.gameObject.tag);
        if (Col.gameObject.tag == "Player")
        {
            //Debug.Log("Coin collected!");
            scoreScript.coinsCollected++;
            coinSpwanerScript.coinCount--;
            Destroy(gameObject);
            
        }
    }
void Update () {

    // Rotation on y axis
    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    //going up and down
    float yPos = Mathf.PingPong(Time.time * speed, 1) * range +yStart;
    transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

    //calcute life span
    timer += Time.deltaTime;
    int seconds =(int)( timer % 60);
    if(seconds>coinLifeSpanInSeconds){ 
        coinSpwanerScript.coinCount--;
        Destroy(gameObject);
    }
}
}
