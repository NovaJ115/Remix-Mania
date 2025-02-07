using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public GameObject jumpIndicator;
    public float xOffset;
    public float yOffset;
    

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }
    public void Update()
    {
        this.transform.position = new Vector2(player.transform.position.x + xOffset, player.transform.position.y + yOffset);
        if (PlayerPrefs.GetString("EnableJumpIndicator") == "True")
        {
            jumpIndicator.gameObject.SetActive(true);
        }
        else
        {
            jumpIndicator.gameObject.SetActive(false);
        }
    }
}
