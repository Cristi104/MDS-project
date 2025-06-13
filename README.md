# Time Echo
![GameLogo](https://github.com/user-attachments/assets/93bed361-7ed2-495b-b7da-d007431d60b7)

## Description
Time Echo is a puzzle-platformer where players manipulate time by working alongside their past selves. Each failed attempt creates an "echo"—a ghostly replay of the player's last actions. By strategically planning movements and utilizing multiple echoes, players must solve increasingly complex puzzles to escape time loops and reach the exit.

## Core Mechanics
• Time Loop System – Every failed attempt spawns an AI-controlled version of the player's past self that repeats previous actions.

• Echo Interaction – Players can use their echoes to activate buttons to move doors or platforms.

• Challenging Puzzles – Levels require strategic planning, precise movement, and clever use of past actions.

• Minimalist Visuals & Atmospheric Soundtrack – A clean and immersive design that enhances the time-looping experience.

## Why It's Unique
Unlike traditional platformers, Time Echo forces players to think ahead, using their own past attempts as tools rather than obstacles.

### [Link Backlog](https://petrecristian2004.atlassian.net/jira/software/projects/MP/boards/2)

## UML Diagrams
### Use Case Diagram
![DiagramaUseCase](https://github.com/user-attachments/assets/79fb2ea8-70db-433c-8090-557b74e2db97)
### State Diagram
![DiagramaStari](https://github.com/user-attachments/assets/9c8b80a5-b69d-4ba1-b62e-98f11b8731e2)

## Coding Standards
This project follows consistent C# coding conventions to maintain clean and readable code. Classes and public methods use PascalCase, while private fields use camelCase with an underscore prefix (e.g., _privateField). Constants are written in UPPER_CASE. All public members include XML documentation with summary tags and remark tags where applicable.
We enforce good encapsulation by keeping fields private and exposing them via properties or serialized fields when needed.

## Key Design Patterns
### Singleton

EventManager acts as a central hub for game events. Only one instance exists, accessible everywhere via EventManager.Instance.
### Observer

The **observer** pattern, implemented through the IEventListener interface, creates a flexible communication layer between game objects. Key implementations include:

• Doors resetting their position when receiving a "reset" event

• Clones restarting their replay sequence on notification

• Buttons activating/deactivating objects without direct references

### Strategy

The Activateable interface (Activate()/Deactivate()) allows buttons to trigger different behaviors. Doors, traps, etc. implement their own logic while working with the same button system.
