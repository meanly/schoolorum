Toxic Townsmen - Complete Game
--------------------------------------

>>> About <<<

	Toxic Townsmen, is a 2D wave shooter set in medieval times with the player being a modern day soldier. 
	After a military experiment goes wrong, the player is sent back in time to the 1300's. The locals don't 
	like the stranger and with his weird weapons, the King sentences you to death. But you wont go down 
	without a fight. Fight through waves of angry medieval knights, archers and mages with your gun. But 
	your final challenge will be the hardest.



>>> Game Features <<<

	- Complete and ready to ship game (no audio though).
	- Wave based gameplay.
	- 3 enemy types.
	- Boss battle at the end.
	- Pixel art.



>>> Project Features <<<

	- Fully documented code.
	- Easy to navigate project folders.
	- Customisable waves.



>>> Tags & Sorting Layers <<<

	>> Tags <<

		- Enemy
			All 3 enemy types have this tag.

		- Projectile
			All projectiles have this tag.

		- Boss
			The boss (King) has this tag.

	>> Sorting Layers <<

		- BG
			Background tiles.

		- Enemy
			Enemy objects.

		- Projectile
			Projectile objects;

		- Player
			The player object.

		- UI
			All UI elements.



>>> Scripts <<<

	- Enemy.cs
		Each of the 3 enemies have this script. It manages enemy movement, shooting, damage taken and death.

	- Game.cs
		Manages wave spawning, camera shake, wave timers, upgrading and start countdown.

	- King.cs
		Manages the boss. Movement, attacking, upgrading, damage taken and death.

	- MenuUI.cs
		Manages all the menu UI elements. Play and Quit buttons. Scene changes and quitting the application.
		Also menu camera shake.

	- Projectile.cs
		Manages data for player and enemy projectiles. Dealing damage on collision enter. If it follows the player
		or not, as well as destroying itself after 3 seconds.

	- UI.cs
		Manages all in-game UI elements. Health bar, wave text, win screen and end screen. Reloading the scene and
		quitting the application.

	- Wave.cs
		Holds data for a wave. Enemies to spawn, spawn rates and if the wave spawns a boss.



>>> How to Create or Edit Waves <<<

	1. Go to Game.unity scene and select the "_GameManager" game object.
	2. In the inspector, look at the Game.cs script.
	3. Under the "Waves" array, each element is an individual wave.
	4. Add a wave by increasing the size of the "Waves" array.

	>> Each Element <<

		Each element in the "Waves" array has:

			- Enemies
				Array of the enemies that are going to spawn in the wave, in order, as EnemyType enum.

			- Spawn Rates
				Array of floats which correlate to the enemies array. Each element in that array is the
				time after the previous enemy, that this one will spawn.

			- Spawn Boss
				Yes or no. Will the boss spawn this round. If so, then it's basically the last round.



>>> Contact <<<

	If you ever wish to contact me about problems, suggestions or questions about this asset or any other one of mine,
	you can talk to me on...

		E-mail: buckleydaniel101@gmail.com