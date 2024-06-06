# UnityCondition
Odin-based Unity Engine component that allows the creation of conditional tests inside the inspector.

![image](https://github.com/yannlemos/UnityCondition/assets/16945950/0312ec6e-6561-4602-9e71-03c3435ccdf5)

While developing a roguelike in Unity, I realized that a lot of times it would be faster to expose game data to the game designers so that they could themselves create useful conditionals that are needed in the game's runtime (ex: only trigger an ability if health is below 50%, or only do this when moving, and so forth).

So I made this little system based on two plug and play scripts that allow you to create conditional tests in the Inspector inspired by the own engine's UnityEvent's ease of use. With this system, you can create conditionals that, when evaluated, trigger either a true of false event, allowing greater flexibility inside the inspector. This helped us immenselly in the game we were making and now that the game has been shipped, I decided to open-source it.

<h3>How To Install</h3>
1. Download all the scripts in the main directory and add them to your project's Scripts folder.
2. Create a GameObject called GameData in your main scene, and add the GameData component to it. 
3. That's it. 
