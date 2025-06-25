# Installing Cinnamon 4 Change Trigger on Debian 12
## Install Change Trigger
* Install .net8:
  ```
  wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
  dpkg -i packages-microsoft-prod.deb
  rm packages-microsoft-prod.deb
  apt update
  apt install -y dotnet-sdk-8.0
  ```
* Move the `changetrigger` folder into `/opt`}.
* Run with
  ```
  dotnet ChangeTrigger.dll --urls="http://127.0.0.1:8081"
  ```
> [!NOTE]
> The compiler creates a dll named like the exe to run it directly with the `dotnet` command. Thus, not the exe, but the dll is specified.



## Run Change Trigger as a service
> [!NOTE]
> Make sure the startup script run.sh contains `#!/bin/bash` as the first line.

* Create a systemd service file
  ```
  nano /etc/systemd/system/changetrigger.service
  ```
* Paste the following content and save the file:
  ```
  [Unit]
  Description=ChangeTrigger Service
  After=network.target
  StartLimitIntervalSec=0
  
  [Service]
  Type=simple
  User=install
  WorkingDirectory=/opt/changetrigger
  ExecStart=/opt/changetrigger/run.sh
  Restart=always
  RestartSec=5
  
  [Install]
  WantedBy=multi-user.target
  ```
* Create a timer file to configure the start up delay:
  ```
  nano /etc/systemd/system/changetrigger.timer
  ```
* Paste this content and save the file:
  ```
  [Unit]
  Description=Start ChangeTrigger Service after 10 seconds
  
  [Timer]
  OnBootSec=10
  Unit=changetrigger.service
  
  [Install]
  WantedBy=timers.target
  ```
* Reload and enable service and timer:
  ```
  systemctl daemon-reload
  systemctl enable changetrigger.timer
  systemctl enable changetrigger.service
  systemctl start changetrigger.timer
  ```
* Check the status:
  ```
  systemctl status changetrigger.timer
  systemctl status changetrigger.service
  ```
