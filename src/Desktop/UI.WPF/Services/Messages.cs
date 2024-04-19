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