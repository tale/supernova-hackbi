# Supernova
HackBI 2021 Game

## Inspiration
One of the first thoughts that came to mind after the theme of the hackathon was announced, was to make a game. 2 of us already had some experience in game development and game libraries like MonoGame, so we thought we could try something new and make an infinite runner style game.

## What it does
Essentially, the game is like any other infinite style runner game where you are supposed to reach higher scores. Some of the obstacles include asteroids and the giant supernova that follows you to make sure you don't stop moving. You also have fuel and health, which depletes over time and goes down when you hit an asteroid respectively. To refuel you must be within planets and your health will also slowly regenerate. If you lose all your health the game is over and your score is presented to you.

## How we built it
The game is built on the MonoGame framework and consists of many different parts. We had to setup a Camera, a Player, a World, and all of the obstacle objects. Gravity had to be implemented along with acceleration for the movement of the player. The game uses the Perlin Noise algorithm to calculate the randomness of obstacles and planets.

## Challenges we ran into
Probably the hardest thing we ran into was the implementation of gravity and collision detection. Because of the way the initial game was setup, it was very hard to access the planets and asteroids globally and made the already annoying math calculations harder to implement.

## Accomplishments that we're proud of
One thing we are proud of is the amount of work we got done in this time frame. In the beginning it felt like we had taken on a project that was too big for the amount of time we had, but at the beginning of the second day that feeling went away as things began to come together.

## What we learned
We learned many things about the MonoGame framework and how to structure code. On a team level, we learned that communication is very important because there have been times when things were just missed because no one brought it up. Additionally, we probably should've planned the game out.

## What's next for Supernova
After this, the next steps would be a huge code cleanup and fixing up the hacky logic that was used. We would then make higher quality sprites and work on lighting + shaders.
