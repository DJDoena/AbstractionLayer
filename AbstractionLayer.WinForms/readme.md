# DoenaSoft.AbstractionLayer.WinForms

WinForms abstractions that make Windows Forms components easier to mock and unit-test. This project targets multiple frameworks to support legacy .NET Framework and modern .NET desktop applications.

Package Id: `DoenaSoft.AbstractionLayer.WinForms`

Supported targets:

- net472
- net6.0-windows
- net8.0-windows

Usage:

Install the package from NuGet and program against the provided interfaces instead of concrete WinForms types to make code testable.

License: MIT


Interfaces and adapters

- Primary interface: `IUIServices` is the primary entry point in WinForms projects. It provides methods to show message boxes and common file/folder dialogs and to perform other UI-related services.
- Supporting interfaces present in the UI contracts package: `IClipboardServices`, `ISynchronizer`, and `IDispatcherOperation`. Dialog option types (for example `OpenFileDialogOptions`, `SaveFileDialogOptions`, `FolderBrowserDialogOptions`) and enums (`Buttons`, `Icon`, `Result`) are also provided by the contracts package.
- Adapters: concrete adapter types in this project (for example `FormUIServices`, `FormClipboardServices`) wrap `System.Windows.Forms` types and implement the contract interfaces from `DoenaSoft.AbstractionLayer.UI`.

Examples

- Inject `IUIServices` to avoid showing real dialogs during tests and verify what would have been shown.
- Use `ISynchronizer` in tests to simulate invocation to the UI thread without requiring an actual UI runtime.

Design guidance

Keep interfaces small and focused: prefer targeted interfaces for the behaviors your code needs rather than mirroring the entire WinForms API.

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

    public void SaveCommand()
    {
        if (_ui.ShowMessageBox("Save changes?", "Confirm", Buttons.YesNo, Icon.Question) == Result.Yes)
        {
            // perform save
        }
    }
}
```

Simple fake for unit tests:

```csharp
class FakeUIServices : DoenaSoft.AbstractionLayer.UIServices.IUIServices
{
    public Result LastMessageBoxResult { get; set; } = Result.Yes;

    public Result ShowMessageBox(string text, string caption, Buttons buttons, Icon icon)
    {
        return LastMessageBoxResult;
    }

    // Implement dialog methods with simple deterministic behavior for tests
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

- This package depends on `DoenaSoft.AbstractionLayer.UI` which contains the contract interfaces (for example `IUIServices`, `IClipboardServices`, `ISynchronizer`, and related dialog option types). The UI package is provided as a separate NuGet package and contains only the interfaces and data types.
- `DoenaSoft.AbstractionLayer.WinForms` provides concrete adapters and implementations for those interfaces (for example `FormUIServices`, `FormClipboardServices`) that wrap `System.Windows.Forms` components. In production you reference and use this package; in unit tests you should depend on the contracts (`DoenaSoft.AbstractionLayer.UI`) and supply test doubles for the interfaces.
