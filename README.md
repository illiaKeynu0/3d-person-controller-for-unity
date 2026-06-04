As name suggests yet another iteration of 3d person controller with z-target system, which you can see in many games including Dark Souls.
I have to point that this project was mainly created as a product of learning process, therefore I want to credit a project that was my helpful referance https://github.com/vanillaGreen/Z-Target-In-Unity/tree/main. The main difference is usage of Cinemachine, which I didn't want to implement as I wanted normal Camera from the beginning.  
Project features:  
CameraC - controller for camera which holds interaction with walls;  
PivotC - controller of camera's position/rotation;  
PlayerC - controller, which holds movement logic;  
PlayerInput - project uses new Input System and script holds inputs from that;  
TargetC - targetable object conroller;  
TargetFinder - main system for finding active targets and cycling through them;  
TargetMarker - logic behind target marker positioning/rotation.  
