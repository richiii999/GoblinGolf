using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;

public class WarpAbility : MonoBehaviour
{
    public Transform player;
    public Transform hole;
    public float distAwayX = 2;
    public float distAwayZ = 2;
    public float distAwayY = 0.005f;
    public GameObject portal;
    public float spawnOffset = 0.5f;
    public float riseTime = 1.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 pos = player.position;

        //Vector3 holePos = hole.position;
        float holeX = hole.position.x;
        float holeY = hole.position.y;
        float holeZ = hole.position.z;
        Vector3 newPos = player.position;
        if (Input.GetKeyDown("w"))
        {
            //newPos = holePos;
            newPos.x = holeX + distAwayX;
            newPos.z = holeZ + distAwayZ;
            newPos.y = holeY + distAwayY;
            player.position = newPos;
            PlayWarpEffect();
            Debug.Log("W is pressed");
        }
    }

    public void warp()
    {
        Vector3 pos = player.position;
        float playerX = pos.x;
        Vector3 holePos = hole.position;
        float holeX = pos.x;
        if (Input.GetKeyDown("w"))
        {
            playerX = holeX - 10;
        }
    }

    private IEnumerator LowerFromHeadToFeet()
    {
        // Create particle at player's feet
        Vector3 feetPos = new Vector3(
            player.position.x,
            player.position.y + spawnOffset,
            player.position.z
        );

        Vector3 headPos = new Vector3(
            player.position.x,
            player.position.y + player.localScale.y * 1.8f,  // approx. head height
            player.position.z
        );
        GameObject fx = Instantiate(portal, feetPos, Quaternion.identity);
        float t = 0;


        fx.transform.position = feetPos;
        yield return null;
       

        // Optional: destroy the effect after done
        Destroy(fx, 1f);
    }
    public void PlayWarpEffect()
    {
        StartCoroutine(LowerFromHeadToFeet());
    }
}
