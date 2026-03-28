# DoenaSoft.AbstractionLayer.WPF

WPF abstractions that make WPF components easier to mock and unit-test. This project targets multiple frameworks to support legacy .NET Framework and modern .NET desktop applications.

Package Id: `DoenaSoft.AbstractionLayer.WPF`

Targets: net472, net8.0-windows, net10.0-windows

Usage:

Install the package from NuGet and program against the provided interfaces instead of concrete WPF types to make code testable.

License: MIT

## Interfaces

This package provides WPF-specific implementations of the interfaces defined in `DoenaSoft.AbstractionLayer.UI`. The key interfaces include:

### UI Services

- **`IUIServices`** — The primary entry point for UI operations. Provides methods to show message boxes and common file/folder dialogs:
  - `ShowMessageBox` — Display message boxes with configurable text, caption, buttons, and icon
  - `ShowOpenFileDialog` — Show open file dialogs for single or multiple file selection
  - `ShowSaveFileDialog` — Show save file dialogs
  - `ShowFolderBrowserDialog` — Show folder browser dialogs

### Clipboard Services

- **`IClipboardServices`** — Comprehensive clipboard operations:
  - **Contains methods:** `ContainsText`, `ContainsAudio`, `ContainsFileDropList`, `ContainsImage`, `ContainsData`
  - **Get methods:** `GetText`, `GetAudioStream`, `GetFileDropList`, `GetImage`, `GetData`, `GetDataObject`
  - **Set methods:** `SetText`, `SetAudio` (byte array or stream), `SetFileDropList`, `SetImage`, `SetData`, `SetDataObject`
  - **Clear method:** `Clear` — Remove all data from clipboard

### Thread Synchronization

- **`ISynchronizer`** — Invoke work on the UI thread:
  - `Invoke(Action)` — Synchronously invoke an action on the UI thread
  - `Invoke<T>(Func<T>)` — Synchronously invoke a function and return its result
  - `BeginInvoke(Action)` — Asynchronously invoke an action on the UI thread

- **`IDispatcherOperation`** — Represents a posted dispatcher operation:
  - `Result` — Gets the result after completion
  - `Status` — Gets the current status (Pending, Aborted, Completed, Executing)
  - `Task` — Gets a Task that represents the operation

## Supporting Data Types

### Dialog Configuration

- **`FileDialogOptions`** — Base configuration for file dialogs
- **`OpenFileDialogOptions`** — Configuration for open file dialogs (includes multi-select)
- **`SaveFileDialogOptions`** — Configuration for save file dialogs
- **`FolderBrowserDialogOptions`** — Configuration for folder browser dialogs

### Enumerations

- **`MessageButton`** — Message box button configuration (OK, YesNo, YesNoCancel)
- **`MessageIcon`** — Message box icon types (None, Information, Warning, Error, Question)
- **`Result`** — Dialog result values (OK, Cancel, Yes, No, etc.)
- **`DispatcherStatus`** — Dispatcher operation status (Pending, Aborted, Completed, Executing)

## Adapters

This package provides concrete WPF adapter implementations:

- **`WindowUIServices`** — Wraps WPF `MessageBox` and Microsoft.Win32 dialog types and implements `IUIServices`
- **`WindowClipboardServices`** — Wraps `System.Windows.Clipboard` and implements `IClipboardServices`
- **`WindowSynchronizer`** — Wraps WPF `Dispatcher.Invoke`/`BeginInvoke` and implements `ISynchronizer`

## Examples

Usage from application code (example view model):

```csharp
public class MyViewModel
{
    private readonly DoenaSoft.AbstractionLayer.UIServices.IUIServices _ui;
    private readonly DoenaSoft.AbstractionLayer.UIServices.IClipboardServices _clipboard;

    public MyViewModel(
        DoenaSoft.AbstractionLayer.UIServices.IUIServices ui,
        DoenaSoft.AbstractionLayer.UIServices.IClipboardServices clipboard)
    {
        _ui = ui;
        _clipboard = clipboard;
    }

    public void OpenFile()
    {
        var options = new OpenFileDialogOptions
        {
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            Title = "Open File"
        };

        if (_ui.ShowOpenFileDialog(options, out var fileName))
        {
            // use fileName
        }
    }

    public void PasteFromClipboard()
    {
        if (_clipboard.ContainsText)
        {
            var text = _clipboard.GetText();
            // use text
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

class FakeClipboardServices : DoenaSoft.AbstractionLayer.UIServices.IClipboardServices
{
    public bool ContainsText => true;
    public string ClipboardText { get; set; } = string.Empty;

    public bool ContainsAudio() => false;
    public bool ContainsFileDropList() => false;
    public bool ContainsImage() => false;
    public bool ContainsData(string format) => false;

    public void Clear() => ClipboardText = string.Empty;

    public string GetText() => ClipboardText;
    public Stream GetAudioStream() => null;
    public StringCollection GetFileDropList() => null;
    public object GetImage() => null;
    public object GetData(string format) => null;
    public object GetDataObject() => null;

    public bool SetText(string text)
    {
        ClipboardText = text;
        return true;
    }

    public void SetAudio(byte[] audioBytes) { }
    public void SetAudio(Stream audioStream) { }
    public void SetFileDropList(StringCollection filePaths) { }
    public void SetImage(object image) { }
    public void SetData(string format, object data) { }
    public void SetDataObject(object data, bool copy, int retryTimes, int retryDelay) { }
}

class FakeSynchronizer : DoenaSoft.AbstractionLayer.UI.Contracts.ISynchronizer
{
    public void Invoke(Action action) => action();

    public T Invoke<T>(Func<T> func) => func();

    public IDispatcherOperation BeginInvoke(Action action)
    {
        action();
        return new FakeDispatcherOperation();
    }
}

class FakeDispatcherOperation : DoenaSoft.AbstractionLayer.UI.Contracts.IDispatcherOperation
{
    public object Result => null;
    public DispatcherStatus Status => DispatcherStatus.Completed;
    public Task Task => Task.CompletedTask;
}
```

## Notes

- This package depends on `DoenaSoft.AbstractionLayer.UI` version 2.0.1 or later, which contains the contract interfaces and data types.
- The concrete adapter implementations in this package wrap WPF framework types (`System.Windows.*`) to implement the UI contracts.
- In production code, use the adapters provided by this package. In unit tests, reference only the contracts package and provide test doubles for the interfaces.
