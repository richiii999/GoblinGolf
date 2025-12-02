using UnityEngine;
using System.Collections;

public class ResizeBall : MonoBehaviour
{
    public Vector3 sizeUp = new Vector3(3f, 3f, 3f);
    public Vector3 sizeDown = new Vector3(1f, 1f, 1f);
    Rigidbody rb;
    public GameObject MagicCircle;
    public Transform player;
    private Vector3 originalSize;
    private float duration = 5f;
    private float durationEnd = 0;
    private bool active = false;
    public float riseTime = 1.2f;
    public float spawnOffset = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q")){
            Grow();
        }

        if (Input.GetKeyDown("e")){
            Shrink();
        }
        if (active && Time.time >= durationEnd)
        {
            returnToBase();
        }
    }

    public void Grow()
    {
        active = true;
        transform.localScale = sizeUp;
        rb.mass = 1.5f;
        MagicCircle.transform.localScale = new Vector3(2f, 2f, 2f);
        durationEnd = Time.time + duration;
        PlayGrowEffect();
    }

    public void Shrink()
    {
        active = true;
        transform.localScale = sizeDown;
        rb.mass = 0.2f;
        MagicCircle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        durationEnd = Time.time + duration;
        PlayShrinkEffect();
    }

    public void returnToBase()
    {   
        active = false;
        transform.localScale = originalSize;
        rb.mass = 1f;
        PlayGrowEffect();
    }
    private IEnumerator RiseFromFeetToHead()
    {
        // Create particle at player's feet
        Vector3 feetPos = new Vector3(
            player.position.x,
            player.position.y + spawnOffset,
            player.position.z
        );

        GameObject fx = Instantiate(MagicCircle, feetPos, Quaternion.identity);

        Vector3 headPos = new Vector3(
            player.position.x,
            player.position.y + player.localScale.y * 1.8f,  // approx. head height
            player.position.z
        );

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / riseTime;
            fx.transform.position = Vector3.Lerp(feetPos, headPos, t);
            yield return null;
        }

        // Optional: destroy the effect after done
        Destroy(fx, 1f);
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
        GameObject fx = Instantiate(MagicCircle, headPos, Quaternion.identity);
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / riseTime;
            fx.transform.position = Vector3.Lerp(headPos, feetPos, t);
            yield return null;
        }

        // Optional: destroy the effect after done
        Destroy(fx, 1f);
    }
    public void PlayGrowEffect()
    {
        StartCoroutine(RiseFromFeetToHead());
    }
    public void PlayShrinkEffect()
    {
        StartCoroutine(LowerFromHeadToFeet());
    }
}
