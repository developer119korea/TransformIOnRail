using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Rail rail = null;

    private int currentSeg = 0;
    private float transition = 0f;
    private bool isCompleted = false;

    private void Update()
    {
        if (!rail) return;

        if (!isCompleted) Play();
    }

    private void Play()
    {
        transition += Time.deltaTime * 1 / 2.5f;
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;
        }

        transform.position = rail.CatmullPosition(currentSeg, transition);
        transform.rotation = rail.Orientation(currentSeg, transition);
    }
}
