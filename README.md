# IntroToUnity_GroupRepository

## What this is: 
This is an advancement of the project started in the seminar "Introduction to Unity" held by Maximilian Wächter, M. Sc. and Farbod Nosrat Nezami M. Sc. at the University of Osnabrueck.
A video portraying the functionalities of this game you can find here: https://www.youtube.com/watch?v=hJ-O2pprvnw


We decided to **improve this project** from multiple perspectives: 

We added a new **menu Scence**. 
This scene contains a background canvas on which the player can interact with 5 UI buttons and the title of our game "Immunizer". These buttons either direct into submenus or start a specific action.
There is the *"PLAY"* button which upon clicking first asks the player to insert a name which the player can but doesnt need to do. The player again hits *"PLAY"* which starts the actual game by transitioning to the "Prototype" scene. 
There is the *"CONTROLS"* button which opens instructions on how the player is controlled. 
There is the *"HIGHSCORE"* button which opens a highscore table containing rank, name and score of past players. 
To implement the highscore functionality we utilized the PlayerPrefs function. This class stores player data between the switching of scenes and thus was well suited for the highscore functionality. 
Moreover, there is the *"QUIT"* button which closes the game window.
Finally there is the *"CREDITS"* button which shows the developers of the game a expression of gratitude to the instructors of the course Intro to Unity.
It also has to be noted that each submenu also contains a *"BACK"* button, which redirects to the previous menu. 
Furthermore, after the player died (in the Prototype scene), there is a short message saying : "You've lost! You got infected with CoViD-19. Quarantine imediately! Return to menu in 3s." After these three seconds the player is redirected to the highscore submenu (if he/she reached more than 0 points) in the menu scene so that the players can inspect how they did in comparisson to previous players.

We adjusted the **player settings**: 

- Player has 5 lifes now (as opposed to 3)
- Player is a Spaceship now (as opposed to a drone)

We added and modified multiple **Powerups**: 

UV Light:
- UVLight collectible (blacklight) (new) 
- UVLight (old)
- UVLight function (Player.cs, PowerUpsCollectible.cs, Vaccine.cs) (old)

Add extra life (new): 
- Addlive collectible (first aid box)
-Addlive function (Player.cs, PowerUpsCollectible.cs, Vaccine.cs)

Stop Viruses in mid-air (new): 
- Freeze collectible (snowflake)
- does not effect the evil vaccine, since it is technically not a virus.
- Feeze function (Player.cs, PowerUpsCollectible.cs, Corona.cs)

Player is surrounded by shield (new):
- Shield collectible (shield)
- Shield function (Player.cs, PowerUpsCollectible.cs, Vaccine.cs)

Increase player speed temporarily (new): 
- SpeedUp Collectible (coffee Cup)
- SpeedUp function (Player.cs, PowerUpsCollectible.cs) 

Decrease player speed temporarily (new): 
- SlowDown Collectible (ice cubes)
- SlowDown function (Player.cs, PowerUpsCollectible.cs) 

Increase player size temporarily (new)
- ScaleUp Collectible (ice cream cone)
- ScaleUp function (Player.cs, PowerUpsCollectible.cs)

Random Powerup Crate (new)
- Random_Powerup Collectible (Sci-Fi crate) (old)
- select random Powerup function (Player.cs, PowerUpsCollectible.cs, Corona.cs, Vaccine.cs)


*Note for Random Powerup Crate:* We considered several functions as powerdowns, which make the game harder. These are: SlowDom functionality, ScaleUp functionality. The Random powerup crate is set up to "contain" a powerup 50 % of the time and a powerdown 50 % of the time. 


We also added and edited **viruses**: 

We kept the functionalities of the three basic types of viruses.
We only adjusted minor things like the size, speed, and "wiggle range" (in case of the B117 version).
We also added a fourth virus type called BIGCorona which is a variant of the 501V2 virus. It inherits all of the functionalities of its original prefab, with the addition that it is considerably bigger, moves slower and has more lifes. We set this virus up with 3 lifes, so the player has to hit the virus 3 times before it dies. Moreover, if the player is hit by the BIGcorona the player loses lifes equal to the amount of lifes the respective BIGCorona still has. As stated earlier, when this virus is affected by the freeze powerup it still shoots its evil vaccine. This is intended since it is technically not a virus and as such should not be affected by the "freeze" This is intended as a BigBoss challange. 
A major change concerning the viruses is that we created our own viruse prefab in Blender, since, to our taste, the planet prefab was not accurate enough given the set up of the game. 
This new prefab is used for all viruses, merely the material and in one case the size is different. 


Have fun playing! 

Cheers, 

Yannick Hardt and Lennard Smyrka


