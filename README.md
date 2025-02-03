# Cinnamon 4 clients
## Server software
Cinnamon 4 server is a separate project maintained in close cooperation with the Cinnamon 4 clients project. Cinnamon 4 server is required to use the various clients in this project.

Find code, documentation and more details [here](https://github.com/dewarim/cinnamon4).

## Summary
Cinnamon 4 (as the successor of Cinnamon 3) is an open source, general purpose, client/server content and document management platform. Cinnamon 4 can be used out of the box with the server and 
Windows Desktop Client installed, but it can also be used with less, more or different client applications, like custom web frontends and the like.

Cinnamon 4 is highly modular and flexibkle and integrates easily with third party software with APIs.

Cinnamon 4, client and server, provide a complete open source system that can be used right away without purchasing licenses. There are vendors, particularly, the project's main contributor 
[texolution](https://texolution.eu), selling modules that extend or replace open source components to provide functionality for various use cases. Here are some examples:
* Enterprise Package, a set of client and server modules adding functionality turning Cinnamon in a Component CMS for authoring of modular technical documentation in DITA or other data formats and models.
Enterprise Package features module-based and version sensitive translation management.
* Smart AI Hub is a web-based, configurable and extensible middleware managing AI prompts and models and providing flexible RAG (Retrieval Augmented Generation) based on your own and public data sources.
* Further applications include high-end, automatic publication of investment banking reports, or management of technical supplier documentation requests in plant engineering.
* And apart from that, you can use the platform as-is or supplemented by commercial add-ons to build your custom applications on it.

# Client projects and applications
The cinnamon4-clients repository contains several projects for libraries, and they are combined to three applications available as VisualStudio solutions in the source code:
* **C4CDCplus:** Cinnamon Desktop Client, a .net 8 application for Windows, enabling access to the server with a GUI resembling Windows File Explorer.
* **C4CAE:** Cinnamon Asynchronous Engine, a .net 8 core application running on the server (thus, typically on Linux). CAE is a framework to run plugins to create summaries from metadata (open source). Commercial plugins as part of Enterprise Package or other products provide translation management, DITA Open Toolkit integration, Workflow Management and other functionality. Custom plugins can be added that, for example, connect to third party systems to fetch data.
* **C4ChangeTrigger:** Change triggers implement .net 8 core based microservice plugins that run in the ChangeTrigger framework. The Cinnamon server can be configured to call such microservices before or after any API command, or after its commit. Apart from some system functions implemented as Change Triggers by standard, and some extensions like the billing and LLM integration in Smart AI Hub, custom functions can easily be added.
