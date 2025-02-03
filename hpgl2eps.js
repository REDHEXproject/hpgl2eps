const fs = require('fs');

function convertHPGLToEPS(inputPath, outputPath) {
    const inputData = fs.readFileSync(inputPath, 'utf8');
    const lines = inputData.split('\n');
    let outputData = "%!PS-Adobe-3.0 EPSF-3.0\n";
    outputData += "%%BoundingBox: 0 0 100 100\n";
    outputData += "0.2662068576 0.2662068576 scale\n";
    outputData += "newpath\n";

    lines.forEach(line => {
        const commands = line.split(';');
        commands.forEach(command => {
            const postScriptCommand = convertHPGLCommandToPostScript(command);
            if (postScriptCommand) {
                outputData += postScriptCommand + "\n";
            }
        });
    });

    outputData += "stroke\n";
    outputData += "showpage\n";

    fs.writeFileSync(outputPath, outputData, 'utf8');
}

function convertHPGLCommandToPostScript(hpglCommand) {
    if (hpglCommand.startsWith("PU")) {
        return handleCoordinates(hpglCommand.substring(2), "moveto");
    } else if (hpglCommand.startsWith("PD")) {
        return handleCoordinates(hpglCommand.substring(2), "lineto");
    } else if (hpglCommand.startsWith("PA")) {
        return handleCoordinates(hpglCommand.substring(2), "moveto");
    }
    return null;
}

function handleCoordinates(coordinateString, command) {
    const coordinates = coordinateString.split(',');
    if (coordinates.length === 2) {
        const scaleFactor = 0.2662068576;
        const x = parseFloat(coordinates[0]) * scaleFactor;
        const y = parseFloat(coordinates[1]) * scaleFactor;
        return `${x} ${y} ${command}`;
    }
    return null;
}

const inputPath = 'input.hpgl';
const outputPath = 'output.eps';
convertHPGLToEPS(inputPath, outputPath);
