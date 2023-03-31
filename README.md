# NoSpammy

Write messages, don't spam them.

## Usage

`/nospam` - Runs a command if the same command has not been run recently.

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