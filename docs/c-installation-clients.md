# Installation instructions
## Summary
The Cinammon 4 client applications are not required for the [Cinnamon 4 server](https://github.com/dewarim/cinnamon4/tree/master) to work. You can run a core Cinnamon 4 server, access it over the API and create your own clients.

However, these applications provide functionality and integration interfaces useful for many or most use cases.
For reasons, we decided not to implement the clients in Java, as one would assume to make more sense. We preferred a .NET based desktop client which shares all core libraries with the applications running on the server.
Thus, the applications running on the server, including custom web applications, are built in .NET.

You can, however, access the Cinnamon 4 server API from any language or environment able to communicate with HTTP(S) and handle XML.

## Applications
### CAE
CAE is the acronym for Cinnamon Asynchronous Engine. CAE is used for peripheral functions that do not run synchronously in the Cinnamon 4 server.
Typical examples are:
* Managing translation requests, creating the data structure
* Executing publication requests, like DITAmap to PDF
* Rendering thumbnails
* Asynchronous push / pull interfaces

### Change Trigger
Cinnamon 4 Change Trigger is a web service providing end points that can be called by Cinnamon 4 server.
The server configuration allows the definition of triggers associated with server API methods. Whenever such an API method is called, the server will call the configured endpoint.
Triggers can be configured so that they are called before executing the API call (pre-triggers), after the API call, but before commit (post-triggers) or after commit (post-commit-triggers).
Cinnamon 4 uses a standard change trigger in combination with the desktop client, CDCplus. When a new user account is created, the change trigger creates a set of folders that the client needs for every user.
Custom plugins performing other tasks can easily be added.

### Cinnamon 4 Desktop Client (CDCplus)
CDCplus is the standard Windows client for interacting with the server. It is a powerful, modular, multipurpose GUI client that can easily be extended with custom functionality through a set of defined interfaces.
These interfaces are also used by the client itself for its own standard functions, thus, these functions can not only be supplemented, but also replaced if required.

## Installing Cinnamon 4 client applications

* [Installing Cinnamon 4 CAE on Debian 12 Linux](t-installation-cae-debian12.md)
* [Installing Cinnamon 4 Change Trigger on Debian 12 Linux](t-installation-changetrigger-debian12.md)
* [Installing Cinnamon 4 Desktop Client (CDCplus) on Windows](t-installation-cdcplus-windows.md)
