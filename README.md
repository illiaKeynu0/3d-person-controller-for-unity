As name suggests yet another iteration of z-target system presented in many games till this day.
I have to point that this project was mainly created as a product of learning process, therefore I want to credit a project that was my helpful referance https://github.com/vanillaGreen/Z-Target-In-Unity/tree/main. The main difference is usage of Cinemachine, which I didn't want to implement as I wanted normal Camera from the beginning.
Project features:
CameraC - controller for camera which holds interaction with walls;
PivotC - controller of camera's rotations in different states;
PlayerC - physical controller, which holds moving logic;
PlayerInput - project uses new Input System, therefore you'd need to add as well. Script holds inputs;
TargetC - enemies conroller;
TargetFinder - main system for finding active targets and cycling through them.
