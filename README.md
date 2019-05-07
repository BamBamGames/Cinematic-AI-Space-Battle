# Cinematic AI Space Battle 

![alt text](https://i.imgur.com/Ddug0aA.png)

# Proposal

I plan on implementing the following:

- Some sort of strategic AI systems for deploying fleets of ships (Leader making decisions, followers following instructions)
- All of the ships will use AI behaviours to move and interact with the environment in battle
- Ships will engage in :
    - Persuits
    - One on one battles (with some randomness to simulate pilot decisions)
    - Fleet on fleet battles
    - Obstacle avoidance, such as other ships and asteroids etc
- Battles should play out differently every time and by the end should have a clear winning team
- Cutscenes will be made for different game end states
- A dynamic camera system will be implemented that will find interesting scenes of battle on the battlefield and track and film these locations

- The job system will be used to implement as many behaviours as possible to keep performance high and as many ships in battle on the scen as possible
- Asteroids will be dynamic and may react to music in the scene?

Rought Scene and Story Outline :

---------------------------------------------------------------------------

![alt text](https://i.imgur.com/hunZKA8.png)

![alt text](https://i.imgur.com/rhYTDXu.png)

![alt text](https://i.imgur.com/W1ZFAeb.png)

![alt text](https://i.imgur.com/KZAqXkX.png)

![alt text](https://i.imgur.com/Ql6ksm3.png)

------------------------------------------------------------------------------------


# Results and Implementation

The final result is a 5 Scene project. With smooth cinematic transitions. Autonomous behaviour and them main part (The fully autnomous AI space battle). This project (mainly the space battle implementation will be used a skeleton for an RTS games). In this case it is a lite version of an RTS game i plan on working with complex AI systems.

# Instructions 

Disable all of the scenes in unity project. Open the scene called First and press play. This will cycle through the 4/5 different scenes. The scene main intro scene (this is the space battle (bad scene name :(..  )) can be ran independently. As with all of the scenes. 

# How it works 

The first 2 scenes are scripted and directed by me to look cinematic. All of the movement in the scenes is autonomous and usessteering behaviours. Co routines, timers , particle effects and audiosources are used to achieve the cinematic and story driven scenes. 

Scene 4/5 are ending scenes which are alternate endings which play depending on the the team that wins the AI battle. All movement in these scenes is also autonomous, including camerea, ships and ship parts. They work in a similar way to the first . 

These 4 scenes work as a story wrapper for the main scene (AI battle, scene 3). All movement is achieved using the seek, arrive and path following behaviours. This includes the camera and also the ships in the scene.

Scene director classes where implemented for each scene and an overall scene director class was made to manage and stay alive throughout the whole project.

------------
The main AI battle Scene 3: 

This main scene consists of 2 HQs which spawn fleets of enemies, fleets consist of a leader ship and following fighters. Every individual ship is autonomous and reactive to the environment and also the state of the battle. Leaders and Fighters work independently of eachother and make decisions. The AI for ships is implemented using Finite State Machines. The ships are implemented as an extension of the base class for ship. All ships share common states their state machines can use, and also ship specific based on ship type , fighter, leader , commander or medic. The ships implement many steering behaviours which are enabled and disabled based on the state of the ship. The behaviours are often truncated to create believable ship movement during combat and also evasive manuevers and asteroid dodging.

The Ships are paired into fleets, fleet pairs manage the state of battle between the two fleets. Fleet pairs are created when two opposing fleets initiate battle. FleetPairs also have state machines which are implemented to represent that state of battle the fleets are in. There are various branching states that fleets, fleet pairs and also the individual ships go into based on the outcome of various engagments. For example if a fleet leader dies, a fleet health is too low, a fleet looses to many fighters. Examples such as fleet escape/chase states/ dog fight states/ evasive states/ scramble fight states are all possible.

This is how fleets look like when they choose fleets to battle :

![alt text](https://github.com/EvgenyTimoshin/Cinematic-AI-Space-Battle/blob/master/Images/RP2fW52Fao.png)


There are various managers in the scene which are responsible for processing and mainting the state of battle. For example the battle field manager class is responsible for containing all of the data in the battle. eg the fleets, leaders, fighters, commanders . This battle manager is usefull for leaders to choose new fleets to attack, which fleets to join in case of abononment etc.

The main chunk of implementation for this project lies in the ship super class, extention classes. The fleet, fleet pair, battlemanager, and FSM states. Steering behaviours used are pretty standard but have been modified to achieve desired and appropriate effects for this project. For example obstacle avoidance was change to allow for more natural asteroid and ship avoidance. The wander behaviours where changed to create an illusion of evasive manuevers. Seek, Arrive, Offset Persue, and other behaviours where used on each ship. All behaviours were also truncated to allow for seamless flying. The main focus was on the AI and management of battle to produce realistic and desired outcomes.

Fleets in different states:
![alt text](https://github.com/EvgenyTimoshin/Cinematic-AI-Space-Battle/blob/master/Images/OpbZ0DqvnR.png)

The ships have a field of view and attack fighters in opposing fleets if they are enganged in battle with them. When fighters hit opposing ships they are awarded resource points, which work to increase the team HQ resource and allow more fleets to spawn. For the purpose of the project. The scene director class was used to time the battle and decide a winner after 3 minutes. 

Post processing stack, 3D sound and audio sources where also used. I did some voice acting and audio manipulation outside of unity to make the voices sound cool. Many particle effects were used to achieve flashy effects.

Cameras in the battle scene are attached to every single ship that is spawned. Fleet pairs also get a camera which faces the average vector point between two batlling fleets. These are picked at random. Ship camera chances are weighed higher and fleet battles are weighed lower. There are also cameras on the hq. In future works I would like to implement some sort of battle rating score which will track action in the battle and appoint cameras based on where the action is happening.

Some action scenes
![alt text](https://github.com/EvgenyTimoshin/Cinematic-AI-Space-Battle/blob/master/Images/Unity_AQWXm7D4Uh.png)

![alt text](https://github.com/EvgenyTimoshin/Cinematic-AI-Space-Battle/blob/master/Images/Unity_Dtcfd1JSBh.png)

![alt text](https://github.com/EvgenyTimoshin/Cinematic-AI-Space-Battle/blob/master/Images/Unity_W0mRjH21VF.png)


Asteroids are spawned in a defined are exposed to the editor.

# What I am most proud of in the assignment 

The overall behaviour and management of the AI space battle as partially described above. This was my first time implementing AI in game development. And I am now inspired to continue this project and make an AI battle RTS style game. During the developement of this project I proceeded to write skeleton code for desired future implementations. I have tested this battle for over 1 hour runtime and the ships continued to battle, hqs kept spawning and there where not many hiccups. Some adjustments still need to be made. Optimally I would of also liked to implement ECS or Job System but for the scale of the project it wasnt necessary, as I was focusing on the boids and AI FSMs rather than performance. They will be implemented also down the line.

The customizablility of the battle. I have exposed several settings to the unity editor so the battle settings could be changed, such as ship speeds, bullet speet, bullet damage, resource etc etc. This should be a really fun epxerience once I implement everything in the future.

The overall look and smoothness of the cinematic cutscenes is also cool. Looks flashy and fun. The music choice and overall composition i am also proud of. The attention to detail I have added in the cutscenes I am proud of also .

Overall really proud of the project. And cant wait to work on this further outside of college.


