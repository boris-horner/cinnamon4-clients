# Installing Cinnamon 4 Change Trigger on Debian 13
## Install Change Trigger
* Install .net8:
  ```
  cd /tmp
  wget https://packages.microsoft.com/config/debian/13/packages-microsoft-prod.deb
  sudo dpkg -i packages-microsoft-prod.deb
  sudo apt update
  sudo apt install aspnetcore-runtime-8.0
  ```
* Move the `changetrigger` folder into `/opt`.
* Run with
  ```
  #!/bin/bash
  cd /opt/changetrigger
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
* Reload and enable service:
  ```
  systemctl daemon-reload
  systemctl enable changetrigger.service
  systemctl start changetrigger
  ```
* Check the status:
  ```
  systemctl status changetrigger.service
  ```
