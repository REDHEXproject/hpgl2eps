# HPGL to EPS Converter

### Overview

This project provides a simple C# program to convert HPGL (Hewlett-Packard Graphics Language) files into EPS (Encapsulated PostScript) format. The program reads an HPGL file, processes its commands, and translates them into corresponding PostScript commands.

### Features

• Reads HPGL commands from an input file.

• Converts basic HPGL commands (```PU```, ```PD```, ```PA```) into PostScript equivalents.

• Writes the converted commands into an EPS file.

• Scales the output to fit within a defined bounding box.


### Prerequisites

• .NET SDK installed on your system.

• Basic knowledge of C#.


## How to Use
1. Place an HPGL file in the project directory and name it input.hpgl (or modify the ```inputPath``` variable accordingly in the code).

2. Compile and run the program:

```
dotnet run
```
3. The converted EPS file will be saved as output.eps in the same directory.


### Code Explanation


• ```ConvertHPGLToEPS``` function:

  • Reads the HPGL file line by line.

  • Splits commands and processes them using ```ConvertHPGLCommandToPostScript```.

  • Writes the corresponding PostScript commands to the EPS file.

• ```ConvertHPGLCommandToPostScript``` function:

  • Converts ```PU``` (Pen Up) to ```moveto```.

  • Converts ```PD``` (Pen Down) to ```lineto```.

  • Converts ```PA``` (Plot Absolute) to ```moveto```.

• ```HandleCoordinates``` function:

  • Parses coordinate values and applies a scaling factor.
  

### Example HPGL Input
```
PU100,100;
PD200,200;
PA300,300;
```


### Example EPS Output

```
 %!PS-Adobe-3.0 EPSF-3.0
%%BoundingBox: 0 0 100 100
0.2662068576 0.2662068576 scale
newpath
100 100 moveto
200 200 lineto
300 300 moveto
stroke
showpage
```
## License

This project is open-source and available for modification and distribution.
