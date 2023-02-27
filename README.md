# Forest Drawing

This repository contains variations of a bouncing ball system to illustrate the AME principles,
 as well as the Observer, Decorator and a Factory Patterns:

* Variation 1 - Flyweights.
  * Software is organized into two layers: an application layer (AppLayer) and a GUI layer (Forests).
  * The AppLayer include a Drawing and an application of the Flyweight pattern (with decorator for adding extrinsic state to flyweights and aggregate for encapsulating intrinsic state in flyweights).
  * This version of the Forest Drawing programs implements the following features:
	* Creating a new diagram
	* Adding trees of a selected kind to the diagram
	* Selecting a tree
	* Deleting a selected tree
	* Saving to a file
	* Load a file into a new diagram
  * The program only supports only one active diagram at a time.
  * Note: The MainForm is heavily coupled to the drawing components.
  
* Variation 2 - Simple Command Pattern with No Invoker
  * Applies the command pattern to decouple the GUI from the drawing components.  This application of the command does not include an Invoker.
  * Refactors control logic out of the GUI and puts it in a Command layer that is more easily tested.  Also, this makes the GUI’s logic simpler.
  * Converts the TreeFactory to a singleton
  * Makes other minor improvement
  * Note: The GUI is still coupled in time to the execution of drawing component behaviors.
  
* Variation 3 - Command Pattern with Invoker
  * Extends the application of the command pattern to include an active-object Invoker.  The active-object Invoker further decouples the GUI from the Drawing Components by making the execution of GUI behaviors independent of the execution of Drawing component behaviors.
  * Makes other minor improvements

* Variation 4 - Undo and Redo
  * Adds "undo" and "redo" features by extending the application of the Command pattern with an application of the Undo Pattern.

* Variation 5 - More Drawing Tools and Adaptor
  * Adds an adapt for working with Graphics objects
  * Adds features for drawing lines and text boxes
  * Illustrates how to draw a rubber-band line when drawing lines and text boxes

* Variation 6 - Saving to AWS

