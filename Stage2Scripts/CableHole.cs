using UnityEngine;

public class CableHole : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;

    public bool IsOccupied => isOccupied;

    public bool TryOccupy()
    {
        if (isOccupied)
        {
            return false;
        }

        isOccupied = true;
        return true;
    }

    public void ReleaseHole()
    {
        isOccupied = false;
    }
}