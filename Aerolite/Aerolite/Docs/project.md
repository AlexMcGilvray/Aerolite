# Project TODO and log


## Todo

* Status bar UI element. 
* Add tooltip support to AeUIElements
* Add touch support to AeButton
* When zooming out the camera, the translation should also be scaled so you zoom on the point your mouse is over rather than the worldspace origin

## Done

*2017 April 18*

* Add image button support to AeButton
* Added ability to remove entities from states
* Fancied up AeButton in general. Has better highlighting with optional border highlighting

*2017 April 17*

* Added AeButton for a GUI button. 
* General UI improvements

*2017 Feb 21*

* Started work on UI module
* Made a progress bar widget as first UI item

*2017 Feb 18*

* Added some stuff to get a bounding box from the camera.
* Exposed rectangle properties on AeAABB

*2017 Feb 12*

* Updated sprite and animator classes to propagate render color downwards

*2017 Jan 14*

* Separate Updatable/Renderable into interfaces so we don't have madness inheriting from AeEntity

*2017 Jan 11*

* Make the state manager act as a stack then move the debug stuff and the HUD into their own states layered on 
top of the game state.
* Update mouse input to be able to do screen-to-world

## Log

*2017 April 17*

Been working a lot on EtherStrike and decided to make a level editor. As such, the main point of focus right now is on getting the UI portion of the engine improved so I can have a level editor. 

