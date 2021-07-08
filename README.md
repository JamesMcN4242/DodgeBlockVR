# DodgeBlockVR
An evening project aimed at re-familiarising myself with the basics of Unity's XR package and also the SteamVR plugin when developing for virtual reality applications.
The plan is currently aimed at making a simplistic rythm-like game based on dodging blocks flying towards you. 
The project is being kept small in scope with a self imposed week long deadline being set. (04/07/2021 - 09/07/2021) This is primarily aimed to be kept so condensed to allow me to move onto some other planned personal projects that will tie back into this repo at a later date. 

## Development Log

### Day One (04/07/2021)
Day one of the project has been a simplistic setup to the premiss of the game, alongside the import of both the SteamVR plugin and Unity's own XR package.
Unity's own XR package was created in the time since I last used the SteamVR plugin, so I'm currently unsure if both are required for new projects. Some reading is required tomorrow to find out and strip out any unnecessary files.

The three hours of development today has seen blocks fly at the user and from scene defineable placements. Varying speeds can be applied to the blocks and collisions between them and the player are tracked and processed. Debug logs are the only messaging used at current to determine a player's score (debug set) and inform player's when they've lost.

### Day Two (05/07/2021)
Day two has been limited in the time available to tinker with the project. The main addition today has been the tracking off two VR controllers. Due to unfamiliarity with the new InputSystem this is currently completed through the older Tracked Pose Driver system. 
As the current controller input (trigger only for now) displays I have opted to move towards using Unity's new input system. Although time was limited today I hopeful I can get the InputSystem functioning on a per controller basis tomorrow.

### Day Three (06/07/2021)
The three hours today were busy but fun. I gained a good grasp of Unity's new InputSystem and used it in relation to the VR controllers. The ability to punch and destroy blocks was added to the game, making it less "DodgeBlockVR" and more "DamageBlockVR". Some work needs to be completed here to ensure users are putting in enough force to break blocks as for now they are fully collision oriented. This is a thing I'll come back to nearer the end of the week if I have time.
A pause state was added to the game. Since I've yet to implement UI into the game at all this state appears much more like a super power than an actual pause state. The video below demonstrates this:

https://user-images.githubusercontent.com/25529411/124666774-70014180-dea6-11eb-9696-1d0f6759cf9f.mov

Overall I'm happy with the results today has brought, as there now some fun to actually playing the game - even if this wasn't the initial plan for the prototype. Adding some haptic feedback (rumble) for sucessful punches has also made the game feel a bit nicer. Tomorrow will have a very constrained amount of time, so I think I'll focus it on getting a score system in place and ready to hook up to some UI which Thursday will aim to complete.

### Day Four (07/07/2021)
As mentioned the day before, only a short amount of time was able to be dedicated to the project today. I am content with the score system I got in under the time constraints as it was purely smooth sailings. In an ideal world I'd like to try make the score interaction types more hands off in the future, but for this project I don't see it being an issue at all. Tomorrow I'm excited to finally get into the UI and UX hook ups for some of the screens. Having played many VR games I'm excited to try take over my favourite types of UX into this prototype.

### Day Five (08/07/2021)
Simplistic (non-pretty) UI screens have been added to the game. These display the ways to navigate any menus, as well as the player's final score. When thinking about the UI I have liked in games I've played, I realised the two that stuck out as positive to me most were the ones which were entirely stationary, or the ones that slowly lerped back into view. I decided to follow the approach where they lerp back into the player's vision as it seemed more interesting to implement. While playing about with the inclusion of these little UI screens I realised how fortunate we all are that the basic free-to-play mobile model has not made it's way to VR (yet). Having a game freeze between interesting segments to display ads that follow your every head tilt would be a truly horrible future... But I made it for a joke tweet anyway, so might as well expose any readers to this grim Black Mirror like concept:

https://user-images.githubusercontent.com/25529411/124990192-f1d4a480-e037-11eb-8fa2-64d34fcc273d.mov

Tomorrow is the last day to work on this project before my self inflicted deadline. As a result I'm weighing up what I could realistically get done in the final 2-3 hours that would add the best experience to this very basic game. I may attempt to flesh out the pause menu to allow the user to more dynamically change the difficulty of the game to their own liking. That or I may try to make the game actually look good, as opposed to it's current default Unity UI and whiteboxed state. Seems like a decision for tomorrow.
