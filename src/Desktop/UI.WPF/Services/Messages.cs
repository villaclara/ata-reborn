using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
/// Message sent when user clicks on 'Show full history' button. Received in Toolbar to navigate to the corresponding View.
/// </summary>
/// <param name="appVM">App instance with full parameters.</param>
public record class FullHistoryForAppTriggeredMessage(AppInstanceVM appVM);


public record class MessageApp(AppInstanceVM appVM);