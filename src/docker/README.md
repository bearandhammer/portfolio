# WSL 2 for Windows and Docker Engine Support

## Introduction

The following code snippets pair with [this blog post]() where I cover configured WSL 2 on Window and the go through configuring the Docker Engine without the need for Docker Desktop. The headings for each section match those included in the post, in exactly the same order (for ease of consumption).

## Utility Files (as Gists)

The post comments on the use of the following files:

:small-orange-diamond: ***docker-compose.yml***
:small-orange-diamond: ***wsl.conf***
:small-orange-diamond: ***.wslconfig***

## Code Snippets

```shell
wsl --install -d ubuntu
```

```shell
wsl -l -v
```

```shell
wsl --set-version Ubuntu 2
```

```bash
sudo apt-get remove docker docker-engine docker.io containerd runc
```

```bash
sudo apt-get update
```

```bash
sudo apt-get install \
    ca-certificates \
    curl \
    gnupg \
    lsb-release
```

```bash
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
```

```bash
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

```bash
sudo apt-get update
```

```bash
sudo apt-get install docker-ce docker-ce-cli containerd.io
```

```bash
sudo service docker start
```

```bash
docker -v
```

```bash
docker run hello-world
```

```bash
sudo groupadd docker
```

```bash
sudo usermod -aG docker $USER
```

```bash
docker pull mcr.microsopft.com/mssql/server:2017-latest
```

```bash
docker image ls
```

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD={YOUR_STRONG_PASSWORD}" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

```bash
docker container ls
```

```bash
docker container ls -a
```

```bash
sudo curl -L https://github.com/docker/compose/releases/download/1.29.2/docker-compose-`uname -s`-`uname -m` -o /usr/local/bin/docker-compose
```

```bash
sudo chmod +x /usr/local/bin/docker-compose
```

```yml
version: '3.1'

services:
  sql-server-db:
    container_name: sql-server-db
    image: mcr.microsoft.com/mssql/server:2017-latest
    restart: always
    ports:
      - "1433:1433"
    environment:
      SA_PASSWORD: "{YOUR_STRONG_PASSWORD}"
      ACCEPT_EULA: "Y"
  mongo:
    image: mongo:latest
    container_name: mongo-db
    restart: always
    environment:
      MONGO_INITDB_ROOT_USERNAME: root
      MONGO_INITDB_ROOT_PASSWORD: {PASSWORD}
    ports:
      - 27017:27017
  mongo-express:
    image: mongo-express:0.54
    container_name: mongo-express
    restart: always
    ports:
      - 8081:8081
    environment:
      ME_CONFIG_MONGODB_ADMINUSERNAME: root
      ME_CONFIG_MONGODB_ADMINPASSWORD: {PASSWORD}
    depends_on:
      - mongo
```

```bash
cd /mnt/c/dockercompose
```

```bash
docker-compose up
```

```bash
sudo chown -R myuser /path/to/folder
```

```text
[boot]
command = service docker start
```

```text
# Settings apply across all Linux distros running on WSL 2
[wsl2]

# Limits VM memory to use no more than 4 GB, this can be set as whole numbers using GB or MB
memory=2GB 

# Sets the VM to use two virtual processors
processors=2

# Turn off default connection to bind WSL 2 localhost to Windows localhost
localhostforwarding=true

# Turns on output console showing contents of dmesg when opening a WSL 2 distro for debugging
debugConsole=true
```

## Utility Snippets (not mentioned in post)

Install a toolkit for making network changes for the Linux kernel:

```bash
sudo apt install net-tools
```
