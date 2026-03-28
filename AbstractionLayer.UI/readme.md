# DoenaSoft.AbstractionLayer.UI

Common UI abstractions used by the WinForms and WPF projects. This library provides small interface definitions that mirror important surface area of framework UI types so that application code can depend on interfaces instead of concrete types.

Package Id: `DoenaSoft.AbstractionLayer.UI`

Targets: netstandard2.0, net472, net10.0

## Interfaces

The `AbstractionLayer.UI` project exposes the following interfaces in the `Contracts` folder. These are the actual interfaces implemented and used by the platform-specific adapters.

### Main UI Services

- **`IUIServices`** (namespace `DoenaSoft.AbstractionLayer.UIServices`) — The primary UI service interface. Provides methods to show message boxes and common file/folder dialogs:
  - `ShowMessageBox` — Display a message box with configurable text, caption, buttons, and icon
  - `ShowOpenFileDialog` — Show an open file dialog for single or multiple file selection
  - `ShowSaveFileDialog` — Show a save file dialog
  - `ShowFolderBrowserDialog` — Show a folder browser dialog

### Clipboard Operations

- **`IClipboardServices`** (namespace `DoenaSoft.AbstractionLayer.UIServices`) — Clipboard operations:
  - `ContainsText` — Check if clipboard contains text
  - `GetText` — Retrieve text from clipboard
  - `SetText` — Set text to clipboard
  - `SetDataObject` — Set data object to clipboard with retry logic and persistence options

### Thread Synchronization

- **`ISynchronizer`** (namespace `DoenaSoft.AbstractionLayer.UI.Contracts`) — Invoke work on the UI thread:
  - `Invoke(Action)` — Synchronously invoke an action on the UI thread
  - `Invoke<T>(Func<T>)` — Synchronously invoke a function on the UI thread and return its result
  - `BeginInvoke(Action)` — Asynchronously invoke an action on the UI thread

- **`IDispatcherOperation`** (namespace `DoenaSoft.AbstractionLayer.UI.Contracts`) — Represents a posted dispatcher operation and exposes:
  - `Result` — Gets the result after completion
  - `Status` — Gets the current status (Pending, Aborted, Completed, Executing)
  - `Task` — Gets a Task that represents the operation

## Supporting Data Types

### Dialog Configuration

- **`FileDialogOptions`** — Base configuration for file dialogs (filter, initial folder, title)
- **`OpenFileDialogOptions`** — Configuration for open file dialogs (extends FileDialogOptions, adds multi-select support)
- **`SaveFileDialogOptions`** — Configuration for save file dialogs (extends FileDialogOptions, adds overwrite prompt)
- **`FolderBrowserDialogOptions`** — Configuration for folder browser dialogs (description, selected path)

### Enumerations

- **`Buttons`** — Message box button configuration (OK, OKCancel, YesNo, YesNoCancel, etc.)
- **`Icon`** — Message box icon types (None, Information, Warning, Error, Question)
- **`Result`** — Dialog result values (OK, Cancel, Yes, No, etc.)
- **`DispatcherStatus`** — Dispatcher operation status (Pending, Aborted, Completed, Executing)

## Usage

Program against these interfaces (for example `IUIServices` and `ISynchronizer`) and use the platform-specific adapter implementations in the WinForms or WPF projects. In unit tests provide fakes or mocks for these interfaces to avoid showing UI and to verify interactions.

### Example

```csharp
public class MyViewModel
{
    private readonly DoenaSoft.AbstractionLayer.UIServices.IUIServices _uiServices;

    public MyViewModel(DoenaSoft.AbstractionLayer.UIServices.IUIServices uiServices)
    {
        _uiServices = uiServices;
    }

    public void SaveFile(string content)
    {
        var options = new SaveFileDialogOptions
        {
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            Title = "Save File"
        };

        if (_uiServices.ShowSaveFileDialog(options, out string fileName))
        {
            // Save content to fileName
        }
    }
}
```

Reference this project from UI-specific packages (WinForms/WPF) or your application and program against the interfaces. Concrete adapter/extension types in the platform-specific projects map the real framework objects to these interfaces at runtime.

License: MIT
