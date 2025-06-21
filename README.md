# Factorio Mod Packer
The main goal of this project is to reduce the Factorio mod archive preparation to calling a single shell command. This program packages all files from the specific folder into archive (excluding files whose name starts with a dot to ignore git-related files and folders) and place it into */Factorio/mods*.
##  Installation
1. Download latest release (or build executable file from source)
2. Place it wherever you want
## Usage
1. Open terminal and go to folder with application
2. Execute command `./FactorioModPacker pack --from path-to-mod-folder`

The program packs all files from `path-to-mod-folder` directory and place archive into Factorio/mods
