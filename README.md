# Trajectory Prediction

This is a Unity3D Project to implement a trajectory prediction system for a projectile
in 3D space. A lot of approaches online involve setting up a parallel scene to simulate the 
motion at an accelerated rate and send the resulting data to the current scene to predict 
the trajectile. I have implemented the prediction system using physics through the following
formula:

P - P<sub>o</sub> = u * t + 0.5 * a * t * t

in vector format.