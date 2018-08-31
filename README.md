Learning resource for Stadin Ammattiopisto Game Programming course

Angry balls student assignment after lesson 2 (30.8.)

Add one of these features:
* Particles when a baddie collides with something
  - Hint: check Unity API documentation for Collision2D on how to find point of collision
  - Bonus: make particle effect bigger the harder the collision
* UI showing remaining balls and baddies
  - Hint: GameManager knows when these numbers change
  - Bonus: show win/lose message when game is over
* Change ball sprite when launched from slingshot
  - Assets/Sprites folder has two alternate sprites to use
  - Hint: fastest way is to replace the sprite value on SpriteRenderer, for more complex animations you can use Animator instead
  - Bonus: add more animations (i.e.  changing color, changing size, etc.)
* Aiming indicator curved line (hard!)
  - Hint: Use LineRenderer component
  - Calculate ball rigidbody velocity at 6-10 points in time using current aim vector and gravity (Physics2D.gravity)
    https://en.wikipedia.org/wiki/Projectile_motion
* Explosive crates (hard!)
  - Hint: you need to keep track of all rigidbodies in the scene
  - Apply force (explosion) to all nearby rigidbody2ds when a crate is destroyed
  
  
NOTE: You can download the whole working project by pressing the green "Clone or download" button
