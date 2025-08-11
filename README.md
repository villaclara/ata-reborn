**ATA Reborn** is the successor of simple [ATA](https://github.com/villaclara/ATA) (ATA stays for _'Application Timer Application'_ - the time tracking app for Windows processes). 
This app allows you to measure time you spend on any application. 


Basically, I was planning to release major update of original ATA, but later decided to make new app, called Reborn (thanks Dota2) as it has been fully redesigned:

### New UI
- Simple and user friendly design.
- Resize Application window as you wish and the height/width will be automatically saved.
- Light and Dark themes are available with very simple toggling mode.
- Trigger 'Refresh' option to display latest actual times.
- Built with WPF, tried to stick to MVVM pattern as close as I could with the help of CommunityToolkit.MVVM.
- Full / Minimized dashboard layout.
- Fast info in tray icon context menu.

### Improved logic
- 5 Processes limit is no more an obstacle.
- Choose any running process in your system, no more limitations on some proceses.
- Run on Window Startup is now on/off option, available in settings.
- Logging via Serilog logs all important stuff into file, allows better debugging and watching flow of the app.
- Built on Interfaces and kept project structure clear.
- And many fixes for bugs occuring in original ATA.



Please navigate to [Releases](https://github.com/villaclara/ata-reborn/releases) to download latest version.

See you at the next update! :smile:


Here is an example of application:
- Default view:
<img width="1111" height="591" alt="image" src="https://github.com/user-attachments/assets/43282bbe-48a2-447d-a2e7-68f95f78bc9d" />

- Minimized view:
<img width="1114" height="591" alt="image" src="https://github.com/user-attachments/assets/1266b22e-bb71-42c4-81dc-ae3af6a5e7e4" />

