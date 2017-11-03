# HouseZero AR
### Building Performance and CFD Simulation Interface
Unity 2017.1.0f3 (64-bit)

A proof-of-concept of an interactive augmented reality building performance visualization interface, developed for the [Harvard Center for Green Buildings and Cities](http://harvardcgbc.org/) in summer 2017. Built in Unity for the Hololens project is also synced to Unity Collaborate.


## Installation
Download the project from GitHub or Unity Collaborate, and select the scene `ZeroHouseMqtt.Update06`. Build for the Hololens (UWP device) and deploy from Visual Studio.

Note that the project assumes a connection to a REST API server with appropriate CFD data, as well as an [MQTT](http://mqtt.org/) broker service for sensor data.

## Usage
The main visualization displays the proposed building for the Havard CGBC, with some basic interactivity and visuzalition overlays. There is also an associated control panel, to toggle on or off specific aspects of the visualization.

### Control Panel
- **Console** - Displays the status of connection to the REST API with CFD data, and MQTT server with sensor data.
- **Air Flow** - toggles a simple animated particle system, showing air circulation through the house.
- **Vector Field** - toggles an animated CFD display, based on the latest model on the server.
- **Uniform Vector** - toggles a static CFD display with normalized vectors.
- **Color Gradient** - toggles a static CFD display with a shaded visualizaiton.
- **CFD Section** - toggles an interactive section cut of the CFD display.
- **House Section** - toggles a fixed section of the house.
- **Auto Rotation On/Off** - toggles the rotation of the house.
- **Move panel** - target and drag the "Tap to Move" panel.

### House
- **Rotate** - target and drag the base of the house to rotate.
- **Scale** - target and drag the top of the house to scale.
- **Sensors** - target and click a sensor (represented as a red sphere) to display real-time temperature and humidity data.
- **CFD section** - target and drag the CFD section to move it through the building.
- **Move house** - target and drag the "Tap to Move" panel, with spatial mapping.

## Contributing

### CFD

### MQTT

### Interactivity
