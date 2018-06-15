# HouseZero AR
### Building Performance and CFD Simulation Interface
Unity 2017.3.1f1 (64-bit)

A proof-of-concept of an interactive augmented reality building performance visualization interface, developed for the new headquarters of the [Harvard Center for Green Buildings and Cities](http://harvardcgbc.org/), and demoed at open house events in April 2018. Built in Unity for the Hololens. Project is also synced to Unity Collaborate.


## Installation
Download the project from GitHub or Unity Collaborate, and select the scene `HZHololens2018`. Build for the Hololens (UWP device) and deploy from Visual Studio.

Note that the project utilizes a HTTP connection to receive CFD data, as well as a connection to a WebSocket server for broadcast sensor data.

## Usage
The main visualization features two scales: a full- or room-scale view designed to overlay information on to the actual HouseZero, and a miniature-scale view which displays the proposed building for the Havard CGBC.

The full- or room-scale view includes:
- prototyped sensor values (temperature and humidity)
- virtual operable windows and associated airflow
- behind-the-wall visualization of infrastructure
- an interactive section of the CFD data

With some basic interactivity and visuzalition overlays. There is also an associated control panel, to toggle on or off specific aspects of the visualization. At start, the building is placed onto the nearest detected horizontal plane.

### Control Panel
TBD

### House
TBD

## Contributing
All scripting completed in C#. As much as possible, comments have been provided in the code for legibility.

### CFD
The CFD models are generated by the `miniCFD` and `roomCFD` GameObjects from a CSV, accessed via an HTTP connection. The cooresponding URL can be changed via Inspector panel using the `Url` field within the `get_JSON` script component. Alternatively, the CSVs can be stored locally for offline operation in the `Local Url` field. The `VMin`, `VMax`, `YMin`, `YMax` fields have been preset to fit the data.

### WebSocket
TBD

### Interactivity
A proof-of-concept level of interactivity is accomplished by transforming standard sliders, and hiding their cooresponding mesh renderers. The project reuses GUI and interaction assets from the [Microsoft Mixed Reality Toolkit for Unity](https://github.com/Microsoft/MixedRealityToolkit-Unity)

## Credits
Main design and development team: [Spyridon Ampanavos](http://www.spyridonampanavos.com/), [Yuan Gao](http://www.yuan-gao.com), [Brian Ho](https://brian-ho.io), [Jiho Song](http://www.jihosong.com).

Project led by Stephen M Ervin, in partnership with the Harvard CGBC directed by Ali Malkwai.
