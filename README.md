# Polygon Rotation Matcher

Recognize the same cyclic marker sequence after changing the starting point.

![Sample run from Polygon Rotation Matcher](docs/demo.webp)

## Run

Install .NET 8 SDK and runtime. Run these commands from the repository root.

```sh
dotnet build --nologo
dotnet bin/Debug/net8.0/App.dll sample.json
```

Replace `sample.json` with a JSON file following the same structure. The app prints a JSON report to standard output.

Give two sequences of up to 1000 nonempty strings. Offset is the number of positions shifted left. Reflections are excluded.

## Verify

Run `python check.py` with Python 3.11 or newer after the build step when applicable. The check verifies the bundled scenario and confirms that an invalid input returns an error.
