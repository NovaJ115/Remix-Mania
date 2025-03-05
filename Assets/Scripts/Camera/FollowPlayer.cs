using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public GameObject jumpIndicator;
    public float xOffset;
    public float yOffset;

    private StatRandomizer statRandomizer;
    private float reverseOffset;
    private float normalOffset;
    public void Start()
    {
        normalOffset = yOffset;
        reverseOffset = -yOffset;

        player = GameObject.FindWithTag("Player");
        statRandomizer = FindFirstObjectByType<StatRandomizer>();
        
    }
    public void Awake()
    {
        
    }
    public void Update()
    {
        jumpIndicator.transform.position = new Vector2(player.transform.position.x + xOffset, player.transform.position.y + yOffset);
        this.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        if (PlayerPrefs.GetString("EnableJumpIndicator") == "True")
        {
            jumpIndicator.gameObject.SetActive(true);
        }
        else
        {
            jumpIndicator.gameObject.SetActive(false);
        }
        if (statRandomizer.isReverseGravity)
        {
            yOffset = reverseOffset;
        }
        else
        {
            yOffset = normalOffset;
        }
    }
}
