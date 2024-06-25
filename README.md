# UnityCondition

> [!WARNING]
> Requires [Odin Inspector](https://odininspector.com/)

Odin-based Unity Engine components that allows the creation of conditional tests inside the inspector.

![UnityCondition](https://github.com/yannlemos/UnityCondition/assets/16945950/0312ec6e-6561-4602-9e71-03c3435ccdf5)

While developing a roguelike in Unity, I realized that it would be faster to expose game data to the game designers so that they could themselves create useful conditionals needed in the game's runtime (e.g., only trigger an ability if health is below 50%, or only do this when moving, and so forth).

So I made this system based on two plug-and-play scripts that allow you to create conditional tests in the Inspector inspired by UnityEvent's ease of use. With this system, you can create conditionals that, when evaluated, trigger either a true or false event, allowing greater flexibility inside the inspector. This helped us immensely in the game we were making and now that the game has been shipped, I decided to open-source it.

## How To Install
1. Download all the scripts in the main directory and add them to your project's `Scripts` folder.
2. Create a GameObject called `GameData` in your main scene, and add the `GameData` component to it.
3. That's it.

## How It Works
A UnityCondition is a collection of statements constructed with game data and/or hard-coded values that, when evaluated, trigger either an `OnTrue` or an `OnFalse` UnityEvent. Game data means information pertinent to your game. You work with game data inside the `GameData.cs`, which is a singleton script responsible for being the place to ask for data in your game. It does this by using Enums, which are:

- **NumericGameData**: Float values important to your game, like player health, player speed, current time of play session, and so on.
- **BooleanGameData**: Boolean values important to your game, like is the player alive, is it daytime, has the boss spawned, and so on.
- **ProgressionGameData**: Progression information important to your game, like has the player unlocked a certain ability, is the final stage still locked, and so on. This returns a specific `ProgressionState` enum that you can tailor to your game. For example, in our case, we had `Hidden`, `Locked`, `Available`, and `MaxedOut` because it made sense to our game's progression.

When a UnityCondition is evaluated, it compares the values in the statements, and if the value in the statement is GameData, it retrieves the data from the `GameData` singleton. So for example, in the screenshot above, it is retrieving the info if the player is moving through `GameData.cs`. How does it retrieve it? That's for you to decide what makes sense for your game's architecture.

To make the system work for your game, all you have to do is fill the Enums in `GameData.cs` with your own important game data and then create the accessors inside the getter functions.

Here's an example below of how you would declare a UnityCondition in a script:

```csharp
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
```
