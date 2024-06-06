using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[BoxGroup]
[Serializable]
public class UnityCondition
{
    [SerializeField]
    [HideLabel]
    [TabGroup("Root", "Parameters", SdfIconType.GearFill, TextColor = "Orange")]
    [EnumToggleButtons]
    private BooleanOperation _operation;

    [Space(5)] [SerializeField] [TabGroup("Root", "Parameters", SdfIconType.GearFill, TextColor = "Orange")]
    private List<Statement> _statements = new();

    [TabGroup("Root", "Events", SdfIconType.ArrowUpShort, TextColor = "Yellow")]
    public UnityEvent OnTrue;

    [TabGroup("Root", "Events", SdfIconType.ArrowUpShort, TextColor = "Yellow")]
    public UnityEvent OnFalse;

    public bool Evaluate()
    {
        var result = _operation switch
        {
            BooleanOperation.AND => _statements.All(t => t.Evaluate()),
            BooleanOperation.OR => _statements.Any(t => t.Evaluate()),
            _ => false
        };

        if (result is true) OnTrue?.Invoke();
        if (result is false) OnFalse?.Invoke();

        return result;
    }
}

[BoxGroup]
[Serializable]
public class Statement
{
    #region Inspector
    
    public enum Type
    {
        Boolean,
        Numeric,
        Progression
    }
    
    [SerializeField] private Type _type;

    #region Boolean

    [HorizontalGroup("Main", Width = 0.47f)]
    [HideLabel]
    [ShowIf("@_type == Type.Boolean")]
    [SerializeField]
    [SuffixLabel("is")]
    private BooleanGameData _booleanValueA;

    [HorizontalGroup("Main", Width = 0.47f)]
    [ShowIf("@_type == Type.Boolean && _useGameDataAsValueB")]
    [SerializeField]
    [HideLabel]
    private BooleanGameData _booleanGameDataValueB;

    [HorizontalGroup("Main", Width = 0.47f)]
    [ShowIf("@_type == Type.Boolean && !_useGameDataAsValueB")]
    [SerializeField]
    [HideLabel]
    private bool _booleanLocalValueB;

    #endregion

    #region Numeric

    [HorizontalGroup("Main", Width = 0.422f)]
    [HideLabel]
    [ShowIf("@_type == Type.Numeric")]
    [SerializeField]
    private NumericGameData _numericValueA;

    [SerializeField]
    [HorizontalGroup("Main", Width = 0.09f)]
    [HideLabel]
    [ShowIf("@_type == Type.Numeric")]
    private NumericOperation _numericOperation;

    [HorizontalGroup("Main", Width = 0.422f)]
    [ShowIf("@_type == Type.Numeric && _useGameDataAsValueB")]
    [SerializeField]
    [HideLabel]
    private NumericGameData _numericGameDataValueB;

    [HorizontalGroup("Main", Width = 0.422f)]
    [ShowIf("@_type == Type.Numeric && !_useGameDataAsValueB")]
    [SerializeField]
    [HideLabel]
    private float _numericLocalValueB;

    #endregion

    #region Progression

    [HorizontalGroup("Main")]
    [HideLabel]
    [ShowIf("@_type == Type.Progression")]
    [SerializeField]
    [SuffixLabel("is")]
    private ProgressionGameData _progressionValueA;

    [HorizontalGroup("Main")] [ShowIf("@_type == Type.Progression")] [SerializeField] [HideLabel]
    private ProgressionState _progressionValueB;

    #endregion
    
    [HorizontalGroup("Main", Width = 0.045f)]
    [Button(Icon = SdfIconType.ArrowLeftRight, IconAlignment = IconAlignment.LeftOfText)]
    [HideLabel]
    [Tooltip("Toggle to game Data or to local value")]
    [ShowIf("@_type != Type.Progression")]
    private void ToggleBooleanValue()
    {
        _useGameDataAsValueB = !_useGameDataAsValueB;
    }

    private bool _useGameDataAsValueB;
    
    #endregion
    
    #region API
    
    public bool Evaluate()
    {
        return _type switch
        {
            Type.Boolean => EvaluateBoolean(),
            Type.Numeric => EvaluateNumeric(),
            Type.Progression => EvaluateProgression(),
            _ => false
        };
    }

    private bool EvaluateProgression()
    {
        return GameData.Current.GetProgressionGameData(_progressionValueA) == _progressionValueB;
    }

    private bool EvaluateNumeric()
    {
        var valueA = GameData.Current.GetNumericGameData(_numericValueA);
        var valueB = _useGameDataAsValueB
            ? GameData.Current.GetNumericGameData(_numericGameDataValueB)
            : _numericLocalValueB;

        return _numericOperation switch
        {
            NumericOperation.Equals => Math.Abs(valueA - valueB) < 0.1f,
            NumericOperation.BiggerThan => valueA > valueB,
            NumericOperation.SmallerThan => valueA < valueB,
            NumericOperation.BiggerThanOrEqual => valueA >= valueB,
            NumericOperation.SmallerThanOrEqual => valueA <= valueB,
            _ => false
        };
    }

    private bool EvaluateBoolean()
    {
        var valueA = GameData.Current.GetBooleanGameData(_booleanValueA);
        var valueB = _useGameDataAsValueB
            ? GameData.Current.GetBooleanGameData(_booleanGameDataValueB)
            : _booleanLocalValueB;

        return valueA == valueB;
    }
    
    #endregion
}

public enum NumericOperation
{
    [InspectorName("==")] Equals,

    [InspectorName(">")] BiggerThan,

    [InspectorName(">=")] BiggerThanOrEqual,

    [InspectorName("<")] SmallerThan,

    [InspectorName("<=")] SmallerThanOrEqual
}

public enum BooleanOperation
{
    [InspectorName("All Statements Must Be True")]
    AND,

    [InspectorName("Only One Statement Must Be True")]
    OR
}