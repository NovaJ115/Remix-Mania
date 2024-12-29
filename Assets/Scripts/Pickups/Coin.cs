using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public CoinManager coinManager;
    public AudioSource pickupSound;
    public float rotationSpeed = 1f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public GameObject pickupEffect;
    
    Vector2 posOrigin = new Vector2();
    Vector2 tempPos = new Vector2();

    public void Start()
    {
        posOrigin = this.transform.position;
    }
    public void Update()
    {
        tempPos = posOrigin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
        this.transform.Rotate(0, rotationSpeed, 0f, Space.Self);
    }
    public void Awake()
    {
        if(SceneManager.GetActiveScene().name != "Win")
        {
            coinManager = FindFirstObjectByType<CoinManager>();
            pickupSound = GameObject.FindWithTag("Coin").GetComponent<AudioSource>();
            rotationSpeed = Random.Range(coinManager.minSpinSpeed, coinManager.maxSpinSpeed);
            pickupEffect = GameObject.Find("RecordPickupEffect");
        }
        if(SceneManager.GetActiveScene().name == "ArtTest")
        {
            Debug.Log(rotationSpeed);
        }
        
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == GameObject.FindWithTag("Body"))
        {
            this.GetComponent<CircleCollider2D>().isTrigger = false;
            pickupSound.Play();
            coinManager.coinAmount += 1;
            Instantiate(Resources.Load("RecordPickupEffect"), this.transform.position, Quaternion.identity);
            //Debug.Log("Added 1 record");
            Destroy(this.gameObject);
        }
        
    }
    
    
}
