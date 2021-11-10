using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float timeScale;
    private float m_FixedDeltaTime;

    void Awake()
    {
        this.m_FixedDeltaTime = Time.fixedDeltaTime;
        this.timeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        timeScale = Time.timeScale;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Time.timeScale >= 1f && Time.timeScale < 52f)
            {
                Time.timeScale += 3f;
            }
            else if (Time.timeScale >= 0.1 && Time.timeScale <= 1.1f)
            {
                Time.timeScale += 0.2f;
            }
            // The fixed delta time will now be 0.02 real-time seconds per frame
            Time.fixedDeltaTime = this.m_FixedDeltaTime * Time.timeScale;

        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Time.timeScale >= 4f)
            {
                Time.timeScale -= 3f;
            }
            else if(Time.timeScale >= 0.3 && Time.timeScale <= 1.1f)
            {
                Time.timeScale -= 0.2f;
            }
            // The fixed delta time will now be 0.02 real-time seconds per frame
            Time.fixedDeltaTime = this.m_FixedDeltaTime * Time.timeScale;
        }

    }
}
