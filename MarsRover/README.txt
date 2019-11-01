Welcome to the thrilling Mars exploration simulator: MarsRover (TM) - Critically acclaimed by everyone & thier grandmother!

MarsRover is written in C3 and compiled as a .net Core 2.1 app - you will need to donwload and install the runtime from the following location: https://dotnet.microsoft.com/download if you do not already have it.

To run the app, in the command prompt use the following command:
	dotnet MarsRover.dll {filename}
where {filename} is replaced with the text file containing your Mars exploration plan (only your imagination can hold you back!)

e.g. if the plan for Mars exploration & domination is located in the c: root, named odyssey.txt the command would be:
	dotnet MarsRover.dll c:\odyssey.txt

Thoughts, Concerns & Allegations regarding the design and responsible use of this software:

	1) The specification for the plan file format stressed me greatly, as there was no separating character between the x and y co-ordinate. In the real word this might have been resolved with a strongly worded email to the product owner, but since the developer is a rather nice chap the decision was made to deal with it based on the assumption that since the zone is always a square, the x and y co-ordinates will be identical.
	2) BE RESPONSIBLE! If you create a plan that will send the rover out of the 'safe zone', it will be stolen by a martian and be repurposed as a rather shiny hat. In MarsRover v2.0 we plan to include an option to validate the planned route prior to uploading the instruction to the rover. For now we sleep soundly thinking of the joy shiny hats bring to those less fortunate.
	3) Disclaimer: Although this software has been thoroughly tested buy the developer through unit testing and test plans, despite being a nice chap he cannot be held responsible for any damages resulting from the use of this software. Possible side effects may include (but are not limited to) BSODs, leprosy & insomnia.

Lastly, I thank you for the opportunity of taking this technical assesment. The task took in total roughly 6 hours. I hope you enjoy conquering the mysterious martian geography using this state of the art simulation engine, and if nothing else, I hope this readme made you smile at least once!