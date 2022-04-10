# WSL 2 for Windows and Docker Engine Support

## Introduction

The following code snippets pair with [this blog post]() where I cover configured WSL 2 on Window and the go through configuring the Docker Engine without the need for Docker Desktop. The headings for each section match those included in the post, in exactly the same order (for ease of consumption).

## Utility Files (as Gists)

The post comments on the use of the following files:

- [x] :small-orange-diamond: docker-compose.yml
- [x] :small-orange-diamond: wsl.conf
- [x] :small-orange-diamond: .wslconfig

A gist exists [here](https://gist.github.com/bearandhammer/0103f70b95a68ff19fd1165d57b91591) if you don't want to copy the code in the below snippets, preferring a downloadable zip of files instead. Double check that the files fit naming conventions for the targeting tools before use, which, at the time of writing, they do. 

***NOTE: The `docker-compose.yml` file contains placeholder strings (denoted by `{}`) which require replacing before use.***

## Code Snippets

Install WSL 2 (Ubuntu distribution) on Windows:

```shell
wsl --install -d ubuntu
```

Check WSL Ubuntu version:

```shell
wsl -l -v
```

Update WSL Ubuntu to version 2, if required:

```shell
wsl --set-version Ubuntu 2
```

Remove all existing Docker components before a clean Docker Engine install:

```bash
sudo apt-get remove docker docker-engine docker.io containerd runc
```

Resynchronise `apt` package indexes from source (refresh), before any new operations are performed:

```bash
sudo apt-get update
```

Allow `apt` to use a repository over https:

```bash
sudo apt-get install \
    ca-certificates \
    curl \
    gnupg \
    lsb-release
```

Install the official Docker GPG key:

```bash
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
```

Configure a ***stable*** Docker repository:

```bash
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

Update the `apt` package index:

```bash
sudo apt-get update
```

Install the latest version of the Docker Engine:

```bash
sudo apt-get install docker-ce docker-ce-cli containerd.io
```

Start the Docker service:

```bash
sudo service docker start
```

Check the version of Docker installed:

```bash
docker -v
```

Pull and run the `hello-world` Image (to test a Docker installation):

```bash
docker run hello-world
```

Create a Docker group:

```bash
sudo groupadd docker
```

Add a user to the Docker group:

```bash
sudo usermod -aG docker $USER
```

Pull the SQL Server 2017 latest Docker Image:

```bash
docker pull mcr.microsoft.com/mssql/server:2017-latest
```

List all pulled Docker Images:

```bash
docker image ls
```

Spin up a new Container using the SQL Server 2017 Image:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD={YOUR_STRONG_PASSWORD}" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

List all running Containers:

```bash
docker container ls
```

List all Containers:

```bash
docker container ls -a
```

Install Docker Compose:

```bash
sudo curl -L https://github.com/docker/compose/releases/download/1.29.2/docker-compose-`uname -s`-`uname -m` -o /usr/local/bin/docker-compose
```

Apply executable permissions to the Docker Compose binary:

```bash
sudo chmod +x /usr/local/bin/docker-compose
```

Sample Docker Compose to configure SQL 2017, MongoDB and Mongo Express:

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

Access Windows folder as `mnt` from bash (Ubuntu):

```bash
cd /mnt/c/dockercompose
```

Execute a Docker Compose file (with the standard name of `docker-compose.yml`):

```bash
docker-compose up
```

Grant permissions to a UNIX user to modify directories (swap in user for `$USER`):

```bash
sudo chown -R $USER /etc
```

Sample `wsl.conf` file:

```text
[boot]
command = service docker start
```

Sample `.wslconfig` file:

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
