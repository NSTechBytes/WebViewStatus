# WebViewStatus - Rainmeter Plugin

**WebViewStatus** is a **Rainmeter** plugin that checks whether **WebView2 Runtime** is installed on your system. It looks for WebView2's presence by checking the Windows registry for specific registry entries associated with WebView2. This plugin provides an easy way to display the installation status of WebView2 Runtime in your Rainmeter skins.

## Features

- **Checks if WebView2 Runtime is installed** by inspecting the Windows registry.
- Returns `1` if WebView2 Runtime is installed and `0` if it is not installed.
- Easy integration into Rainmeter skins with a simple measure.

## Installation

1. **Download the latest release** of **WebViewStatus** from the [Releases page](https://github.com/NSTechBytes/WebViewStatus/releases).
2. **Install the plugin** by copying `WebViewStatus.dll` into your Rainmeter `Plugins` directory:
   - The default path is:  
     `C:\Users\<YourUsername>\Documents\Rainmeter\Plugins\`

3. After installation, you can use the plugin in your Rainmeter skins.

## Usage

### 1. Create a Measure in your Rainmeter skin

In your `.ini` skin file, define a measure that uses the **WebViewStatus** plugin.

```ini
[Rainmeter]
Update=1000
BackgroundMode=2
SolidColor=FFFFFF

[mWebView2]
Measure=Plugin
Plugin=WebViewStatus
Type=WebView2

[Text]
Meter=STRING
MeasureName=mWebView2
X=5
Y=35
W=200
H=70
FontColor=000000
stringAlign = LeftCenter
Antialias=1
FontSize=12
FontFace=Arial
Text="WebView2 Installed: %1"
```

In this example:
- `%1` will display `1` if WebView2 Runtime is installed, or `0` if it is not.


## Parameters

- **None**: This plugin doesn't require any additional parameters in the `.ini` skin file.

## Troubleshooting

- **WebView2 Runtime Not Found**: If the plugin returns `0` and you are sure WebView2 Runtime is installed, ensure the registry entries are correct and WebView2 is properly installed.
- **File Not Found**: The plugin checks registry paths related to WebView2 Runtime. If WebView2 is installed but not detected, verify the registry keys used by the plugin match your installation.

## Contributing

If you'd like to contribute to this project, feel free to fork the repository and submit a pull request. Please follow the existing style and include tests where applicable.

### Bug Reports & Feature Requests

If you encounter bugs or want to suggest features, please use the [Issues tab](https://github.com/NSTechBytes/WebViewStatus/issues).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

