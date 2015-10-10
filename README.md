### Summary

This is a simple auto-bulldoze script that does the minimum possible to accomplish the job.

The small set of requirements should lead to small code that is easy to check for correctness.

#### Building the Mod

I use Visual Studio 2015 Profession to develop this. I would expect VS2015 Community to work as well.

1. Open the solution.
2. Fix the probably broken references to DLLs in the Cities: Skylines directory. There is an issue files to fix this .
3. Build.

#### Background

This mod currently removes abandoned and burned down buildings every 5 game seconds. No animation/sound is
played, they simply disappear ...

Bits of code comes from "Automatic Bulldoze" by Sadler. I searched for "bulldoze", sorted by "Top Rated All Time", and that was the top result. I looked at the source code (which for some reason had the extension .ccs instead of .cs) and observed multiple problems (i.e. performance and maintainability). I decided to write my own.

I must state I know next to nothing about modding Cities: Skylines, but I am a professional software engineer
and have some experience writing plug-ins for Kerbal Space Program.

#### Requirements

Here are the project requirements:

1. Remove abandoned and burned down buildings periodically because I'm lazy. (done)
2. Somehow signal to the player how many and how often building are being destroyed, so any city issues aren't hidden. (not implemented yet)
3. Provide a way to enable/disable the mod in-game very easily, like a checkbox somewhere. (not implemented yet)
