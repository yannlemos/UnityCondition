using UnityEngine;

public class UnityConditionExample : MonoBehaviour
{
    public UnityCondition Condition;
    public bool EvaluateOnUpdate;
    public bool EvaluateOnStart;

    private void Start()
    {
        if (EvaluateOnStart) Condition.Evaluate();
    }
    
    public void Update()
    {
        if (EvaluateOnUpdate) Condition.Evaluate();
    }
}
