# HouseZero AR
### Building Performance and CFD Simulation Interface
Unity 2017.1.2f1 (64-bit)

A proof-of-concept of an interactive augmented reality building performance visualization interface, developed for the [Harvard Center for Green Buildings and Cities](http://harvardcgbc.org/) in summer 2017. Built in Unity for the Hololensl project is also synced to Unity Collaborate.


## Installation
Download the project from GitHub or Unity Collaborate, and select the scene `ZeroHouseMqtt.Update06`. Build for the Hololens (UWP device) and deploy from Visual Studio.

## How-To
The main visualization displays the proposed building for the Havard CGBC. There is also an associated panel, which controls the visualization.

### Control Panel
- **Air Flow** - toggles a simple animated particle system, showing air circulation through the house.
- **Vector Field** - toggles an animated CFD display, based on the latest model on the server.

**Uniform Vector** - toggles a static CFD display with normalized vectors.

**Color Gradient** - toggles a static CFD display with a shaded visualizaiton.

**CFD Section** - toggles an interactive section cut of the CFD display.

**House Section** - toggles a fixed section of the house.

**Auto Rotation On/Off** - toggles the rotation of the house.

### House
Target and drag the base of the house to rotate. Target and drag the top of the house to scale. House

## Features

### CFD

### MQTT

### Interactivity
