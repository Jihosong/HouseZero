# HouseZero AR
### Building Performance and CFD Simulation Interface
Unity 2017.1.0f3 (64-bit)

A proof-of-concept of an interactive augmented reality building performance visualization interface, developed for the [Harvard Center for Green Buildings and Cities](http://harvardcgbc.org/) in summer 2017. Built in Unity for the Hololens. Project is also synced to Unity Collaborate.


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
All scripting completed in C#. As much as possible, comments have been provided in the code for legibility.

### CFD
The CFD model is generated by the `CfdSimulation` GameObject from a CSV, downloaded via a REST API. The cooresponding URL can be changed via Inspector panel using the `Url` field within the `CFD_JSON` sctip component. The `VMin`, `VMax`, `YMin`, `YMax` fields have been preset for the data.

Individual CFD visualization styles can be adjusted in the cooresponding script components (i.e. `Vector Field` changes the default animated CFD display).

### MQTT
The MQTT connection allows for real-time broadcasting of external sensor data into the visualization. This implementation utilizes Adafruit's free [IO](https://io.adafruit.com) service, with data being streamed from an Arduino sensor assembly.

Connection 

### Interactivity
