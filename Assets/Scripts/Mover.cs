using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Rail rail = null;
    [SerializeField] private PlayMode playMode = PlayMode.Linear;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private bool isReversed = false;
    [SerializeField] private bool isLooping = false;
    [SerializeField] private bool isPingPong = false;

    private int currentSeg = 0;
    private float transition = 0f;
    private bool isCompleted = false;

    private void Update()
    {
        if (!rail) return;

        if (!isCompleted) Play(!isReversed);
    }

    private void Play(bool isForward = true)
    {
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        transition += isForward ? s : -s;
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
            if (currentSeg == rail.nodes.Length -1)
            {
                if (isLooping)
                {
                    if (isPingPong)
                    {
                        transition = 1;
                        currentSeg = rail.nodes.Length - 2;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = 0;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;
            if (currentSeg == -1)
            {
                if (isLooping)
                {
                    if (isPingPong)
                    {
                        transition = 0;
                        currentSeg = 0;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = rail.nodes.Length - 2;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }

        }

        transform.position = rail.PositionOnRail(currentSeg, transition, playMode);
        transform.rotation = rail.Orientation(currentSeg, transition);
    }
}
