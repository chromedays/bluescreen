using UnityEngine;

class Timer
{
    private float _t = 0f;

    public void Update(float dt)
    {
        _t += dt;
    }

    public void Reset()
    {
        _t = 0f;
    }

    public bool IsElapsed(float testElapsedTime)
    {
        if (_t > testElapsedTime)
            return true;

        return false;
    }
}

public class Stage : MonoBehaviour
{
    public int State = 0;
    public float UnbrokenDesktopTime = 30f;
    public float BlueScreenTime = 10f;

    private Timer _timer = new Timer();

    void Start()
    {
    }

    void Update()
    {
        _timer.Update(Time.deltaTime);

        switch (State)
        {
            case 0:
                if (_timer.IsElapsed(UnbrokenDesktopTime))
                {
                    _timer.Reset();
                    ++State;
                }
                break;
            case 1:
                if (_timer.IsElapsed(BlueScreenTime))
                {
                    _timer.Reset();
                    ++State;
                }
                break;
            case 2:
                break;
        }
    }
}
