# Installing Cinnamon 4 CAE on Debian 13
## Install CAE
> [!NOTE]
> CAE, like Change Trigger, is not a Java application as the Cinnamon 4 server, but runs on .NET 8.

* Install .net8:
```
cd /tmp
wget https://packages.microsoft.com/config/debian/13/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt update
sudo apt install aspnetcore-runtime-8.0
```
* Move the ```cae``` folder into ```/opt```.
* Run with
```
#!/bin/bash
cd /opt/cae/bin
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
Wants=network-online.target
After=network-online.target

[Service]
Type=simple
User=install
WorkingDirectory=/opt/cae/bin
ExecStart=/opt/cae/bin/cae.sh

Restart=on-failure
RestartSec=5
TimeoutStopSec=60

SyslogIdentifier=cae

[Install]
WantedBy=multi-user.target
```
* Reload and enable service:
```
systemctl daemon-reload
systemctl enable cae.service
```
* Check the status:
```
systemctl status cae.service
```


