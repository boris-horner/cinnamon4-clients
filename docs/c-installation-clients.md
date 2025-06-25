# Installation instructions
## Summary
The Cinammon 4 client applications are not required for the [Cinnamon 4 server](https://github.com/dewarim/cinnamon4/tree/master) to work. You can run a core Cinnamon 4 server, access it over the API and create your own clients.

However, these applications provide functionality and integration interfaces useful for many or most use cases.
For reasons, we decided not to implement the clients in Java, as one would assume to make more sense. We preferred a .NET based desktop client which shares all core libraries with the applications running on the server.
Thus, the applications running on the server, including custom web applications, are built in .NET.

You can, however, access the Cinnamon 4 server API from any language or environment able to communicate with HTTP(S) and handle XML.

## Applications
### CAE
CAE is the acronym for Cinnamon Asynchronous Engine.

### Change Trigger

### Cinnamon 4 Desktop Client (CDCplus)

## Installing Cinnamon 4 client applications

* [Installing Cinnamon 4 CAE on Debian 12 Linux](t-installation-cae-debian12.md)
* [Installing Cinnamon 4 Change Trigger on Debian 12 Linux](t-installation-changetrigger-debian12.md)
* [Installing Cinnamon 4 Desktop Client (CDCplus) on Windows](t-installation-cdcplus-windows.md)
