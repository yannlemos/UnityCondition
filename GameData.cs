using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Current { get; private set; }

    private void Awake()
    {
        if (Current != null && Current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Current = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public float GetNumericGameData(NumericGameData numericGameData)
    {
        switch (numericGameData)
        {
            // Insert access to data here
            default:
                return 0;
        }
    }

    public bool GetBooleanGameData(BooleanGameData booleanGameData)
    {
        switch (booleanGameData)
        {
            // Insert access to data here
            default:
                return false;
        }
    }

    public ProgressionState GetProgressionGameData(ProgressionGameData progressionGameData)
    {
        switch (progressionGameData)
        {
            // Insert access to data here
            default:
                return ProgressionState.None;
        }
    }
}

public enum BooleanGameData
{
    None
    // Create new data here
}

public enum ProgressionGameData
{
    None
    // Create new data here

}

public enum NumericGameData
{
    None
    // Create new data here

}

public enum ProgressionState
{
    None
    // Create new data here

}
