# StackWars

## What is this?
This project is my student work whose purpose is studying and use of different programming patters. The application is a game that simulates battle between two armies.  

## How dows it work?  

The battle is a sequence of the three steps:  
1. All units except first rows of both armies are making it's turn, like range attack or some magic. The order of these turns is random  
2. Units of first rows attacks each other in random order  
In both steps dead units do nothing and are not affected by other units (but it can be changed in the future)s
3. Both armies are collecting deads  

The battle is presented in three forms, selected by a user: 1-vs-1, 3-vs-3, all-vs-all (by the number of units in the row).

The armies consists of different units which are generated by random. Each unit type has cost, so the armies are constructed by the principle of the same total cost.

Following types of units are presented:  
1. Light infantry is a simple clonable healable unit with melee attack. Can buff heavy infantry with horse, helmet, rapier or armor if it stays in range 1 and not buffed by the same buff  
2. Heavy infantry is a melee unit with stronger armor but weaker attack. Can be buffed by the light infantry  
3. Archer is a clonable healable ranged unit. As the other ranged units it can attack enemy in some range. If the archer in the first row, then it attacks with melee attack
4. Cleric is a ranged unit that can heal random healable friendly unit in some range
5. Cloner is a ranged unit that can clone friendly clonable unit in some range
6. GulyayGorod is unit that can not attack but it has large hp and armor

Also the game supports undo and redo moves

## What is under the hood?

The decision what to do and to do it is made in three global parts:  

Unit classes
---------------------------------------------------------------------------
Unit classes are derived of base class ```Unit``` that has virtual ```MaxHealth```, ```CurrentHealth```, ```Defense``` and ```Attack``` properties. The abilities of derived units are described by their interfaces such as ```IRangedUnit```, ```IHealableUnit```, etc.  

Clonable units has method ```Clone``` and implements pattern ```Prototype```.  
Buffs of the heavy infantry are presented by BuffedUnit which is implementation of the pattern ```Decarator```.  

Also there is ```IObservable``` that does not affect the game process but when its implementor(Cleric) dies, special message and beep appears on the console. Second service type is ```ProxyClonerUnit``` that are replace Cloners in the armies and provide access to replaced units but logs the message when the unit dies.

Game engine
---------------------------------------------------------------------------
Game engine controls action of the game. When new game is created, it initializes armies, sets up special types (```IObservable```, ```ProxyClonerUnit```). During the game it enumerates units in both armies, takes its abilities, melee attacks and controls actions with dead units.  
The game is presented in three forms of the fight, so the specification of each mode is described in ```IFightStrategy``` implementor which is stored in the game engine

Commands invoker
---------------------------------------------------------------------------
As stated above, the game supports undo and redo moves, so all action of the game are not invoked instantly, but are stored in special class derived from abstract ```Command``` class with two methods - ```Execute``` and ```Undo```.  
The game engine during its work produces commands and sends it to commands invoker to invoke them. When the command is invoked, its method ```Execute``` is called and the command is stored in the Undo stack.  
When user wants to undo last moves, each command of last turn moves from Undo stack to Redo stack and its method ```Undo``` is called.  
On redo action, commands move from Redo stack back to Undo stack. On the new turn, redo stack is cleared.