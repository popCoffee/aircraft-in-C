# aircraft-in-C
Read me!

# VR-Flight-physics-Unity
Incorporating realistic XPlane flight physics into unity for VR applications and training of landing planes on carrier decks. This application was controlled with an xbox controller.  
Author: Andrew Kubal
 © 2018

### Notes
This is a VR application for unity. No meta files were included. Copy and paste the code into unity to get it to work. 
This works with Flywithlua, an addon to xplane11 that helps interfacing with external programs. 
 Check Xplane settings to ensure the ports are set up.
#### .cs files
Communication with xplane11 occurs via UDP data structure. That is, the raw data string contains 41 bytes per sentence and the last 36 bytes are the message. Xplane works by loading a scene with an aircraft and afterwards sends messages. 
* controlship--
Enables xplanes to control the ship in unity.
* testcontrol--
Sends xbox controller commands to unity and xplane to control the plane.
* collidercarrierxplane--
Returns a true if the plane contacts the carrier deck. This creates the effect of landing the plane by disengaging the user's controls.
* dataxplane--
Displays xplane aircraft position and rotation onto unity aircraft object.
* decoding--
Translates xplane11 raw data string to intepret postion rotation aircraft_id velocity weather and carrier info.
* portconnect--
Initializes communication.
* senddata--
Sends data (unnecesary file, dont need it).
#### .lua files
* position--
Controls the aspects of aircraft needed for landing: brakes gear flaps wind_direction.
* reloadairplanev--
Reloads the scene and aircraft in xplane (unnecessary file, dont need it).
* trim_fighter--
Provides nose authority and creates a burble. This makes flying the aircraft more realistic.
