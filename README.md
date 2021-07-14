# AR App I Like Digital

This application is client version and the purpose of this application is to provide the experience to user holding phone/ tablet that they are in AR show which is being controlled by external moderator.

So according to description this has to side one is receiver which will be unity application running on iPhone and other is Touch OSC which will work as a layout for the moderator to start specific sequence or set of sequences.

The main behind this prototype is to recreate the experiment Joseph Beuys & the coyote (I like America and America likes me, 1974)([Video]( https://www.youtube.com/watch?v=TIU0Sx6ijhE&t=77s&ab_channel=PublicDelivery)).

This prototype has following features:

- Positioning of AR Model
- Configuration of stage/ area
- Local/Server OSC messages communication
- External control of application *(Touch OSC Layout)
- Narrator Speech App via App *(VIVOX)
- 3D Spatial Audio

This application has two modes AI behavior such as attack and external control sequences through OSC layout.


## Plugins

This set of samples relies on five Unity packages:

* ARSubsystems ([documentation](https://docs.unity3d.com/Packages/com.unity.xr.arsubsystems@4.1/manual/index.html))
* ARCore XR Plugin ([documentation](https://docs.unity3d.com/Packages/com.unity.xr.arcore@4.1/manual/index.html))
* ARKit XR Plugin ([documentation](https://docs.unity3d.com/Packages/com.unity.xr.arkit@4.1/manual/index.html))
* ARFoundation ([documentation](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.1/manual/index.html))
* VIVOX
([documentation]( https://docs.vivox.com/v5/general/unity/5_13_0/en-us/Default.htm ))

## Technology Used

- Unity 2020.1.6f1
- Target Platform iOS min 12
- ARKIT
- ARCORE
- AR Subfoundation

## Architecture

### Nodes:
1. Unity Application
2. Touch OSC layout
3. Server (PC) Running with OSC data monitor

So in order to have network there is a need of local wifi network and all devices should be connected to same network. This setup has two versions Local and Host

### Local Host

- Local Host IP (127.0.0.1) Approach - For testing without PC
- No need of any PC running with OSC monitor.
- Just need 2 devices one with AR application and one with Tuch OSC layout
- Both should be connected to the same router.
- For configuration follow steps 1 till 4

### Server Host 

- Host IP (192.168.178.34)Approach - For sound generation
- There will be need of PC running with OSC Monitor Software (tested with OSC Data Monitor and for download kasperkamperman) with static IP 192.168.178.34
- Need 2 devices one with AR application and one with Tuch OSC layout
- All three devices should be connected to the same router.
- For configuration follow all steps except 4

## OSC Installation & Configuration

- Install Touch OSC in your mobile/ tablet 
- Configure Touch OSC
- Host IP (Input the IP of a phone running AR application)
- In Port 9000
- Out Port 8000

## OSC & App Communication

- Once Touch OSC is configured then run AR application (Don't forget they both have to be on the same network)
- Local Host: For testing without a PC check no Host option and load the show. You will be able to control operations through Touch OSC (* wait for the moment that animal should appear and then perform actions)
- Host IP: For Host solution in terms of receiving OSC messages for audio purpose check Host option in the menu.(* wait for the moment that animal should appear and then perform actions)

In order to receive messages don't forget to refresh listening to the ports 8000 and 9000 on monitoring software, they have to be refreshed one application is loaded and animal is available.

## Things to remember:

- All devices must share the same network
- AR app should be running after Touch OSC configuration and for host solution Host should be running on the same network with static IP
- Host PC should have static IP
- In order to control and receive messages listening to ports 8000 and 9000 has to be refreshed otherwise there will be an issue in transmission from Touch OSC to APP and then app to Host.

## Narrator Voice

This application also has narrative mode which requires two phones running with unity application and one has to select receiver and other has to select sender mode after opening the application than communication channel will be open till application life cycle or till narrator donÕt leave the channel. For this purpose, VIVOX plugin has been used. 

## 3d Audio

For Doppler and 3D sound effect following configurations are used

Volume Intensity 0.65
Spatial Blend  = 3D
Reverb Zone Mix 1
Doppler Level 1
Volume Rolloff = Linear Rolloff
Min distance 0.25 units
Max Distance 3.25 units

## VIVOX Environment Variables

API End-Point https://mt1s.www.vivox.com/api2

## Technology / Architecture Detials

More details for architecture and setup can be found here
https://github.com/jantons/ar-app-i-like-digital/wiki/Application-Technology
