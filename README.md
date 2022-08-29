# Kraken
This project is an attempt to introduce my son to world of programming.

This is a video game he's conceived which I will try to implement 
with him beside me, hopefully giving him some stuff to do so he can
learn how these things are made.

# The Game
In the game, you play a Kraken with up to 8 tentacles and your goal is 
to smash increasingly more difficult pirate ships (starting with row boats
and increasing in difficulty to ships of the line with cannons that shoot 
back). You can hit things with your tentacles, or you can wrap your
tentacles around ships.

It will be written in C# and Godot


# GAMEPLAY

Goal of the game is to destroy ships. You do that by hitting them with your 
tentacles, or by dragging them underwater. Boats fight back by destroying 
your tentacles.

 * You get 4 buttons and direction (left-right)
 * Each button controls up to two tentacles
 * The idea is to press a button and a direction to throw a tentacle

As you destroy boats, you get their treasure.
As you destroy boats, the level of boats increases.
As you destroy boats, you charge up a special attack (maybe a tsunami?) which weakens boats.
As the boat level increases, their weapons become more devastating.

e.g. You start with one tentacle, fighting row boats, which do not fight back. If you get 
enough treasure, you get another tentacle and you fight a small boats with a deck, where
the people on deck have machetes. If the boat kills your tentacle, you drop down to the 
level below where there are only rowboats. The game doesn't end.
