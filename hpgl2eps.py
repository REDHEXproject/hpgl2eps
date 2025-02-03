import os

def convert_hpgl_to_eps(input_path, output_path):
    with open(input_path, 'r', encoding='utf-8') as infile, open(output_path, 'w', encoding='utf-8') as outfile:
        outfile.write("%!PS-Adobe-3.0 EPSF-3.0\n")
        outfile.write("%%BoundingBox: 0 0 100 100\n")
        outfile.write("0.2662068576 0.2662068576 scale\n")
        outfile.write("newpath\n")

        for line in infile:
            commands = line.strip().split(';')
            for command in commands:
                postscript_command = convert_hpgl_command_to_postscript(command)
                if postscript_command:
                    outfile.write(postscript_command + "\n")

        outfile.write("stroke\n")
        outfile.write("showpage\n")

def convert_hpgl_command_to_postscript(hpgl_command):
    if hpgl_command.startswith("PU"):
        return handle_coordinates(hpgl_command[2:], "moveto")
    elif hpgl_command.startswith("PD"):
        return handle_coordinates(hpgl_command[2:], "lineto")
    elif hpgl_command.startswith("PA"):
        return handle_coordinates(hpgl_command[2:], "moveto")
    return None

def handle_coordinates(coordinate_string, command):
    coordinates = coordinate_string.split(',')
    if len(coordinates) == 2:
        scale_factor = 0.2662068576
        try:
            x = float(coordinates[0]) * scale_factor
            y = float(coordinates[1]) * scale_factor
            return f"{x} {y} {command}"
        except ValueError:
            return None
    return None

input_path = "input.hpgl"
output_path = "output.eps"
convert_hpgl_to_eps(input_path, output_path)
