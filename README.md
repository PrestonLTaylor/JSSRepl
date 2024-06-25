# JSSRepl

A simple REPL for the JSS JavaScript Engine made in C# and Blazor.

![](https://i.imgur.com/oFcKIv3.png)

# Configuration

You can configure how long a script can run by defining the "ScriptTimeoutMilliseconds" under the "JSSExecution" section.

This has a default of 10000ms.

For example this will set the script timeout to be 5 seconds:

```
"JSSExecution": {
    "ScriptTimeoutMilliseconds": 5000
}
```

# License

This project is licensed under the [MIT License](https://github.com/PrestonLTaylor/JSSRepl/blob/master/LICENSE).
