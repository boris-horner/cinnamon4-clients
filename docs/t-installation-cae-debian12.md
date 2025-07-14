# Installing Cinnamon 4 CAE on Debian 12
## Install CAE
> [!NOTE]
> CAE, like Change Trigger, is not a Java application as the Cinnamon 4 server, but runs on .NET 8.

* Install .net8:
```
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
apt update
apt install -y dotnet-sdk-8.0
```
* Move the ```cae``` folder into ```/opt```.
* Run with
```
dotnet CAE.dll
```
> [!NOTE]
> The compiler creates a dll named like the exe to run it directly with the ```dotnet``` command. Thus, not the exe, but the dll is specified.



== Run CAE as a service
> [!NOTE]
> Make sure the startup script cae.sh contains ```#!/bin/bash``` as the first line.


* Create a systemd service file
```
nano /etc/systemd/system/cae.service
```
* Paste the following content and save the file:
```
[Unit]
Description=CAE Service
After=network.target
StartLimitIntervalSec=0

[Service]
Type=simple
User=install
WorkingDirectory=/opt/cae/bin
ExecStart=/opt/cae/bin/cae.sh
Restart=always
RestartSec=5

[Install]
WantedBy=multi-user.target
```
* Create a timer file to configure the start up delay:
```
nano /etc/systemd/system/cae.timer
```
* Paste this content and save the file:
```
[Unit]
Description=Start CAE Service after 30 seconds

[Timer]
OnBootSec=30
Unit=cae.service

[Install]
WantedBy=timers.target
```
* Reload and enable service and timer:
```
systemctl daemon-reload
systemctl enable cae.timer
systemctl enable cae.service
systemctl start cae.timer
```
* Check the status:
```
systemctl status cae.timer
systemctl status cae.service
```

