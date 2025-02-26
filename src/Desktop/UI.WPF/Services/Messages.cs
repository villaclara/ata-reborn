using Shared.ViewModels;

namespace UI.WPF.Services;


/// <summary>
/// Message sent when the Selected App from Processes List was added to Tracking.
/// </summary>
public record class TrackedAppAddedMessage(string ProcessName, string? AppName);

/// <summary>
/// Message sent when the Selected App from TrackedAppItemViewModel was removed from Tracking.
/// </summary>
public record class TrackedAppDeletedMessage(string AppName);

/// <summary>
/// Message sent when the selected App from TrackedAppItemViewModel_Minimal meant to display FULL TrackedAppItem.
/// </summary>
public record class InfoAppMessage(string AppName);


/// <summary>
/// Message sent when user clicks on 'Show full history' button. Received in Toolbar to navigate to the corresponding View.
/// </summary>
/// <param name="appVM">App instance with full parameters.</param>
public record class FullHistoryForAppTriggeredMessage(AppInstanceVM appVM);


public record class MessageApp(AppInstanceVM appVM);