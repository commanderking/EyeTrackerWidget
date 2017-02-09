# EyeTrackerWidget
Eye Tracking Widget using Tobii 4c Eye Tracker 

The Eye Tracker Widget displays realtime eye tracking data from the Tobii 4c Eye Tracker, specifically the x-coordinate and 
y-coordinate of the user's gaze on the screen. It also outputs this data into a csv file for further data analysis. 

#Prerequisites
* Tobii Eye Tracker Device. Tested with Tobii 4c.
* [Tobii Eye Tracking Core Software](https://tobiigaming.com/getstarted/)
* Windows 10
* Visual Studio 2015. Tested with Community edition.

#Getting Started
The primary solution, titled UserPresenceWpf is located at /source/WpfSamples/UserPresenceWpf. Open this file in Visual Studio to begin editing. 

A release build that works out of the box can be found at /source/WpfSamples/UserPresenceWpf/bin/x86/Release. Open UserPresenceWpf (Type Application) to run the program.
All collected data will be found in the Output folder under gazeDataOutput.csv. As of now, each new session will replace the data in the old gazeDataOut.csv
