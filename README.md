# NoSpammy

Write messages, don't spam them.

## Installation

This is a Dalamud plugin, requiring the use of [FFXIVQuickLauncher](https://github.com/goatcorp/FFXIVQuickLauncher).

1. Type the `/xlsettings` command into your chatbox. This will open your Dalamud Settings.
2. In the window that opens, navigate to the "Experimental" tab. Scroll down to "Custom Plugin Repositories".
3. Copy and paste the repo URL (seen below) into the input box, **making sure to press the "+" button to add it.**
4. Press the "Save and Close" button. This will add this plugin to Dalamud's list of available plugins.
5. Open the plugin installer by typing the `/xlplugins` command, search for the plugin, and click install.

### Repo URL

`https://raw.githubusercontent.com/smrq/NoSpammy/master/repo.json`

## Usage

`/nospam` - Runs a command if the same command has not been run recently.

`/nospamcfg` - Shows the config window.

```
/nospam party Raising <t> <se.9>
/ac Raise <t>
/micon Raise
```

Any command can be prefixed with `/nospam` to prevent it from running repeatedly. A command prefixed with `/nospam` will only run if the debouncing interval (default: 1.5 seconds) has elapsed since the last time it was run.

```
/echo This will run.
/nospam echo Ping!
/wait 1
/echo These will not run.
/nospam echo Ping!
/wait 1
/nospam echo Ping!
/wait 1
/nospam echo Ping!
/wait 1
/nospam echo Ping!
/wait 1
/nospam echo Ping!
/wait 3
/echo This will run.
/nospam echo Ping!
```
