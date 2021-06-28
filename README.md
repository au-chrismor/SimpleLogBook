# Simple Log Book
# ... .. -- .--. .-.. . .-.. --- --. -... --- --- -.-
An amateur radio logbook program for Windows in C#

Sure, there are lots of Ham Radio logbook programs out there, so why not have one more?

SimpleLogBook is exactly that.  It doesn't try to be clever, it just stores all the necessary parts of your station operations.

## Requirements
- This code will build with Visual Studio 2019 Community, and it _should_ build with Visual Studio 2017 or later
- We need Windows 8.1 or later to run.  Perhaps with Xamarin, you could build it for MacOS, but I haven't tried that yet.
- The application is built for the .Net Framework 4.0, specifically version 4.6.1
- The database uses SQLite3, but your could change it to pretty much any database with a .Net provider.  I chose SQLite because it's neatly standalone and doesn't have a particularly onerous license.

## But I don't want to compile it myself
No problem, just grab the current release and install it.  There's nothing special required.