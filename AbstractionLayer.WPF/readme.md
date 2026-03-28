# DoenaSoft.AbstractionLayer.WPF

WPF abstractions that make WPF components easier to mock and unit-test. This project targets multiple frameworks to support legacy .NET Framework and modern .NET desktop applications.

Package Id: `DoenaSoft.AbstractionLayer.WPF`

Supported targets:

- net472
- net6.0-windows
- net8.0-windows

Usage:

Install the package from NuGet and program against the provided interfaces instead of concrete WPF types to make code testable.

License: MIT


Interfaces and patterns

- Primary interface: `IUIServices` is the main service interface used by WPF projects. It provides methods to show message boxes and file/folder dialogs and to perform other UI-related services.
- Supporting interfaces present in the UI contracts package: `IClipboardServices`, `ISynchronizer`, and `IDispatcherOperation`. Dialog option types (`OpenFileDialogOptions`, `SaveFileDialogOptions`, `FolderBrowserDialogOptions`) and enums (`Buttons`, `Icon`, `Result`) are also included in the contracts package.

Common scenarios

- Test view model logic that interacts with the UI by injecting a fake `IUIServices` and asserting calls to message/dialog APIs.
- Use `ISynchronizer` to simulate dispatcher invocation in tests without a real UI thread.

Adapters

Platform-specific adapters in this project (for example `WindowUIServices`) convert WPF framework types into implementations of the interfaces defined in the UI contracts package.

Examples

Usage from application code (example view model):

```csharp
public class MyViewModel
{
    private readonly DoenaSoft.AbstractionLayer.UIServices.IUIServices _ui;

    public MyViewModel(DoenaSoft.AbstractionLayer.UIServices.IUIServices ui)
    {
        _ui = ui;
    }

    public void OpenFile()
    {
        if (_ui.ShowOpenFileDialog(new OpenFileDialogOptions { Filter = "Text|*.txt" }, out var fileName))
        {
            // use fileName
        }
    }
}
```

Simple fake for unit tests:

```csharp
class FakeUIServices : DoenaSoft.AbstractionLayer.UIServices.IUIServices
{
    public Result ShowMessageBox(string text, string caption, Buttons buttons, Icon icon)
    {
        return Result.OK;
    }

    public bool ShowOpenFileDialog(OpenFileDialogOptions options, out string fileName)
    {
        fileName = "test.txt";
        return true;
    }

    public bool ShowOpenFileDialog(OpenFileDialogOptions options, out string[] fileNames)
    {
        fileNames = new[] { "test.txt" };
        return true;
    }

    public bool ShowSaveFileDialog(SaveFileDialogOptions options, out string fileName)
    {
        fileName = "out.txt";
        return true;
    }

    public bool ShowFolderBrowserDialog(FolderBrowserDialogOptions options, out string folder)
    {
        folder = "C:\\Temp";
        return true;
    }
}
```

Notes about dependency and adapters

- This package depends on `DoenaSoft.AbstractionLayer.UI` which contains the interface contracts (for example `IUIServices`, `IClipboardServices`, `ISynchronizer` and dialog option types). That package provides only the contracts and data types.
- `DoenaSoft.AbstractionLayer.WPF` provides concrete adapter implementations (for example `WindowUIServices`) that wrap WPF framework types and implement the contracts from the UI package. Use these adapters in production; for unit tests reference the contracts package and provide test doubles for the interfaces.
